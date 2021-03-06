﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Board {
    class CellAutomataGame {
        public int boardSize;
        public CellGrid myCellGrid;  // 自分のセルを管理するBoard
        public CellGrid enemyCellGrid;  // 敵のセルを管理するBoard
        private CellStatusType[] myCellStatusTypes;
        private CellStatusType[] enemyCellStatusTypes;
        [SerializeField] private Character.PlayerCharacter myPlayerCharacter;
        [SerializeField] private Character.PlayerCharacter enemyPlayerChracter;

        enum GameState {
            INGAME, PLAYER1WIN, PLAYER2WIN
        }

        public CellAutomataGame(int[] rules_in1, int[] rules_in2, int initBoardSize, int initialResource) {
            boardSize = initBoardSize;
            myCellGrid = new CellGrid(rules_in1, boardSize, initialResource);
            enemyCellGrid = new CellGrid(rules_in2, boardSize, initialResource);
            myCellStatusTypes = myCellGrid.CellStatusTypes;
            enemyCellStatusTypes = enemyCellGrid.CellStatusTypes;
        }

        public void UpdateGameBoard() {
            myCellGrid.UpdateBoard();
            enemyCellGrid.UpdateBoard();
            ApplyCellFunctionToGrids();
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
        public void ApplyCellFunctionToGrids() {
            myCellGrid.CopyToBoardBuffer();
            enemyCellGrid.CopyToBoardBuffer();
            for (int x = 0; x < boardSize; x++) {
                for (int y = 0; y < boardSize; y++) {
                    ApplyCellFunctionToCell(
                        myCellGrid, enemyCellGrid, myPlayerCharacter, enemyPlayerChracter, myCellStatusTypes, x, y);
                    ApplyCellFunctionToCell(
                        enemyCellGrid, myCellGrid, enemyPlayerChracter, myPlayerCharacter, enemyCellStatusTypes, x, y);
                }
            }
            myCellGrid.CopyToBoard();
            enemyCellGrid.CopyToBoard();
        }

        // 各セル毎のFunctionの処理
        private void ApplyCellFunctionToCell(
            CellGrid OwnerCellGrid, CellGrid AnotherCellGrid, 
            Character.PlayerCharacter OwnerPlayerCharacter, Character.PlayerCharacter AnotherPlayerChracter, 
            CellStatusType[] cellStatusTypes, int x, int y) {
            switch (cellStatusTypes[OwnerCellGrid.GetCell(true, x, y)].CellFunction) {
                case CellFunction.Normal:
                    break;
                case CellFunction.Eater:
                    // セルの破壊
                    for(int i = -1; i <= 1; i++) {
                        for(int j = -1; j <= 1; j++) {
                            if (i != 0 || j != 0) AnotherCellGrid.SetCell(false, x + i, y + j, 0);
                        }
                    }
                    //
                    // TODO : プレイヤーへの攻撃
                    //
                    break;
                case CellFunction.Turret:
                    // TODO : 最もコストの高いセルorプレイヤーを攻撃(関数として分けたほうがいいかな？)
                    if((AnotherPlayerChracter.GetPosition() - new Vector2(x, y)).magnitude >= CellStatusType.TURRETRANGE) {
                        int minDistance = CellStatusType.TURRETRANGE;
                        int maxCost = -1;
                        (int dx, int dy) position = (0, 0);
                        for(int i = -CellStatusType.TURRETRANGE; i <= CellStatusType.TURRETRANGE; i++) {
                            for(int j = -CellStatusType.TURRETRANGE; j <= CellStatusType.TURRETRANGE; j++) {
                                if(cellStatusTypes[AnotherCellGrid.GetCell(false, x + i, y + j)].Cost >= maxCost) {
                                    maxCost = cellStatusTypes[AnotherCellGrid.GetCell(false, x + i, y + j)].Cost;
                                    if(i * i + j * j <= minDistance) {
                                        minDistance = i * i + j * j;
                                        position = (i, j);
                                    }
                                }
                            }
                        }
                        AnotherCellGrid.SetCell(false, x + position.dx, y + position.dy, 0);
                    } else {
                        AnotherPlayerChracter.TakeDamage(CellStatusType.TURRETPOWER);
                    }
                    break;
            }
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
