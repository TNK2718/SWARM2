using UnityEngine;

namespace Character {
    // プレイヤーがスキルを発動しようとしている状態。スキルを発動する対象の選択などをする
    public class SkillStrategy : ActionStrategyBase {
        public override void ReceiveInput() {
            if (Input.GetMouseButtonUp(1)) {
                playerCharacter.UseSkill(Camera.main.ScreenToWorldPoint(Input.mousePosition)); // TODO : これだとバグるかも
            }
        }
    }
}
