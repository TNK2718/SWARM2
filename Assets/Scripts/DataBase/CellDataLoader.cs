using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase {
    public class CellDataLoader {
        public Board.CellStatusType[] CellStatusTypes;

        // TODO : 外部に保存して読み込むようにする(プレイヤーが編集できるようにする)
        public CellDataLoader(int cellStateSize) {
            CellStatusTypes = new Board.CellStatusType[cellStateSize];
            CellStatusTypes[0] = new Board.CellStatusType() {
                Armor = 0,
                Cost = 0,
                CellFunction = Board.CellFunction.None
            };
            CellStatusTypes[1] = new Board.CellStatusType() {
                Armor = 1,
                Cost = 1,
                CellFunction = Board.CellFunction.Normal
            };
            CellStatusTypes[2] = new Board.CellStatusType() {
                Armor = 1,
                Cost = 1,
                CellFunction = Board.CellFunction.Normal
            };
            CellStatusTypes[3] = new Board.CellStatusType() {
                Armor = 1,
                Cost = 1,
                CellFunction = Board.CellFunction.Normal
            };
            CellStatusTypes[4] = new Board.CellStatusType() {
                Armor = 1,
                Cost = 1,
                CellFunction = Board.CellFunction.Normal
            };
            CellStatusTypes[5] = new Board.CellStatusType() {
                Armor = 1,
                Cost = 1,
                CellFunction = Board.CellFunction.Normal
            };
            CellStatusTypes[6] = new Board.CellStatusType() {
                Armor = 1,
                Cost = 1,
                CellFunction = Board.CellFunction.Normal
            };
            CellStatusTypes[7] = new Board.CellStatusType() {
                Armor = 1,
                Cost = 1,
                CellFunction = Board.CellFunction.Normal
            };
            CellStatusTypes[8] = new Board.CellStatusType() {
                Armor = 5,
                Cost = 5,
                CellFunction = Board.CellFunction.Eater
            };
            CellStatusTypes[9] = new Board.CellStatusType() {
                Armor = 10,
                Cost = 10,
                CellFunction = Board.CellFunction.Turret
            };
        }
    }
}
