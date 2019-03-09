using UnityEngine;
using System.Collections.Generic;

namespace Character {
    // プレイヤーが操作するキャラクターの実装
    public class PlayerCharacter : MonoBehaviour{
        public int Id { get; set; }
        public bool Owned { get; set; } // この端末のプレイヤーかどうか
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
        public ActionStrategyBase CurrentStrategy { get; set; } // プレイヤーの状態を管理する
        private WaitStrategy waitStrategy;
        private SkillStrategy skillStrategy;
        private DeadStrategy deadStrategy;
        private FreezeStrategy freezeStrategy;

        // バフ追加
        public void AddBuff(Buff buff) {
            Buffs.Add(buff);
        }

        // バフの処理
        public void ProcessBuffs() {
            foreach(Buff buff in Buffs) {
                if (buff.Duration == 0) {
                    //
                    // バフが切れる際の処理
                    //
                    Buffs.Remove(buff);
                } else buff.Duration -= Time.deltaTime;
                //
                // 各バフ毎の処理
                //
            }
        }

        // Destinationまで秒速speedで移動する（毎フレーム呼び出す）
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

        // スキルの処理
        public void UseSkill(Vector2 targetPosition) {

        }

        // プレイヤーの状態を変更するメソッドたち
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
