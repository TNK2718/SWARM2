using System.Collections;
using System.Collections.Generic;

namespace DataBase {
    public class CharacterDataFormat{
        public int Id { get; set; }
        public int Maxhp { get; set; }
        public int BaseAtk { get; set; }
        public int BaseArmor { get; set; }
        public float BaseSpeed { get; set; }
        public int BaseRegen { get; set; }
        public int BaseNanoMachineArts { get; set; }
        public int BaseAntiNanoMachine { get; set; }
        public List<int> SkillList { get; set; }
    }
}
