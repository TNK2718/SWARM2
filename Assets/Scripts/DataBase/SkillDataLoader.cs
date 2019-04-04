using System.Collections;
using System.Collections.Generic;

namespace DataBase {
    public class SkillDataLoader : DataLoaderBase{
        public List<SkillDataFormat> skillDataFormats;
        private string[,] data;

        public SkillDataLoader(int cellStateSize) {
            skillDataFormats = new List<SkillDataFormat>();
            ReadCSV("", ref data);
            for (int i = 0; i < cellStateSize; i++) {
                skillDataFormats[i] = new SkillDataFormat(
                    int.Parse(data[i, 0]), int.Parse(data[i, 1]), int.Parse(data[i, 2]), float.Parse(data[i, 3]), 
                    int.Parse(data[i, 4]), int.Parse(data[i, 5]), int.Parse(data[i, 6])
                    );
            }
        }
    }
}
