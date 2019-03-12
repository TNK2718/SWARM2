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
    public GameObject character;
    public GameObject sphere;

    private List<List<GameObject>> cellSprites;
    private bool isSimulating = false;
    private CellAutomataGA cellAutomataGA;
    private CellAutomataGame cellAutomataGame;
    [SerializeField] private int INITIAL_RESOURCE;
    private CellGridView cellGridView;
    private CharacterView characterView;
    private Vector2 prevFrameMousePosition;

    private readonly int NUM_LEARNING_ITERATION = 1;
    private readonly int BOARD_SIZE = 20;
    private readonly int BOARD_UPDATE_INTERVAL = 30;

    // ゲームのエントリーポイント
    void Start() {
        // debug
        new UI.UIButton("SampleButton", new Rect(10.5f, 10f, 6f, 2f), textureBase, () => {
            Debug.Log("Clicked!");
        });
        new UI.UIButton("SampleButton", new Rect(10.5f, 7.9f, 6f, 2f), textureBase, () => {
            Debug.Log("Clicked!");
        });
        new UI.UIButton("SampleButton", new Rect(10.5f, 5.8f, 6f, 2f), textureBase, () => {
            Debug.Log("Clicked!");
        });

        Application.targetFrameRate = 30;
        Physics.gravity = new Vector3(0, 0, 1f);  // 重力小さめ
        prevFrameMousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        initUnityGameObjects();
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

        if (Time.frameCount % BOARD_UPDATE_INTERVAL == 0) {
            cellAutomataGame.UpdateGameBoard();
        }
        cellGridView.update(cellAutomataGame.getMyBoardData(), cellAutomataGame.getEnemyBoardData());

        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        var mouseHovered = new RaycastHit();
        if (Physics.Raycast(ray, out mouseHovered, 300f)) {
            sphere.SetActive(true);
            sphere.transform.position = mouseHovered.transform.position + new Vector3(0, 0, -0.1f);
            // クリックで移動
            // TODO: 操作方法を決める（移動可能な範囲が光って表示される感じにする？）
            if (Input.GetMouseButtonDown(0)) {
                var pos = cellGridView.getPositionOf(mouseHovered.transform.gameObject);
                if (pos.found) {
                    characterView.setPosition(pos.x, pos.y);
                }
            }
        } else {
            sphere.SetActive(false);
        }

        // ドラッグで画面スクロール
        if (Input.GetMouseButton(0)) {
            Camera.main.transform.position += new Vector3(
                Input.mousePosition.x - prevFrameMousePosition.x,
                Input.mousePosition.y - prevFrameMousePosition.y,
                0) * 0.03f;
        }
        prevFrameMousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
    }

    private void initUnityGameObjects() {
        // カメラ設定
        var camera = GetComponent<Camera>();
        // camera.orthographicSize = 15;
        transform.position = new Vector3(16, -16, -8);
        transform.localRotation = Quaternion.LookRotation(
            new Vector3(0, 0, 9) - transform.position, new Vector3(0, 0, -1));

        cellGridView = new CellGridView(Instantiate, BOARD_SIZE, blackCube, star);
        characterView = new CharacterView(character, 0, 0, BOARD_SIZE);

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
}
