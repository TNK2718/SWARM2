using System;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI {
    public class CustomCursor : MonoBehaviour {
        public GameObject cursorSprite;

        void Start() {
            Cursor.visible = false;
        }

        void Update() {
            Cursor.visible = false;  // マウスが画面外に出るとvisibleがtrueになるため毎フレームセット
            // var newCursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // newCursorPos.z = 0f;
            cursorSprite.transform.position = Input.mousePosition;

            // 回転させる
            cursorSprite.transform.rotation *= Quaternion.Euler(0, 0, 10);
        }
    }
}