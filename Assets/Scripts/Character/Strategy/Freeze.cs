using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Character.Strategy {
    // 操作を受け付けない状態
    public class Freeze : ActionStrategyBase {
        public Freeze(int boardSize) : base(boardSize) {
        }

        public override void Update(
                CharacterData characterData,
                (bool found, int x, int y) mouseHoveredCell) {

        }
    }
}
