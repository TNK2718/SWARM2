using UnityEngine;

namespace Character {
    // スキルを発動してない状態を表す。スキル発動ボタンが押されたらskillstrategyに移行
    public class WaitStrategy : ActionStrategyBase {
        public override void ReceiveInput() {
            if (Input.GetMouseButtonUp(1)) {
                playerCharacter.Destination = Camera.main.ScreenToWorldPoint(Input.mousePosition); // TODO : これだとバグるかも
            }
            // TODO : カメラをドラッグで移動
        }
    }
}
