﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UniRx;

namespace SamuraiComma.Main.Camera
{
    /// <summary>
    /// 2019/10/30 toyoda
    /// GameState.Finished時に再生されるタイムライン。
    /// GameStateManagerで観察する。
    /// </summary>

    public class ObservableDefeatTimelineTrigger : MonoBehaviour, ITimeControl
    {
        [SerializeField] private BoolReactiveProperty _isFinishedDirection = new BoolReactiveProperty(false);

        public IReadOnlyReactiveProperty<bool> isFinishedDirection => _isFinishedDirection;


        public void OnControlTimeStart()
        {
            _isFinishedDirection.SetValueAndForceNotify(false);
        }

        public void OnControlTimeStop()
        {
            _isFinishedDirection.SetValueAndForceNotify(true);
        }

        public void SetTime(double time)
        {
        }

    }
}