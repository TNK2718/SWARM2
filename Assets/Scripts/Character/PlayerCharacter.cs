using UnityEngine;
using System.Collections.Generic;

namespace Character {
    // �v���C���[�����삷��L�����N�^�[�̎���
    public class PlayerCharacter : MonoBehaviour{
        public int Id { get; set; }
        public bool Owned { get; set; } // ���̒[���̃v���C���[���ǂ���
        public int Maxhp { get; set; }
        public int Hp { get; set; }
        public int BaseAtk { get; set; }
        public int Atk { get; set; }
        public int Armor { get; set; }
        public float BaseSpeed { get; set; }
        public float Speed { get; set; }
        public int BaseRegen { get; set; }
        public int Regen { get; set; }
        public int BaseNanoMachineArts { get; set; }
        public int NanoMachineArts { get; set; }
        public int BaseAntiNanoMachine { get; set; }
        public int AntiNanoMachine { get; set; }
        public List<int> SkillList { get; set; }
        public List<Buff> Buffs { get; set; }
        public Vector2 Destination { get; set; }
        public int CurrentSkillId { get; set; }
        public ActionStrategyBase CurrentStrategy { get; set; } // �v���C���[�̏�Ԃ��Ǘ�����
        private WaitStrategy waitStrategy;
        private SkillStrategy skillStrategy;
        private DeadStrategy deadStrategy;
        private FreezeStrategy freezeStrategy;

        // �o�t�ǉ�
        public void AddBuff(Buff buff) {
            Buffs.Add(buff);
        }

        // �o�t�̏���
        public void ProcessBuffs() {
            foreach(Buff buff in Buffs) {
                if (buff.Duration == 0) {
                    //
                    // �o�t���؂��ۂ̏���
                    //
                    Buffs.Remove(buff);
                } else buff.Duration -= Time.deltaTime;
                //
                // �e�o�t���̏���
                //
            }
        }

        // Destination�܂ŕb��speed�ňړ�����i���t���[���Ăяo���j
        public void MoveToDestination(float speed) {
            if (Destination == null) return;
            Vector2 position = gameObject.transform.position;
            Vector2 differrence = new Vector2();
            differrence = Destination - position;
            if (differrence.magnitude <= speed) transform.position = Destination;
            else {
                position += speed * differrence.normalized;
                transform.position = position;
            }
        }

        // �X�L���̏���
        public void UseSkill(Vector2 targetPosition) {

        }

        // �v���C���[�̏�Ԃ�ύX���郁�\�b�h����
        public void SetWait() {
            CurrentStrategy = waitStrategy;
        }

        public void SetSkill() {
            CurrentStrategy = skillStrategy;
        }

        public void SetDead() {
            CurrentStrategy = deadStrategy;
        }

        public void SetFreeze() {
            CurrentStrategy = freezeStrategy;
        }

        private void Start() {
            waitStrategy = GetComponent<WaitStrategy>();
            skillStrategy = GetComponent<SkillStrategy>();
            deadStrategy = GetComponent<DeadStrategy>();
            freezeStrategy = GetComponent<FreezeStrategy>();
            CurrentStrategy = waitStrategy;
        }

        private void Update() {
            ProcessBuffs();
            CurrentStrategy.ReceiveInput();
            MoveToDestination(Speed);
        }
    }
}
