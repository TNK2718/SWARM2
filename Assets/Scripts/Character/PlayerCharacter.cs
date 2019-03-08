namespace Character {
    // プレイヤーが操作するキャラクターの実装
    public class PlayerCharacter {
        public int Id { get; set; }
        public int Maxhp { get; set; }
        public int Hp { get; set; }
        public int BaseAtk { get; set; }
        public int Atk { get; set; }
        public int Armor { get; set; }
        public int BaseSpeed { get; set; }
        public int Speed { get; set; }
        public int BaseRegen { get; set; }
        public int Regen { get; set; }
        public int BaseNanoMachineArts { get; set; }
        public int NanoMachineArts { get; set; }
        public int BaseAntiNanoMachine { get; set; }
        public int AntiNanoMachine { get; set; }
        public PlayerStatus[] PlayerStatus { get; set; }
        public ActionStrategyBase ActionStrategyBase { get; set; }
    }
}
