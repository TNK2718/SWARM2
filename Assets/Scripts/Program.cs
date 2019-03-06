﻿using System;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GeneticAlgorithm;

public class Program : MonoBehaviour {
    public GameObject star;
    private List<List<GameObject>> cellSprites;
    private bool isSimulating = false;
    private CellAutomataGA cellAutomataGA;
    private CellAutomataGame cellAutomataGame;

    private readonly int NUM_LEARNING_ITERATION = 2;
    private readonly int BOARD_SIZE = 10;

    void Start() {
        cellSprites = initGameObjects();
        StartLearning();
    }

    private List<List<GameObject>> initGameObjects() {
        // カメラ設定
        var camera = GetComponent<Camera>();
        camera.orthographicSize = 15;
        camera.transform.position.Set(0, 0, 0);

        // Spriteを並べる
        var sprites = new List<List<GameObject>>();
        Debug.Log(star.transform.position.z);
        for (float y = -BOARD_SIZE; y < BOARD_SIZE; y += 2) {
            sprites.Add(new List<GameObject>());
            for (float x = -BOARD_SIZE; x < BOARD_SIZE; x += 2) {
                sprites[sprites.Count - 1].Add(
                    Instantiate(star, new Vector3(x, y, 10), Quaternion.identity));
            }
        }

        // 元画像を隠す
        star.SetActive(false);

        return sprites;
    }

    private void StartLearning() {
        cellAutomataGA = new CellAutomataGA(BOARD_SIZE);

        Task.Run(() => {
            for(int i = 0; i < NUM_LEARNING_ITERATION; i++) {
                cellAutomataGA.NextGeneration();
                Debug.Log("Episode" + i);
                cellAutomataGA.ShowScores();
                cellAutomataGA.ShowEliteScores();
            }
            isSimulating = true;
        });
        Debug.Log("start!");
    }

    void Update() {
        if (!isSimulating) {
            return;
        }
        if (cellAutomataGame == null) {
            cellAutomataGame = new CellAutomataGame(
                cellAutomataGA.EliteRule(), cellAutomataGA.rulesForEvalate, BOARD_SIZE);

            cellAutomataGame.InitializeBoards();
        }
        cellAutomataGame.Draw(cellSprites);
        cellAutomataGame.UpdateGameBoard();
    }
}
