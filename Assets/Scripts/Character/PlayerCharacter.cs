using UnityEngine;
using System;
using System.Collections.Generic;

namespace Character {
    // キャラクターのデータ
    public class CharacterData {
        public PlayerCharacterStatus Status = new PlayerCharacterStatus();
        public List<Buff> Buffs = new List<Buff>();

        // ボードのマスでの座標
        public Vector2 Position = new Vector2(0, 0);
        public Vector2Int Destination = new Vector2Int(0, 0);

        public int CurrentSkillId = -1; // 現在使用しようとしているスキル
    }

    // プレイヤーが操作するキャラクターの実装
    public class PlayerCharacter {
        private int Id;
        private bool Owned;  // この端末のプレイヤーかどうか
        private CharacterData characterData = new CharacterData();
        private ActionStrategyBase CurrentStrategy; // プレイヤーの状態を管理する
        private DataBase.SkillDataLoader skillDataLoader =
            new DataBase.SkillDataLoader();
        private DataBase.CharacterDataLoader characterDataLoader =
            new DataBase.CharacterDataLoader();
        private Board.CellStatusType[] myCellStatusTypes;
        private Board.CellStatusType[] enemyCellStatusTypes;
        [SerializeField] private Board.CellGrid myCellGrid;
        [SerializeField] private Board.CellGrid enemyCellGrid;
        [SerializeField] private PlayerCharacter enemyCharacter;
        private int boardSize;

        public PlayerCharacter(int boardSize) {
            this.boardSize = boardSize;
            CurrentStrategy = new Strategy.Default(boardSize);
        }

        public void Update((bool found, int x, int y) mouseHoveredCell) {
            ProcessBuffs();
            CurrentStrategy.Update(characterData, mouseHoveredCell);
            MoveToDestination(characterData.Status.Speed.currentValue);
        }

        public Vector2 GetPosition() {
            return characterData.Position;
        }

        // バフ追加
        public void AddBuff(Buff buff) {
            characterData.Buffs.Add(buff);
        }

        // バフの処理
        public void ProcessBuffs() {
            foreach(Buff buff in characterData.Buffs) {
                if (buff.Duration == 0) {
                    //
                    // バフが切れる際の処理
                    //
                    characterData.Buffs.Remove(buff);
                } else buff.Duration -= Time.deltaTime;
                //
                // 各バフ毎の処理
                //
            }
        }

        // ダメージを受ける
        public void TakeDamage(int power) {
            // TODO : ダメージ計算式を考える(減算or除算or?)
        }

        // Destinationまで秒速speedで移動する（毎フレーム呼び出す）
        public void MoveToDestination(float speed) {
            if (characterData.Position == characterData.Destination) {
                return;
            }
            var differrence = characterData.Destination - characterData.Position;
            if (differrence.magnitude <= speed) {
                characterData.Position = characterData.Destination;
            } else {
                characterData.Position += speed * differrence.normalized;
            }
        }

        // スキルの処理
        public void UseSkill(Vector2 targetPosition) {
            var skillData = skillDataLoader.skillDataFormats[characterData.CurrentSkillId];
            switch (skillData.SkillType) {
                case DataBase.SkillType.Beam:
                    for(int i = 0; i < skillData.FloatParameter; i++) {
                        int power1 = skillData.parameter1;
                        var direction = targetPosition - GetPosition();
                        direction.Normalize();
                        if(enemyCellStatusTypes[
                            enemyCellGrid.GetCell(true, (int) (i * direction.x), (int) (i * direction.y))].Armor <= power1) {

                        }
                    }
                    break;
                case DataBase.SkillType.Bomb:
                    int power2 = skillData.parameter1;
                    for(int i = (int) -skillData.FloatParameter; i < skillData.FloatParameter; i++) {
                        for (int j = (int) -skillData.FloatParameter; j < skillData.FloatParameter; j++) {
                            if(i * i + j * j <= skillData.FloatParameter * skillData.FloatParameter && 
                                power2 >= enemyCellStatusTypes[
                                    enemyCellGrid.GetCell(true, (int)targetPosition.x + i, (int)targetPosition.y + j)].Armor) {
                                power2 -= enemyCellStatusTypes[
                                    enemyCellGrid.GetCell(true, (int)targetPosition.x + i, (int)targetPosition.y + j)].Armor;
                            }
                        }
                    }
                    if ((enemyCharacter.GetPosition() - targetPosition).magnitude <=
                        skillData.FloatParameter) {
                        enemyCharacter.TakeDamage(skillData.parameter1);
                    }
                    break;
                case DataBase.SkillType.Construction:
                    break;             
            }
        }

        // プレイヤーの状態を変更するメソッドたち
        public void SetWait() {
            // CurrentStrategy = waitStrategy;
        }

        public void SetSkill() {
            // CurrentStrategy = skillStrategy;
        }

        public void SetDead() {
            // CurrentStrategy = deadStrategy;
        }

        public void SetFreeze() {
            // CurrentStrategy = freezeStrategy;
        }
    }
}
