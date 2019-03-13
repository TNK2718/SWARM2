using UnityEngine;
using System;
using System.Collections.Generic;

namespace Character {
    public class CharacterData {
        public PlayerCharacterStatus Status = new PlayerCharacterStatus();
        public List<int> SkillList = new List<int>();  // TODO: SkillList��Status�Ɋ܂܂��ׂ�������
        public List<Buff> Buffs = new List<Buff>();

        // �{�[�h�̃}�X�ł̍��W
        public Vector2 Position = new Vector2(0, 0);
        public Vector2Int Destination = new Vector2Int(0, 0);

        public int CurrentSkillId = -1; // ���ݎg�p���悤�Ƃ��Ă���X�L��
    }

    // �v���C���[�����삷��L�����N�^�[�̎���
    public class PlayerCharacter {
        private int Id;
        private bool Owned;  // ���̒[���̃v���C���[���ǂ���
        private CharacterData characterData = new CharacterData();

        private ActionStrategyBase CurrentStrategy; // �v���C���[�̏�Ԃ��Ǘ�����
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

        // �o�t�ǉ�
        public void AddBuff(Buff buff) {
            characterData.Buffs.Add(buff);
        }

        // �o�t�̏���
        public void ProcessBuffs() {
            foreach(Buff buff in characterData.Buffs) {
                if (buff.Duration == 0) {
                    //
                    // �o�t���؂��ۂ̏���
                    //
                    characterData.Buffs.Remove(buff);
                } else buff.Duration -= Time.deltaTime;
                //
                // �e�o�t���̏���
                //
            }
        }

        // Destination�܂ŕb��speed�ňړ�����i���t���[���Ăяo���j
        public void MoveToDestination(float speed) {
            // TODO: ��������
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

        // �X�L���̏���
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

        // �v���C���[�̏�Ԃ�ύX���郁�\�b�h����
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
