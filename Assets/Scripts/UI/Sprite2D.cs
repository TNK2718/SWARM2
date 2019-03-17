using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEditor;

namespace UI {
    // 画面上に2Dの画像（UI）を描画するためのクラスです。
    // アニメーションをここで管理したい。
    public class Sprite2D {
        private GameObject gameObject;

        public Sprite2D(String fileName, Vector2 pos, float width, float height) {
            gameObject = new GameObject();
            // TODO: GameObject.Findはよくなさそう？
            gameObject.transform.parent = GameObject.Find("Canvas").transform;
            // TODO: or var sprite = Sprite.Create(new Texture2D(1000, 1000), new Rect(0, 0, 1000, 1000), Vector2.zero, 100);
            var sprite = Resources.Load<Sprite>(fileName);
            gameObject.AddComponent<SpriteRenderer>()
                .sprite = sprite;
            // TODO: z座標が適当
            gameObject.transform.position = new Vector3(pos.x, pos.y, 1);
            gameObject.transform.localScale = new Vector3(
                width / sprite.texture.width,
                height / sprite.texture.height,
                1f);
        }
    }
}
