﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace SamuraiComma.Main.Player
{
    /// <summary>
    /// 2019/11/02 toyoda
    /// プレイヤーの状態
    /// </summary>

    public class PlayerState : MonoBehaviour
    {
        [SerializeField] public BoolReactiveProperty canQuickDraw = new BoolReactiveProperty(true);
        //public IReadOnlyReactiveProperty<bool> canQuickDraw => _canQuickDraw;
        //[SerializeField] private BoolReactiveProperty _is

    }
}



