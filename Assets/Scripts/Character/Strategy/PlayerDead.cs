using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Character.Strategy {
    // 死んだ状態
    public class PlayerDead : ActionStrategyBase {
        public PlayerDead(int boardSize) : base(boardSize) {
        }

        public override void Update(
                CharacterData characterData,
                (bool found, int x, int y) mouseHoveredCell) {

        }
    }
}
