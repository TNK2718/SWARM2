using UnityEngine;

namespace Character {
    // �X�L���𔭓����ĂȂ���Ԃ�\���B�X�L�������{�^���������ꂽ��skillstrategy�Ɉڍs
    public class WaitStrategy : ActionStrategyBase {
        public override void ReceiveInput() {
            if (Input.GetMouseButtonUp(1)) {
                playerCharacter.Destination = Camera.main.ScreenToWorldPoint(Input.mousePosition); // TODO : ���ꂾ�ƃo�O�邩��
            }
            // TODO : �J�������h���b�O�ňړ�
        }
    }
}
