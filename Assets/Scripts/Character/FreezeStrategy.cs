namespace Character {
    // 操作を受け付けない状態
    public class FreezeStrategy : ActionStrategyBase {
        public override void ReceiveInput() {
            throw new System.NotImplementedException();
        }
    }
}
