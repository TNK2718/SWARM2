using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI {
    public class MouseHandler : MonoBehaviour {
        public Rect hitArea;
        private bool isMouseInsideLastFrame = false;
        // TODO: イベントを使うべきなのか？
        private List<Action> pointerEnterListeners = new List<Action>();
        private List<Action> pointerExitListeners = new List<Action>();
        private List<Action> pointerDownListeners = new List<Action>();
        private List<Action> pointerUpListeners = new List<Action>();
        private List<Action> pointerClickListeners = new List<Action>();
        private List<Action> whilePointerDownListeners = new List<Action>();
        private List<Action> whilePointerUpListeners = new List<Action>();
        private List<Action> whilePointerInsideListeners = new List<Action>();
        private List<Action> whilePointerOutsideListeners = new List<Action>();

        public void Update() {
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var isMouseInside =
                mousePos.x >= hitArea.x && mousePos.x <= hitArea.x + hitArea.width &&
                mousePos.y >= hitArea.y && mousePos.y <= hitArea.y + hitArea.height;

            if (isMouseInside) {
                whilePointerInsideListeners.ForEach((fn) => fn());
                if (Input.GetMouseButtonDown(0)) {
                    pointerDownListeners.ForEach((fn) => fn());
                }
                if (Input.GetMouseButtonUp(0)) {
                    pointerUpListeners.ForEach((fn) => fn());
                    pointerClickListeners.ForEach((fn) => fn());  // TODO
                }
                if (Input.GetMouseButton(0)) {
                    whilePointerDownListeners.ForEach((fn) => fn());
                } else {
                    whilePointerUpListeners.ForEach((fn) => fn());
                }
            } else {
                whilePointerOutsideListeners.ForEach((fn) => fn());
            }
            if (!isMouseInsideLastFrame && isMouseInside) {
                pointerEnterListeners.ForEach((fn) => fn());
            }
            if (isMouseInsideLastFrame && !isMouseInside) {
                pointerExitListeners.ForEach((fn) => fn());
            }

            isMouseInsideLastFrame = isMouseInside;
        }

        public void onPointerEnter(Action callback) {
            pointerEnterListeners.Add(callback); }
        public void onPointerExit(Action callback) {
            pointerExitListeners.Add(callback); }
        public void onPointerDown(Action callback) {
            pointerDownListeners.Add(callback); }
        public void onPointerUp(Action callback) {
            pointerUpListeners.Add(callback); }
        public void onPointerClick(Action callback) {
            pointerClickListeners.Add(callback); }
        public void whilePointerDown(Action callback) {
            whilePointerDownListeners.Add(callback); }
        public void whilePointerUp(Action callback) {
            whilePointerUpListeners.Add(callback); }
        public void whilePointerInside(Action callback) {
            whilePointerInsideListeners.Add(callback); }
        public void whilePointerOutside(Action callback) {
            whilePointerOutsideListeners.Add(callback); }
    }
}
