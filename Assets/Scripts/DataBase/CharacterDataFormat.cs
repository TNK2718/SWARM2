using System.Collections;
using System.Collections.Generic;

namespace DataBase {
    public class CharacterDataFormat{
        public const int CHRACTERNUM = 1; // キャラクターの数
        public const int SKILLNUM = 3; // skillの数は４つ
        public const int BUFFNUM = 5; // buffは５つ

        public int Id { get; set; }
        public int Maxhp { get; set; }
        public int BaseAtk { get; set; }
        public int BaseArmor { get; set; }
        public float BaseSpeed { get; set; }
        public int BaseRegen { get; set; }
        public int BaseMicroMachineArts { get; set; }
        public int BaseAntiMicroMachine { get; set; }
        public int[] SkillList { get; set; }
        public int[] initialBuffList;

        public CharacterDataFormat(int _id, int _maxhp, int _baseAtk, int _baseArmor, float _baseSpeed, int _baseRegen, 
            int _baseMicroMachineArts, int _baseAntiMicroMachines, int[] _skillList, int[] _initialBuffList) {
            Id = _id;
            Maxhp = _maxhp;
            BaseAtk = _baseAtk;
            BaseArmor = _baseArmor;
            BaseSpeed = _baseSpeed;
            BaseRegen = _baseRegen;
            BaseMicroMachineArts = _baseMicroMachineArts;
            BaseAntiMicroMachine = _baseAntiMicroMachines;
            SkillList = _skillList;
            initialBuffList = _initialBuffList;
        }
    }
}
