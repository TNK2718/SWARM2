using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace GeneticAlgorithm {
    class Board {
        private int BOARD_SIZE;
        public readonly int CELL_STATE_SIZE = 8;
        private readonly int NUM_MOORE_NEIGHBORHOOD = 9;
        [SerializeField] private int INITIAL_RESOURCE;
        public int[,] board;
        private int[,] board_buffer;
        private int[] rules;
        public int Resource {
            get { return Resource; }
            set {
                Resource = value;
                if (value < 0) Resource = 0;
            }
        }
        public CellSpec[] CellSpecs { get; set; }

        public Board(int[] rules_in, int boardsize) {
            BOARD_SIZE = boardsize;
            Resource = INITIAL_RESOURCE;
            board = new int[BOARD_SIZE, BOARD_SIZE];
            board_buffer = new int[BOARD_SIZE, BOARD_SIZE];
            CellSpecs = new CellSpec[CELL_STATE_SIZE];
            for (int x = 0; x < BOARD_SIZE; x++) {
                for (int y = 0; y < BOARD_SIZE; y++) {
                    SetCell(false, x, y, 0);
                }
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

        public int GetConditionsNo(bool is_board, int x, int y) {
            int result = 0;
            for (int i = 0; i < NUM_MOORE_NEIGHBORHOOD; i++) {
                result += FastPower(CELL_STATE_SIZE, i) * GetCell(is_board, x + i % 3 - 1, y + i / 3 - 1);
            }
            return result;
        }

        public void UpdateBoard() {
            Array.Copy(board, board_buffer, BOARD_SIZE * BOARD_SIZE);
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
                        && Resource >= CellSpecs[rule / FastPower(CELL_STATE_SIZE, NUM_MOORE_NEIGHBORHOOD)].Cost) {
                        SetCell(false, x, y, rule / FastPower(CELL_STATE_SIZE, NUM_MOORE_NEIGHBORHOOD));
                        ConsumeResuorce(GetCell(true, x, y), GetCell(false, x, y), CellSpecs);
                    }
                }
            }
            Array.Copy(board_buffer, board, BOARD_SIZE * BOARD_SIZE);
        }

        public void ConsumeResuorce(int preState, int nextState, CellSpec[] _cellSpecs) {
            Resource -= _cellSpecs[nextState].Cost - _cellSpecs[preState].Cost;
        }

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

        public void Draw() {
            for (int y = 0; y < BOARD_SIZE; y++) {
                for (int x = 0; x < BOARD_SIZE; x++) {
                    Console.Write(GetCell(true, x, y));
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public void ClearBoard() {
            for (int x = 0; x < BOARD_SIZE; x++) {
                for (int y = 0; y < BOARD_SIZE; y++) {
                    SetCell(true, x, y, 0);
                }
            }
        }
    }

    class CellSpec {
        public int Armor { get; set; }
        public int Cost { get; set; }
        public CellFunction CellFunction { get; set; }
    }

    public enum CellFunction {

    }
}
