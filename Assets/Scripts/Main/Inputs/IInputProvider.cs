using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace SamuraiComma.Main.Inputs
{

    /// <summary>
    /// 2019/11/01 toyoda
    /// 入力関係のインターフェース
    /// </summary>

    public interface IInputProvider
    {
        IReadOnlyReactiveProperty<bool> enterTrigger { get; }
        IReadOnlyReactiveProperty<bool> cancelTrigger { get; }

    }
}
