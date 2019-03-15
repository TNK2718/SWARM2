using GeneticAlgorithm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Board;

namespace GeneticAlgorithm {
    // 遺伝的アルゴリズムの実装。
    // このクラスのメソッドを呼び出すことで、学習させる。
    public class CellAutomataGA {
        private const int POPULATION = 100;
        private const int ELITE_POPULATION = 1;
        private const int CHROMOSOME_SIZE = 2000;
        private const int MOORE_NEIGHBORHOOD = 9;
        private const int CELL_STATE_SIZE = 8;
        private const double CROSSOVER_RATE = 1;
        private const double MUTATION_RATE = 0.2;
        private const double MUTATION_RANGE = 0.05;
        private const double STABILITY = 1;
        private const int EPISODES_FOR_EVALATION = 50;
        private const int INITIAL_RESOURCES = 100;
        private int chromosomeMaxNumber;
        private int boardSize;

        private IntArrayChromosomes intArrayChromosomes;

        public int[] rulesForEvalate;

        public CellAutomataGA(int initBoardSize) {
            boardSize = initBoardSize;
            chromosomeMaxNumber = FastPower(CELL_STATE_SIZE, MOORE_NEIGHBORHOOD + 1) - 1;
            intArrayChromosomes = new IntArrayChromosomes(POPULATION, CHROMOSOME_SIZE, chromosomeMaxNumber);
            SetUpChromosomes();

            rulesForEvalate = new int[5]{
                ConvertToConditionNo(new int[] { 1, 0, 1, 0, 0, 0, 0, 0, 0, 0 }),
                ConvertToConditionNo(new int[] { 1, 1, 0, 0, 0, 0, 0, 0, 0, 0 }),
                ConvertToConditionNo(new int[] { 1, 0, 0, 0, 0, 0, 0, 1, 0, 0 }),
                ConvertToConditionNo(new int[] { 1, 0, 0, 0, 0, 0, 1, 0, 0, 0 }),
                ConvertToConditionNo(new int[] { 1, 0, 0, 0, 0, 0, 0, 0, 1, 0 }),
            };
        }

        // 染色体を初期状態にセットする
        public void SetUpChromosomes() {
            System.Random random = new System.Random();
            for (int i = 0; i < POPULATION; i++) {
                for (int j = 0; j < CHROMOSOME_SIZE / 20; j++) {
                    int rule = 0;
                    for (int k = 0; k < MOORE_NEIGHBORHOOD; k++) {
                        rule += random.Next(0, 2) * FastPower(CELL_STATE_SIZE, k);
                    }
                    rule += random.Next(0, CELL_STATE_SIZE) * FastPower(CELL_STATE_SIZE, MOORE_NEIGHBORHOOD);
                    intArrayChromosomes.SetChromosome(i, j, rule);
                }
            }
        }

        // 次の世代に以上する処理
        public void NextGeneration() {
            Evaluate();
            Crossover(intArrayChromosomes, CROSSOVER_RATE);
            Mutation(intArrayChromosomes, MUTATION_RATE, chromosomeMaxNumber);
            //Reproduce_Ranking(intArrayChromosomes);
            Reproduce_Roulette(intArrayChromosomes);
        }

        // 交叉
        public void Crossover(IntArrayChromosomes intChromosomes, double crossoverrate) {
            System.Random random = new System.Random();
            for (int i = 0; i < POPULATION - 1; i++) {
                for (int j = i + 1; j < POPULATION; j++) {
                    if (random.NextDouble() <= crossoverrate) {
                        intChromosomes.CrossOverChromosomes(
                            i, j, random.Next(0, CHROMOSOME_SIZE), random.Next(0, CHROMOSOME_SIZE));
                    }
                }
            }
        }

        // 評価をする
        public void Evaluate() {
            for (int i = 0; i < POPULATION; i++) {
                CellAutomataGame cellAutomataGame = new CellAutomataGame(
                    intArrayChromosomes.ReadChromosomeAsRule(i), rulesForEvalate, boardSize, INITIAL_RESOURCES);
                cellAutomataGame.InitializeBoards();
                for (int n = 0; n < EPISODES_FOR_EVALATION; n++) {
                    cellAutomataGame.UpdateGameBoard();
                }
                intArrayChromosomes.SetScore(i, EvaluateFunction(cellAutomataGame));
            }
        }

