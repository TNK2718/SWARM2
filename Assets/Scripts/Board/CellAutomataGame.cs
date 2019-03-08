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

        // 描画
        public void Draw(List<List<GameObject>> cellSprites) {
            for (int y = 0; y < boardSize; y++) {
                for (int x = 0; x < boardSize; x++) {
                    if (myCellGrid.GetCell(true, x, y) != 0) {
                        cellSprites[y][x].SetActive(true);
                        cellSprites[y][x].GetComponent<ImageList>().changeImage(0);
                    } else if (enemyCellGrid.GetCell(true, x, y) != 0) {
                        cellSprites[y][x].SetActive(true);  // TODO: キャラで色を変える
                        cellSprites[y][x].GetComponent<ImageList>().changeImage(1);
                    } else {
                        cellSprites[y][x].SetActive(false);
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
