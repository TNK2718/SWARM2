using UnityEngine;

namespace Character {
    // プレイヤーキャラクターの状態（待機中か、スキルを発動しようとしているかなど）を表す抽象クラス
    public abstract class ActionStrategyBase : MonoBehaviour{
        protected PlayerCharacter playerCharacter;
        public abstract void ReceiveInput();

        private void Start() {
            playerCharacter = GetComponent<PlayerCharacter>();
        }
    }
}
