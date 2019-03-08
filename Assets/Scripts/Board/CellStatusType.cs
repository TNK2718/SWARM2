namespace Board {
    // 状態数に割り当てられたセルの機能を表す（状態８のarmorは1で…など）
    class CellStatusType {
        public int Armor { get; set; }
        public int Cost { get; set; }
        public CellFunction CellFunction { get; set; }
    }
}
