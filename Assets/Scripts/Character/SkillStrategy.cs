using UnityEngine;

namespace Character {
    // �v���C���[���X�L���𔭓����悤�Ƃ��Ă����ԁB�X�L���𔭓�����Ώۂ̑I���Ȃǂ�����
    public class SkillStrategy : ActionStrategyBase {
        public override void ReceiveInput() {
            if (Input.GetMouseButtonUp(1)) {
                playerCharacter.UseSkill(Camera.main.ScreenToWorldPoint(Input.mousePosition)); // TODO : ���ꂾ�ƃo�O�邩��
            }
        }
    }
}
