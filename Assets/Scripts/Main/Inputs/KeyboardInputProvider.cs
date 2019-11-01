using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace SamuraiComma.Main.Inputs
{
    /// <summary>
    /// 2019/11/01 toyoda
    /// キーボード(デバッグ)用InputProvider
    /// </summary>

    public class KeyboardInputProvider : MonoBehaviour,IInputProvider
    {
        public IReadOnlyReactiveProperty<bool> enterTrigger => _enterTrigger;
        public IReadOnlyReactiveProperty<bool> cancelTrigger => _cancelTrigger;

        private BoolReactiveProperty _enterTrigger = new BoolReactiveProperty();
        private BoolReactiveProperty _cancelTrigger = new BoolReactiveProperty();

        private void Start()
        {
            this.UpdateAsObservable()
                .Subscribe(_ =>
                {
                    _enterTrigger.Value = Input.GetKey(KeyCode.Z);
                    _cancelTrigger.Value = Input.GetKey(KeyCode.X);
                });
        }

    }
}


