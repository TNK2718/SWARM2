namespace Character {
    public class PlayerCharacterStatus {
        public int MaxHp = 100;
        public int Hp    = 100;
        public (int   baseValue, int   currentValue) BaseAtk = (100, 100);
        public (int   baseValue, int   currentValue) Atk     = (100, 100);
        public (int   baseValue, int   currentValue) Armor   = (100, 100);
        public (float baseValue, float currentValue) Speed   = (0.07f, 0.07f);
        public (int   baseValue, int   currentValue) Regen   = (100, 100);
        public (int   baseValue, int   currentValue) NanoMachineArts = (100, 100);
        public (int   baseValue, int   currentValue) AntiNanoMachine = (100, 100);
    }
}
