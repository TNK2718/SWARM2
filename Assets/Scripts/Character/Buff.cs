namespace Character {
    // �v���C���[�̓����Ԃ�o�t���Ǘ�����
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
