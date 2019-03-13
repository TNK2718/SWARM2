using System;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GeneticAlgorithm;
using Board;
using Visual;

public class SimulationScene : MonoBehaviour {
    public GameObject star;
    public Texture2D textureBase;
    public GameObject blackCube;
    public GameObject characterSprite;
    public GameObject sphere;

    private bool isSimulating = false;
    private CellAutomataGA cellAutomataGA;
    private CellAutomataGame cellAutomataGame;
    private CellGridView cellGridView;
    private CharacterView characterView;
    private Vector2 prevFrameMousePosition;
    private Character.PlayerCharacter characterModel;

    private readonly int NUM_LEARNING_ITERATION = 1;
    private readonly int BOARD_SIZE = 8;
    private readonly int BOARD_UPDATE_INTERVAL = 30;
    private readonly int INITIAL_RESOURCE = 100;

    // ゲームのエントリーポイント
    void Start() {
        Application.targetFrameRate = 30;
        Physics.gravity = new Vector3(0, 0, 1f);  // 重力小さめ
        prevFrameMousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        characterModel = new Character.PlayerCharacter(BOARD_SIZE);
        InitUnityGameObjects();
        StartLearning();
    }

    void Update() {
        if (!isSimulating) {
            // 学習が終わるまではシミュレーションを開始しない
            return;
        }
        if (cellAutomataGame == null) {
            cellAutomataGame = new CellAutomataGame(
                cellAutomataGA.EliteRule(), cellAutomataGA.rulesForEvalate, BOARD_SIZE, INITIAL_RESOURCE);

            cellAutomataGame.InitializeBoards();
        }

        var mouseHoveredCell = cellGridView.GetMouseHoveredCell();
        UpdateSelectionSphere(mouseHoveredCell);
        characterModel.Update(mouseHoveredCell);
        characterSprite.transform.position =
            CellGridView.boardPosTo3DPos(BOARD_SIZE, characterModel.GetPosition().x, characterModel.GetPosition().y) +
            new Vector3(0, 0, -0.7f);

        if (Time.frameCount % BOARD_UPDATE_INTERVAL == 0) {
            cellAutomataGame.UpdateGameBoard();
        }
        cellGridView.Update(cellAutomataGame.getMyBoardData(), cellAutomataGame.getEnemyBoardData());

        // ドラッグで画面スクロール
        if (Input.GetMouseButton(0)) {
            Camera.main.transform.position += new Vector3(
                Input.mousePosition.x - prevFrameMousePosition.x,
                Input.mousePosition.y - prevFrameMousePosition.y,
                0) * 0.03f;
        }
        prevFrameMousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
    }

    private void InitUnityGameObjects() {
        // カメラ設定
        var camera = GetComponent<Camera>();
        // camera.orthographicSize = 15;
        transform.position = new Vector3(16, -16, -8);
        transform.localRotation = Quaternion.LookRotation(
            new Vector3(0, 0, 9) - transform.position, new Vector3(0, 0, -1));

        cellGridView = new CellGridView(Instantiate, BOARD_SIZE, blackCube, star);
        characterView = new CharacterView(characterSprite, 0, 0, BOARD_SIZE);

        // 元画像を隠す
        star.SetActive(false);
    }

    // 学習開始
    private void StartLearning() {
        cellAutomataGA = new CellAutomataGA(BOARD_SIZE);

        Task.Run(() => {
            for (int i = 0; i < NUM_LEARNING_ITERATION; i++) {
                cellAutomataGA.NextGeneration();
                Debug.Log("Episode" + i);
                cellAutomataGA.ShowScores();
            }
            isSimulating = true;
        });
        Debug.Log("start!");
    }

    private void UpdateSelectionSphere((bool found, int x, int y) mouseHoveredCell) {
        if (mouseHoveredCell.found) {
            sphere.SetActive(true);
            sphere.transform.position = CellGridView.boardPosTo3DPos(
                BOARD_SIZE, mouseHoveredCell.x, mouseHoveredCell.y) + new Vector3(0, 0, -0.2f);
        } else {
            sphere.SetActive(false);
        }
    }
}
