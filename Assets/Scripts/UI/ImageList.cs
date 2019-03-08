using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// UnityのSpriteの画像を切り替えるためのMonoBehaviour
public class ImageList : MonoBehaviour {
    public List<Sprite> sprites;

    void Start() {
    }

    void Update() {
    }

    public void changeImage(int index) {
        if (0 <= index && index < sprites.Count) {
            GetComponent<SpriteRenderer>().sprite = sprites[index];
        } else {
            throw new ArgumentOutOfRangeException();
        }
    }
}
