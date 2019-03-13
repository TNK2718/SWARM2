using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Character {
    // プレイヤーキャラクターの状態（待機中か、スキルを発動しようとしているかなど）を表す抽象クラス
    public abstract class ActionStrategyBase {
        protected int boardSize;

        public ActionStrategyBase(int boardSize) {
            this.boardSize = boardSize;
        }

        // TODO: 引数をより少ないオブジェクトへまとめる
        public abstract void Update(
            CharacterData characterData,
            (bool found, int x, int y) mouseHoveredCell);
    }
}
