namespace Character {
    // スキルを発動してない状態を表す。スキル発動の準備時はSkillStrategyに移行
    public class DeadStrategy : ActionStrategyBase {
        public override void ReceiveInput() {
            throw new System.NotImplementedException();
        }
    }
}
