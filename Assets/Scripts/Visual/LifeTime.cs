using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Visual {
    public class LifeTime : MonoBehaviour {
        public int lifeTime;
        private int startTime;

        public void Start() {
            startTime = Time.frameCount;
        }

        public void Update() {
            if (lifeTime == 0) {  // 未代入
                return;
            }
            if (Time.frameCount >= lifeTime + startTime) {
                Destroy(this.gameObject);
            }
        }
    }
}
