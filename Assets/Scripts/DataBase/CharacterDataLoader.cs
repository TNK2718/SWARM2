using System.Collections;
using System.Collections.Generic;

namespace DataBase {
    public class CharacterDataLoader : DataLoaderBase{
        public CharacterDataFormat[] characterDataFormats;
        private string[,] data;

        public CharacterDataLoader(int characterNumber) {
            characterDataFormats = new CharacterDataFormat[characterNumber];
            ReadCSV("/Scripts/DataBase/CSVData/ChracterData.csv", ref data);
            for (int i = 0; i < characterNumber; i++) {
                int[] skillList = new int[CharacterDataFormat.SKILLNUM];
                for(int j = 0; j < CharacterDataFormat.SKILLNUM; j++) {
                    skillList[j] = int.Parse(data[i, 8 + j]);
                }
                int[] buffList = new int[CharacterDataFormat.BUFFNUM];
                for(int k = 0; k < CharacterDataFormat.BUFFNUM; k++) {
                    buffList[k] = int.Parse(data[i, 8 + CharacterDataFormat.SKILLNUM + k]);
                }
                characterDataFormats[i] = new CharacterDataFormat(
                    int.Parse(data[i, 0]), int.Parse(data[i, 1]), int.Parse(data[i, 2]), int.Parse(data[i, 3]), 
                    float.Parse(data[i, 4]), int.Parse(data[i, 5]), int.Parse(data[i, 6]), int.Parse(data[i, 7]),
                    skillList, buffList);
            }
        }
    }
}
