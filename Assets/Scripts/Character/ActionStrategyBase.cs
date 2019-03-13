using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Character {
    // �v���C���[�L�����N�^�[�̏�ԁi�ҋ@�����A�X�L���𔭓����悤�Ƃ��Ă��邩�Ȃǁj��\�����ۃN���X
    public abstract class ActionStrategyBase {
        protected int boardSize;

        public ActionStrategyBase(int boardSize) {
            this.boardSize = boardSize;
        }

        // TODO: ��������菭�Ȃ��I�u�W�F�N�g�ւ܂Ƃ߂�
        public abstract void Update(
            CharacterData characterData,
            (bool found, int x, int y) mouseHoveredCell);
    }
}
