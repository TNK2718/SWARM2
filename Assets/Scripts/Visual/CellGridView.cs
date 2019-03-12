using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Visual {
    using InstantiateType = Func<GameObject, Vector3, Quaternion, GameObject>;

    // ゲームのボードの描画を行うクラス
    public class CellGridView {
        public static Vector3 boardPosTo3DPos(int boardSize, int x, int y) {
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
                    groundSprites[groundSprites.Count - 1].Add(
                        Instantiate(star, boardPosTo3DPos(boardSize, x, y), Quaternion.identity));
                }
            }
        }

        public void update(List<List<bool>> myBoardData, List<List<bool>> enemyBoardData) {
            for (int y = 0; y < myBoardData.Count; y++) {
                for (int x = 0; x < myBoardData[0].Count; x++) {
                    // 地面を描画
                    groundSprites[y][x].SetActive(true);
                    if (myBoardData[y][x]) {
                        groundSprites[y][x].GetComponent<ImageList>().changeImage(0);
                    } else if (enemyBoardData[y][x]) {
                        groundSprites[y][x].GetComponent<ImageList>().changeImage(1);
                    } else {
                        groundSprites[y][x].GetComponent<ImageList>().changeImage(2);
                    }
                    // 黒いブロックを生成
                    if (myBoardData[y][x] || enemyBoardData[y][x]) {
                        var newBlackCube = Instantiate(
                            blackCube,
                            boardPosTo3DPos(boardSize, x, y) + new Vector3(UnityEngine.Random.Range(-0.3f, 0.3f), UnityEngine.Random.Range(-0.3f, 0.3f), -1),
                            Quaternion.identity);
                        newBlackCube.GetComponent<Rigidbody>().AddForce(new Vector3(
                            UnityEngine.Random.Range(-1f, 1f),
                            UnityEngine.Random.Range(-1f, 1f),
                            -2f), ForceMode.Impulse);
                        newBlackCube.AddComponent<LifeTime>().lifeTime = 50;
                    }
                }
            }
        }
    }
}