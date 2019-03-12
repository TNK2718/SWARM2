using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Board {
    class CellAutomataGame {
        public int boardSize;
        public CellGrid myCellGrid;  // 自分のセルを管理するBoard
        public CellGrid enemyCellGrid;  // 敵のセルを管理するBoard

        enum GameState {
            INGAME, PLAYER1WIN, PLAYER2WIN
        }

        public CellAutomataGame(int[] rules_in1, int[] rules_in2, int initBoardSize, int initialResource) {
            boardSize = initBoardSize;
            myCellGrid = new CellGrid(rules_in1, boardSize, initialResource);
            enemyCellGrid = new CellGrid(rules_in2, boardSize, initialResource);
        }

        public void UpdateGameBoard() {
            myCellGrid.UpdateBoard();
            enemyCellGrid.UpdateBoard();
            ApplyCellfunction();
            ApplyCollision();
        }

        // 自分と相手のセルが重なっていた場合、どちらも状態0にする
        private void ApplyCollision() {
            for (int x = 0; x < boardSize; x++) {
                for (int y = 0; y < boardSize; y++) {
                    if (myCellGrid.GetCell(true, x, y) != 0 && enemyCellGrid.GetCell(true, x, y) != 0) {
                        myCellGrid.SetCell(true, x, y, 0);
                        enemyCellGrid.SetCell(true, x, y, 0);
                    }
                }
            }
        }

        // セルの特殊機能（Function）の実装
        public void ApplyCellfunction() {

        }

        public void InitializeBoards() {
            myCellGrid.ClearBoard();
            enemyCellGrid.ClearBoard();
            myCellGrid.SetCell(true, 1, 1, 1);
            enemyCellGrid.SetCell(true, boardSize - 1, boardSize - 1, 1);
        }

        public List<List<Boolean>> getMyBoardData() {
            var data = new List<List<Boolean>>();
            for (int y = 0; y < boardSize; y++) {
                var row = new List<Boolean>();
                for (int x = 0; x < boardSize; x++) {
                    row.Add(myCellGrid.GetCell(true, x, y) != 0);
                }
                data.Add(row);
            }
            return data;
        }

        public List<List<Boolean>> getEnemyBoardData() {
            var data = new List<List<Boolean>>();
            for (int y = 0; y < boardSize; y++) {
                var row = new List<Boolean>();
                for (int x = 0; x < boardSize; x++) {
                    row.Add(enemyCellGrid.GetCell(true, x, y) != 0);
                }
                data.Add(row);
            }
            return data;
        }
    }
}
