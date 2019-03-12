using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Visual {
    // キャラクターの描画を行うクラス
    public class CharacterView {
        private GameObject sprite;

        public CharacterView(GameObject sprite, int x, int y, int boardSize) {
            this.sprite = sprite;
            sprite.transform.position =
                CellGridView.boardPosTo3DPos(boardSize, x, y) +
                new Vector3(0, 0, -1);
            var cameraZProjectionPos = Camera.main.transform.position;
            cameraZProjectionPos = new Vector3(
                cameraZProjectionPos.x, cameraZProjectionPos.y, 0);
            sprite.transform.rotation = Quaternion.LookRotation(cameraZProjectionPos - sprite.transform.position, new Vector3(0, 0, -1));
        }
    }
}