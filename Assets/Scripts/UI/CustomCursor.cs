using System;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI {
    public class CustomCursor : MonoBehaviour {
        public Texture2D image;
        private GameObject cursorSprite;

        void Start() {
            if (image == null) {
                return;
            }

            Cursor.visible = false;
            cursorSprite = new GameObject("cursorSprite");
            cursorSprite.AddComponent<SpriteRenderer>();
            cursorSprite.GetComponent<SpriteRenderer>().sprite =
                Sprite.Create(
                    image,
                    new Rect(0, 0, image.width, image.height),
                    new Vector2(0.5f, 0.5f),
                    100f);
        }

        void Update() {
            if (image == null) {
                return;
            }

            Cursor.visible = false;  // マウスが画面外に出るとvisibleがtrueになるため毎フレームセット
            var newCursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            newCursorPos.z = 1f;
            cursorSprite.transform.position = newCursorPos;

            // 回転させる
            var angle = cursorSprite.transform.eulerAngles;
            angle.z -= 7f;
            cursorSprite.transform.eulerAngles = angle;
        }
    }
}