using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Visual {
    using InstantiateType = Func<GameObject, Vector3, Quaternion, GameObject>;

    // ゲームのボードの描画を行うクラス
    public class CellGridView {
        public static Vector3 boardPosTo3DPos(int boardSize, float x, float y) {
            return new Vector3(-boardSize + 2 * x, -boardSize + 2 * y, 10);
        }
    
        private GameObject blackCube;
        private List<List<GameObject>> groundSprites;
        private InstantiateType Instantiate;
        private int boardSize;

        public CellGridView(
            InstantiateType Instantiate, int boardSize, GameObject blackCube, GameObject star) {
            this.Instantiate = Instantiate;
            this.boardSize = boardSize;
            this.blackCube = blackCube;
            groundSprites = new List<List<GameObject>>();
            for (int y = 0; y < boardSize; y++) {
                groundSprites.Add(new List<GameObject>());
                for (int x = 0; x < boardSize; x++) {
                    var sprite = Instantiate(star, boardPosTo3DPos(boardSize, x, y), Quaternion.identity);
                    groundSprites[groundSprites.Count - 1].Add(sprite);
                }
            }
        }

        public void UpdateGridView(List<List<bool>> myBoardData, List<List<bool>> enemyBoardData) {
            for (int y = 0; y < myBoardData.Count; y++) {
                for (int x = 0; x < myBoardData[0].Count; x++) {
                    // 地面を描画
                    groundSprites[y][x].SetActive(true);
                    if (myBoardData[y][x]) {
                        groundSprites[y][x].GetComponent<ImageList>().ChangeImage(0);
                    } else if (enemyBoardData[y][x]) {
                        groundSprites[y][x].GetComponent<ImageList>().ChangeImage(1);
                    } else {
                        groundSprites[y][x].GetComponent<ImageList>().ChangeImage(2);
                    }
                    // 黒いブロックを生成
                    // TODO: 「もぞもぞ動く」
                    if (Time.frameCount % 4 == 0 && (myBoardData[y][x] || enemyBoardData[y][x])) {
                        var newBlackCube = Instantiate(
                            blackCube,
                            boardPosTo3DPos(boardSize, x, y) + new Vector3(UnityEngine.Random.Range(-0.3f, 0.3f), UnityEngine.Random.Range(-0.3f, 0.3f), -1),
                            Quaternion.identity);
                        newBlackCube.GetComponent<Rigidbody>().AddForce(new Vector3(
                            UnityEngine.Random.Range(-0.4f, 0.4f),
                            UnityEngine.Random.Range(-0.4f, 0.4f),
                            -1.3f), ForceMode.Impulse);
                        newBlackCube.AddComponent<LifeTime>().lifeTime = 120;
                    }
                }
            }
        }

        public (bool found, int x, int y) GetMouseHoveredCell() {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            var mouseHovered = new RaycastHit();
            if (Physics.Raycast(ray, out mouseHovered, 300f)) {
                return GetPositionOf(mouseHovered.transform.gameObject);
            }
            return (false, 0, 0);
        }

        public (bool found, int x, int y) GetPositionOf(GameObject cell) {
            for (int y = 0; y < boardSize; y++) {
                for (int x = 0; x < boardSize; x++) {
                    if (groundSprites[y][x] == cell) {
                        return (true, x, y);
                    }
                }
            }
            return (false, 0, 0);
        }
    }
}