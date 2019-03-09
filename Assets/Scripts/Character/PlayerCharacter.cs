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
        public List<Buff> Buffs { get; set; }
        public Vector2 Destination { get; set; }
        public ActionStrategyBase CurretStrategy { get; set; }
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
                if (buff.Duration == 0) Buffs.Remove(buff);
                else buff.Duration -= Time.deltaTime;
            }
        }

        // カーソルで指定した点まで秒速speedで移動する（毎フレーム呼び出す）
        public void MoveToDestination(float speed) {

        }

        // プレイヤーの状態の変更
        public void SetWait() {
            CurretStrategy = waitStrategy;
        }

        public void SetSkill() {
            CurretStrategy = skillStrategy;
        }

        public void SetDead() {
            CurretStrategy = deadStrategy;
        }

        public void SetFreeze() {
            CurretStrategy = freezeStrategy;
        }

        private void Start() {
            waitStrategy = GetComponent<WaitStrategy>();
            skillStrategy = GetComponent<SkillStrategy>();
            deadStrategy = GetComponent<DeadStrategy>();
            freezeStrategy = GetComponent<FreezeStrategy>();
            CurretStrategy = waitStrategy;
        }

        private void Update() {
            ProcessBuffs();
            MoveToDestination(Speed);
        }
    }
}
