﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Zenject;
using SamuraiComma.Main.Manager;

namespace SamuraiComma.Main.UI
{
    /// <summary>
    /// 2019/11/03 toyoda
    /// 画面上に軌跡を斬るまでの制限時間を表示する。
    /// Battle Canvas
    /// </summary>

    public class DisplayTrajectoryTime : MonoBehaviour
    {

        [Inject] private TrajectoryTimeLimitCounter _timeCounter;
        [Inject] private GameStateManager _gameStateManager;
        [SerializeField] private Text _timeLimitText;

        private void Start()
        {

            _gameStateManager.CurrentGameState
                             .SkipLatestValueOnSubscribe()
                             .FirstOrDefault(x => x == GameState.Battle)
                             .Subscribe(_ => _timeLimitText.enabled = true); 

            _timeCounter.signalTimer
                        .DistinctUntilChanged()
                        .Subscribe(time => _timeLimitText.text = time.ToString("0.00"));

            _timeCounter.signalTimer
                        .SkipLatestValueOnSubscribe()
                        .DistinctUntilChanged()
                        .Where(x => x <= 0)
                        .Subscribe(_ => _timeLimitText.enabled = false);
        }

    }
}


