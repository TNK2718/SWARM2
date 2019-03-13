using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Character.Strategy {
    // プレイヤーがスキルを発動しようとしている状態。スキルを発動する対象の選択などをする
    public class SelectSkill : ActionStrategyBase {
        public SelectSkill(int boardSize) : base(boardSize) {
        }

        public override void Update(
                CharacterData characterData,
                (bool found, int x, int y) mouseHoveredCell) {
            if (Input.GetMouseButtonUp(1)) {
                // playerCharacter.UseSkill(Camera.main.ScreenToWorldPoint(Input.mousePosition)); // TODO : これだとバグるかも
            }
        }
    }
}