        // 評価関数
        private double EvaluateFunction(CellAutomataGame cellAutomataGame) {
            double score1 = 0;
            double score2 = 0;
            for (int x = 0; x < cellAutomataGame.boardSize; x++) {
                for (int y = 0; y < cellAutomataGame.boardSize; y++) {
                    if (cellAutomataGame.myCellGrid.GetCell(true, x, y) != 0) score1++;
                    score2 += cellAutomataGame.enemyCellGrid.GetCell(true, x, y);
                }
            }
            return Math.Abs(score1 / Math.Pow(cellAutomataGame.boardSize, 2) / score2);
        }

        // 突然変異
        public void Mutation(IntArrayChromosomes intChromosomes, double mutationrate, int chromosomeMaxNumber) {
            System.Random random = new System.Random();
            for (int i = 0; i < POPULATION; i++) {
                double rndnum = random.NextDouble();
                if (rndnum <= mutationrate) {
                    for (int j = 0; j < CHROMOSOME_SIZE * MUTATION_RANGE; j++) {
                        intChromosomes.SetChromosome(i, random.Next(0, CHROMOSOME_SIZE), random.Next(0, chromosomeMaxNumber));
                    }
                }
            }
        }

        // 選択・淘汰
        public void Reproduce_Ranking(IntArrayChromosomes _intArrayChromosomes) {
            _intArrayChromosomes.SortChromosomes();
            var random = new System.Random();
            int population = _intArrayChromosomes.GetPopulation();
            for (int i = 0; i < population - POPULATION; i++) {
                int individual = (int)(DensityFunction(random) * _intArrayChromosomes.GetPopulation()) + ELITE_POPULATION;
                if (individual >= POPULATION) individual = POPULATION - 1;
                _intArrayChromosomes.RemoveChromosome(individual);
            }
        }

        // 確率密度関数を吐く（逆関数法）
        private double DensityFunction(System.Random random) {
            return Math.Pow(3.0 * random.NextDouble(), 1.0 / 3.0);
        }

        // ルーレット選択
        public void Reproduce_Roulette(IntArrayChromosomes _intarraychromosomes) {
            _intarraychromosomes.SortChromosomes();
            var random = new System.Random();
            double scoreSum = 0;
            for (int i = 0; i < _intarraychromosomes.GetPopulation(); i++) scoreSum += _intarraychromosomes.ReadScore(i);
            for (int i = ELITE_POPULATION; i < _intarraychromosomes.GetPopulation() &&
                _intarraychromosomes.GetPopulation() > POPULATION; i++) {
                if (random.NextDouble() <= 1 - _intarraychromosomes.ReadScore(i) / scoreSum) {
                    _intarraychromosomes.RemoveChromosome(i);
                }
            }
            int population = _intarraychromosomes.GetPopulation();
            for (int i = 0; i < population - POPULATION; i++) {
                _intarraychromosomes.RemoveChromosome(random.Next(ELITE_POPULATION, _intarraychromosomes.GetPopulation()));
            }
        }

        // int型の累乗計算
        public int FastPower(int _base, int exponent) {
            if (exponent == 0) return 1;
            else if (exponent == 1) return _base;
            else if (exponent % 2 == 0) {
                int tmp = FastPower(_base, exponent / 2);
                return tmp * tmp;
            } else {
                int tmp = FastPower(_base, (exponent - 1) / 2);
                return tmp * tmp * _base;
            }
        }

        // セルのルールの命令をint型の数に変換する
        private int ConvertToConditionNo(int[] input) {
            int returnvalue = 0;
            for (int i = 0; i <= MOORE_NEIGHBORHOOD; i++) {
                returnvalue += FastPower(CELL_STATE_SIZE, i) * input[MOORE_NEIGHBORHOOD - i];
            }
            return returnvalue;
        }

        // Scoreをデバッグに表示
        public void ShowScores() {
            // Debug.Log("Scores:");
            for (int i = 0; i < POPULATION; i++) {
                // Debug.Log(intArrayChromosomes.ReadScore(i));
            }
        }

        // 最もスコアが高いルールを返す
        public int[] EliteRule() {
            return intArrayChromosomes.ReadChromosomeAsRule(0);
        }
    }
}
