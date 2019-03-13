using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Character.Strategy {
    // スキルを発動してない状態を表す。
    // スキル発動ボタンが押されたらskillstrategyに移行
    public class Default : ActionStrategyBase {
        public Default(int boardSize) : base(boardSize) {
        }

        public override void Update(
                CharacterData characterData,
                (bool found, int x, int y) mouseHoveredCell) {

            if (mouseHoveredCell.found && Input.GetMouseButtonDown(0)) {
                // TODO: Destinationに変える
                characterData.Position =
                    Visual.CellGridView.boardPosTo3DPos(boardSize, mouseHoveredCell.x, mouseHoveredCell.y);
            }

            // TODO: これの実装
            // if (Input.GetButtonUp("1")) {
            //     playerCharacter.CurrentSkillId = playerCharacter.SkillList[0];
            //     playerCharacter.SetSkill();
            // }

        }
    }
}
