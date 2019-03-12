using System;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GeneticAlgorithm;
using Board;
using Visual;

public class Program : MonoBehaviour {
    public GameObject star;
    public Texture2D textureBase;
    public GameObject blackCube;
    public GameObject character;

    private List<List<GameObject>> cellSprites;
    private bool isSimulating = false;
    private CellAutomataGA cellAutomataGA;
    private CellAutomataGame cellAutomataGame;
    [SerializeField] private int INITIAL_RESOURCE;
    private CellGridView cellGridView;
    private CharacterView characterView;

    private readonly int NUM_LEARNING_ITERATION = 1;
    private readonly int BOARD_SIZE = 10;

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

        Physics.gravity = new Vector3(0, 0, 3f);  // 重力小さめ
        initUnityGameObjects();
        StartLearning();
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

        cellGridView.update(cellAutomataGame.getMyBoardData(), cellAutomataGame.getEnemyBoardData());
        cellAutomataGame.UpdateGameBoard();

        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        var mouseHovered = new RaycastHit();
        if (Physics.Raycast(ray, out mouseHovered, 300f)) {
            Debug.Log("RAYCAST  OK");
            // mouseHovered.collider.gameObject.GetComponent<SpriteRenderer>()
            //     .color = new Color(1f, 1f, 0, 0.5f);
        }

        
    }
}
