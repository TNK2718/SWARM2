using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Board {
    // セルはこのボードによって管理される。
    // キャラクターは管理しない。
    class CellGrid {
        private int BOARD_SIZE;
        public const int CELL_STATE_SIZE = 8;
        private readonly int NUM_MOORE_NEIGHBORHOOD = 9;
        public int[,] board;
        private int[,] board_buffer;
        private int[] rules;
        private int resource;
        public int Resource {
            get { return resource; }
            set {
                resource = value;
                if (value < 0) resource = 0;
            }
        }
        public CellStatusType[] CellStatusTypes { get; set; }
        private DataBase.CellDataLoader CellDataLoader;

        public CellGrid(int[] rules_in, int boardsize, int initialResource) {
            BOARD_SIZE = boardsize;
            Resource = initialResource;
            board = new int[BOARD_SIZE, BOARD_SIZE];
            board_buffer = new int[BOARD_SIZE, BOARD_SIZE];
            CellStatusTypes = new CellStatusType[CELL_STATE_SIZE];
            CellDataLoader = new DataBase.CellDataLoader(CELL_STATE_SIZE);
            for(int i = 0; i < CELL_STATE_SIZE; i++) {
                CellStatusTypes[i] = new CellStatusType(CellDataLoader.cellDataFormats[i].Armor, 
                    CellDataLoader.cellDataFormats[i].Cost,
                    CellDataLoader.cellDataFormats[i].CellFunction);
            }
            rules = rules_in;
            Array.Sort(rules);
            ClearBoard();
        }

        public int GetCell(bool is_board, int x, int y) {
            if (x >= 0 && x < BOARD_SIZE && y >= 0 && y < BOARD_SIZE) {
                if (is_board == true) return board[x, y];
                else return board_buffer[x, y];
            } else {
                return 0;
            }
        }

        public void SetCell(bool is_board, int x, int y, int value) {
            if (x >= 0 && x < BOARD_SIZE && y >= 0 && y < BOARD_SIZE) {
                if (is_board == true) board[x, y] = value;
                else board_buffer[x, y] = value;
            } else {
                return;
            }
        }

        // 位置(x, y)のセルの近傍のセルの状態を取得する
        public int GetConditionsNo(bool is_board, int x, int y) {
            int result = 0;
            for (int i = 0; i < NUM_MOORE_NEIGHBORHOOD; i++) {
                result += FastPower(CELL_STATE_SIZE, i) * GetCell(is_board, x + i % 3 - 1, y + i / 3 - 1);
            }
            return result;
        }

        //  BoardをBoardBufferにコピー
        public void CopyToBoardBuffer() {
            Array.Copy(board, board_buffer, BOARD_SIZE * BOARD_SIZE);
        }

        // BoardBufferをBoardにコピー
        public void CopyToBoard() {
            Array.Copy(board_buffer, board, BOARD_SIZE * BOARD_SIZE);
        }

        // CellGridを次のStepにアップデートする
        public void UpdateBoard() {
            CopyToBoardBuffer();
            for (int x = 0; x < BOARD_SIZE; x++) {
                for (int y = 0; y < BOARD_SIZE; y++) {
                    int rule = -1;
                    for (int i = 0; i < CELL_STATE_SIZE; i++) {
                        int index = Array.BinarySearch(
                            rules,
                            GetConditionsNo(true, x, y)
                                + (CELL_STATE_SIZE - 1 - i) * FastPower(CELL_STATE_SIZE, NUM_MOORE_NEIGHBORHOOD)
                            );
                        if (index >= 0) rule = rules[index];
                        if (rule >= 0) break;
                    }
                    if (rule >= 0 && rule % FastPower(CELL_STATE_SIZE, NUM_MOORE_NEIGHBORHOOD) != 0
                        && Resource >= CellStatusTypes[rule / FastPower(CELL_STATE_SIZE, NUM_MOORE_NEIGHBORHOOD)].Cost) {
                        SetCell(false, x, y, rule / FastPower(CELL_STATE_SIZE, NUM_MOORE_NEIGHBORHOOD));
                        ConsumeResuorce(GetCell(true, x, y), GetCell(false, x, y), CellStatusTypes);
                    }
                }
            }
            CopyToBoard();
        }

        // リソースを消費する処理
        public void ConsumeResuorce(int preState, int nextState, CellStatusType[] _cellSpecs) {
            Resource -= _cellSpecs[nextState].Cost - _cellSpecs[preState].Cost;
        }

        // int型の累乗計算用メソッド
        public int FastPower(int number, int power) {
            if (power == 0) return 1;
            else if (power == 1) return number;
            else if (power % 2 == 0) {
                int tmp = FastPower(number, power / 2);
                return tmp * tmp;
            } else {
                int tmp = FastPower(number, (power - 1) / 2);
                return tmp * tmp * number;
            }
        }

        // 使わない
        public void Draw() {
            for (int y = 0; y < BOARD_SIZE; y++) {
                for (int x = 0; x < BOARD_SIZE; x++) {
                    Console.Write(GetCell(true, x, y));
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        // すべてのセルを状態0にする
        public void ClearBoard() {
            for (int x = 0; x < BOARD_SIZE; x++) {
                for (int y = 0; y < BOARD_SIZE; y++) {
                    SetCell(true, x, y, 0);
                    SetCell(false, x, y, 0);
                }
            }
        }
    }
}
