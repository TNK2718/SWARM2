using UnityEngine;
using System;
using System.Collections.Generic;

namespace Character {
    public class CharacterData {
        public PlayerCharacterStatus Status = new PlayerCharacterStatus();
        public List<int> SkillList = new List<int>();  // TODO: SkillListはStatusに含まれるべきか聞く
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
        private Board.CellGrid cellGrid;
        private int boardSize;

        public PlayerCharacter(int boardSize) {
            this.boardSize = boardSize;
            CurrentStrategy = new Strategy.Default(boardSize);
        }

        public void Update((bool found, int x, int y) mouseHoveredCell) {
            ProcessBuffs();
            CurrentStrategy.Update(characterData, mouseHoveredCell);
            // TODO
            // MoveToDestination(Speed);
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

        // Destinationまで秒速speedで移動する（毎フレーム呼び出す）
        public void MoveToDestination(float speed) {
            // TODO: 書き換え
            // if (characterData.Destination == null) return;
            // Vector2 position = gameObject.transform.position;
            // Vector2 differrence = new Vector2();
            // differrence = Destination - position;
            // if (differrence.magnitude <= speed) transform.position = Destination;
            // else {
            //     position += speed * differrence.normalized;
            //     transform.position = position;
            // }
        }

        // スキルの処理
        public void UseSkill(Vector2 targetPosition) {
            switch (skillDataLoader.skillDataFormats[characterData.CurrentSkillId].SkillType) {
                case DataBase.SkillType.Beam:
                    break;
                case DataBase.SkillType.Bomb:
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
