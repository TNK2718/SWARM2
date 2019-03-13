namespace Character {
    public class PlayerCharacterStatus {
        int MaxHp = 100;
        int Hp    = 100;
        (int   baseValue, int   currentValue) BaseAtk = (100, 100);
        (int   baseValue, int   currentValue) Atk     = (100, 100);
        (int   baseValue, int   currentValue) Armor   = (100, 100);
        (float baseValue, float currentValue) Speed   = (100, 100);
        (int   baseValue, int   currentValue) Regen   = (100, 100);
        (int   baseValue, int   currentValue) NanoMachineArts = (100, 100);
        (int   baseValue, int   currentValue) AntiNanoMachine = (100, 100);
    }
}
