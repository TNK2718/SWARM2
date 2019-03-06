using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace GeneticAlgorithm
{
    class CellAutomataGame
    {
        public int boardSize;
        public Board board1;
        public Board board2;

        enum GameState
        {
            INGAME, PLAYER1WIN, PLAYER2WIN
        }

        public CellAutomataGame(int[] rules_in1, int[] rules_in2, int initBoardSize)
        {
            boardSize = initBoardSize;
            board1 = new Board(rules_in1, boardSize);
            board2 = new Board(rules_in2, boardSize);
        }

        public void UpdateGameBoard()
        {
            board1.UpdateBoard();
            board2.UpdateBoard();
            ApplyCellfunction();
            ApplyCollision();
        }

        private void ApplyCollision()
        {
            for(int x = 0; x < boardSize; x++) {
                for(int y = 0; y < boardSize; y++) {
                    if(board1.GetCell(board1.board, x, y) != 0 && board2.GetCell(board2.board, x, y) != 0) {
                        board1.SetCell(true, x, y, 0);
                        board2.SetCell(true, x, y, 0);
                    }
                }
            }
        }

        public void ApplyCellfunction()
        {

        }

        public void InitializeBoards()
        {
            board1.ClearBoard();
            board2.ClearBoard();
            board1.SetCell(true, 1, 1, 1);
            board2.SetCell(true, boardSize - 1, boardSize - 1, 1);
        }

        public void Draw(List<List<GameObject>> cellSprites)
        {
            for (int y = 0; y < boardSize; y++) {
                for (int x = 0; x < boardSize; x++) {
                    if (board1.GetCell(board1.board, x, y) != 0) {
                        cellSprites[y][x].SetActive(true);
                        cellSprites[y][x].GetComponent<ImageList>().changeImage(0);
                    } else if (board2.GetCell(board2.board, x, y) != 0) {
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
