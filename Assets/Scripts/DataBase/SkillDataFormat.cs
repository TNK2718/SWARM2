using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase {
    public class SkillDataFormat {
        public int Id;
        public SkillType SkillType;
        public AreaType AreaType;
        public float FloatParameter;
        public int parameter1;
        public int parameter2;
        public int parameter3;

        public SkillDataFormat(int _id, int _skillType, int _areaType, float _fParameter, 
            int _parameter1, int _parameter2, int _parameter3) {
            Id = _id;
            SkillType = (SkillType) _skillType;
            AreaType = (AreaType)_areaType;
            FloatParameter = _fParameter;
            parameter1 = _parameter1;
            parameter2 = _parameter2;
            parameter3 = _parameter3;
        }
    }
}
