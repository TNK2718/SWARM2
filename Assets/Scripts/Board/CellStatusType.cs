namespace Board {
    // 状態数に割り当てられたセルの機能を表す（状態８のarmorは1で…など）
    public class CellStatusType {
        public const int TURRETRANGE = 5;
        public const int TURRETPOWER = 1;
        public int Armor { get; set; }
        public int Cost { get; set; }
        public CellFunction CellFunction { get; set; }
    }
}
