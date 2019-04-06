using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace DataBase {
    // セルデータの読み込み
    public class CellDataLoader : DataLoaderBase {
        public CellDataFormat[] cellDataFormats;
        private string[,] data;
        public CellDataLoader(int cellStateSize) {
            cellDataFormats = new CellDataFormat[cellStateSize];
            ReadCSV("/Scripts/DataBase/CSVData/CellData.csv", ref data);
            for (int i = 0; i < cellStateSize; i++) {
                cellDataFormats[i] = new CellDataFormat() {
                    Armor = int.Parse(data[i, 0]),
                    Cost = int.Parse(data[i, 1]),
                    CellFunction = data[i, 2]
                };
            }
        }
    }
}
