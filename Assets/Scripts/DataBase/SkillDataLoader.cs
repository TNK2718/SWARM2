using System.Collections;
using System.Collections.Generic;

namespace DataBase {
    public class SkillDataLoader : DataLoaderBase{
        public List<SkillDataFormat> skillDataFormats;
        private string[,] data;

        public SkillDataLoader() {
            skillDataFormats = new List<SkillDataFormat>();
            ReadCSV("/Scripts/DataBase/CSVData/SkillData.csv", ref data);
            for (int i = 0; i < data.GetLength(0); i++) {
                SkillDataFormat tmp = new SkillDataFormat(
                    int.Parse(data[i, 0]), data[i, 1], data[i, 2], data[i, 3], 
                    data[i, 4], float.Parse(data[i, 5]), int.Parse(data[i, 6]), int.Parse(data[i, 7]), int.Parse(data[i, 8])
                    );
                skillDataFormats.Add(tmp);
            }
        }
    }
}
