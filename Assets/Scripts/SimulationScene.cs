using Board;
using GeneticAlgorithm;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
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
    private Character.PlayerCharacter characterModel;
    private CharacterView characterView;
    private Vector2 prevFrameMousePosition;

    private readonly int NUM_LEARNING_ITERATION = 10;
    private readonly int BOARD_SIZE = 8;
    private readonly int BOARD_UPDATE_INTERVAL = 30;
    private readonly int INITIAL_RESOURCE = 100;

    // ゲームのエントリーポイント
    void Start() {
        Application.targetFrameRate = 30;
        Physics.gravity = new Vector3(0, 0, 1f);  // 重力小さめ
        prevFrameMousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        characterModel = new Character.PlayerCharacter(BOARD_SIZE);
        cellAutomataGA = new CellAutomataGA(BOARD_SIZE);
        InitUnityGameObjects();
        SetButtonEventListeners();
    }

    void Update() {
        if (!isSimulating) {
            return;
        }

        if (Time.frameCount % BOARD_UPDATE_INTERVAL == 0) {
            cellAutomataGame.UpdateGameBoard();
        }

        var mouseHoveredCell = cellGridView.GetMouseHoveredCell();
        characterModel.Update(mouseHoveredCell);

        UpdateGameObjects(mouseHoveredCell);
        HandleMouseInput();
    }

    private void StartSimulation() {
        cellAutomataGame = new CellAutomataGame(
            cellAutomataGA.EliteRule(), cellAutomataGA.rulesForEvalate, BOARD_SIZE, INITIAL_RESOURCE);

        cellAutomataGame.InitializeBoards();
        isSimulating = true;
    }

    private void HandleMouseInput() {
        // スクリーンをドラッグしてスクロール
        if (Input.GetMouseButton(1)) {
            Camera.main.transform.position -= new Vector3(
                Input.mousePosition.x - prevFrameMousePosition.x,
                Input.mousePosition.y - prevFrameMousePosition.y,
                0) * 0.03f;
        }
        prevFrameMousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
    }

    private void UpdateGameObjects((bool found, int x, int y) mouseHoveredCell) {
        // キャラクター
        characterSprite.transform.position =
            CellGridView.boardPosTo3DPos(BOARD_SIZE, characterModel.GetPosition().x, characterModel.GetPosition().y) +
            new Vector3(0, 0, -0.7f);

        // ボード
        cellGridView.UpdateGridView(cellAutomataGame.GetMyBoardData(), cellAutomataGame.GetEnemyBoardData());

        // マウスの選択範囲のハイライト
        UpdateSelectionSphere(mouseHoveredCell);
    }

    private void SetButtonEventListeners() {
        // [Learn] を押すと、学習が開始される。学習の状態はコンソールに出力される。
        // [Simulate] を押すと、シミュレーションが開始される。今は学習と同時にシミュレーションできるが、バグるかも。
        // [Save] を押すと、cellAutomataGAが持つChromosomeをPlayerPrefsに保存する。
        // [Load] を押すと、PlayerPrefsからChromosomeを読み込む。
        GameObject.Find("LearnButton")
            .GetComponent<Button>()
            .onClick.AddListener(StartLearning);
        GameObject.Find("SimulateButton")
            .GetComponent<Button>()
            .onClick.AddListener(StartSimulation);
        GameObject.Find("LoadButton")
            .GetComponent<Button>()
            .onClick.AddListener(LoadChromosomes);
        GameObject.Find("SaveButton")
            .GetComponent<Button>()
            .onClick.AddListener(SaveChromosomes);
    }

    private void LoadChromosomes() {
        var saveDataStr = PlayerPrefs.GetString("Chromosomes", "");
        Debug.Assert(saveDataStr != "");
        var saveDataArray = new List<int[]>();
        foreach (var line in saveDataStr.Split('\n')) {
            if (line != "") {
                saveDataArray.Add(Array.ConvertAll(line.Split(','), (s) => int.Parse(s)));
            }
        }
        cellAutomataGA.ImportChromosomes(saveDataArray);
    }

    private void SaveChromosomes() {
        int i = 0;
        var saveDataStr = new System.Text.StringBuilder();
        cellAutomataGA.ExportChromosomes().ForEach((chromosome) => {
            saveDataStr.Append(String.Join(",", chromosome));
            saveDataStr.Append("\n");
        });
        PlayerPrefs.SetString("Chromosomes", saveDataStr.ToString());
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
        Task.Run(() => {
            for (int i = 0; i < NUM_LEARNING_ITERATION; i++) {
                cellAutomataGA.NextGeneration();
                Debug.Log("Episode" + i);
                cellAutomataGA.ShowScores();
            }
            Debug.Log("finish!");
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
