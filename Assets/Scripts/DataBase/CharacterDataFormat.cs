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
        public int BaseMicroMachineArts { get; set; }
        public int BaseAntiMicroMachine { get; set; }
        public List<int> SkillList { get; set; }
        public List<Character.Buff> initialBuffList;
    }
}
