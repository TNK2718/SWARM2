namespace Character {
    // プレイヤーの特殊状態やバフを管理する
    public class Buff {
        public BuffType BuffType { get; set; }
        private float duration;
        public float Duration { get {
                return duration;
            } set {
                duration = value;
                if (value < 0) duration = 0;
            }
        }
    }
}
