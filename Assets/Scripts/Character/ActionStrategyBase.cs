using UnityEngine;

namespace Character {
    // �v���C���[�L�����N�^�[�̏�ԁi�ҋ@�����A�X�L���𔭓����悤�Ƃ��Ă��邩�Ȃǁj��\�����ۃN���X
    public abstract class ActionStrategyBase : MonoBehaviour{
        protected PlayerCharacter playerCharacter;
        public abstract void ReceiveInput();

        private void Start() {
            playerCharacter = GetComponent<PlayerCharacter>();
        }
    }
}
