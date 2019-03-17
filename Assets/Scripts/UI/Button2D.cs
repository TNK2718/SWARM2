using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEditor;

namespace UI {
    // 新規UnityObjectを生成して、ボタンを表示します。
    public class Button2D {
        private Sprite2D sprite;
        private float paperHeight = 0;

        // nameはデバッグ用の名前
        public Button2D(string name, Rect rect, Texture2D textureBase, Action onClick) {
            sprite = new Sprite2D(name, new Vector2(0, 0), 100, 100);

            // // SpriteRenderer
            // // import settings （画像ファイルをクリックしたら出る）での
            // //  Advanced > Read/Write Enabled がONになっている必要がある。
            // var originalTexturePixels = textureBase.GetPixels();
            // var buf = new Color[originalTexturePixels.Length];
            // var newTexture = new Texture2D(textureBase.width, textureBase.height, TextureFormat.RGBA32, false);
            // newTexture.filterMode = FilterMode.Point;
            // Draw(newTexture, buf, textureBase.width, textureBase.height);

            // sprite.AddComponent<SpriteRenderer>().sprite = Sprite.Create(
            //     newTexture,
            //     new Rect(0, 0, textureBase.width, textureBase.height),
            //     new Vector2(0, 0),
            //     1);

            // // RectTransform
            // sprite.transform.localScale = new Vector3(
            //     rect.width / textureBase.width, rect.height / textureBase.height, 1);
            // sprite.transform.position = new Vector3(rect.x, rect.y, 10f);

            // SetMouseHandlers(rect, newTexture, buf, textureBase.width, textureBase.height, onClick);
        }

        // private void SetMouseHandlers(Rect rect, Texture2D newTexture, Color[] buf, int w, int h, Action onClick) {
        //     var mouseHandler = sprite.AddComponent<MouseHandler>();
        //     mouseHandler.hitArea = rect;

        //     mouseHandler.OnPointerClick(onClick);

        //     mouseHandler.WhilePointerInside(() => {
        //         if (paperHeight >= 0.3) {
        //             return;
        //         }
        //         paperHeight += 0.1f;
        //         if (paperHeight >= 0.3) {
        //             paperHeight = 0.3f;
        //         }
        //         Draw(newTexture, buf, w, h);
        //     });

        //     mouseHandler.WhilePointerOutside(() => {
        //         if (paperHeight <= 0) {
        //             return;
        //         }
        //         paperHeight -= 0.1f;
        //         if (paperHeight <= 0) {
        //             paperHeight = 0f;
        //         }
        //         Draw(newTexture, buf, w, h);
        //     });

        //     mouseHandler.WhilePointerDown(() => {
        //         if (paperHeight > 0.6) {
        //             return;
        //         }
        //         paperHeight += 0.1f;
        //         if (paperHeight > 0.6) {
        //             paperHeight = 0.6f;
        //         }
        //         Draw(newTexture, buf, w, h);
        //     });

        //     mouseHandler.WhilePointerUp(() => {
        //         if (paperHeight <= 0.3) {
        //             return;
        //         }
        //         paperHeight -= 0.1f;
        //         if (paperHeight <= 0.3) {
        //             paperHeight = 0.3f;
        //         }
        //         Draw(newTexture, buf, w, h);
        //     });
        // }

        // // とても重いため、頻繁には呼び出さないこと。
        // private void Draw(Texture2D tex, Color[] buf, int w, int h) {
        //     for (int x = 0; x < w; x++) {
        //         for (int y = 0; y < h; y++) {
        //             buf.SetValue(new Color(paperHeight + 0.3f, paperHeight + 0.3f, paperHeight + 0.3f, 1), x + w * y);
        //         }
        //     }
        //     tex.SetPixels(buf);
        //     tex.Apply();
        // }
    }
}
