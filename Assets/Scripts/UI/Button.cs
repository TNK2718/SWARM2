using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI {
    // 新規UnityObjectを生成して、ボタンを表示します。
    // TODO: UnityEngine.UI.Button と名前が被るから名前を考える。
    public class UIButton {
        private GameObject sprite;

        // nameはデバッグ用の名前
        public UIButton(string name, Rect rect, Texture2D texture) {
            sprite = new GameObject(name);

            // SpriteRenderer
            sprite.AddComponent<SpriteRenderer>();
            sprite.GetComponent<SpriteRenderer>().sprite =
                Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0, 0), 100);

            // RectTransform
            sprite.AddComponent<RectTransform>();
            sprite.GetComponent<RectTransform>().position =
                new Vector3(rect.x, rect.y, 10f);
            sprite.GetComponent<RectTransform>().sizeDelta =
                new Vector2(rect.width, rect.height);

            // EventTrigger
            var trigger = sprite.AddComponent<EventTrigger>();
            trigger.triggers = new List<EventTrigger.Entry>();

            var entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerEnter;
            entry.callback.AddListener((data) => onPointerEnter(data));
            trigger.triggers.Add(entry);
        }

        public void onPointerEnter(BaseEventData data) {
            Debug.Log("Mouse Enter!!!!!!!!");
        }
    }
}