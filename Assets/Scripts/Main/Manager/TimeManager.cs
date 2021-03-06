﻿using System.Collections;
using UnityEngine;
using UniRx;
using Zenject;
using SamuraiComma.Main.Manager;
using SamuraiComma.Main.WS;

namespace SamuraiComma.Main.Manager
{
    /// <summary>
    /// 2019/11/03 toyoda
    /// 時間を管理する。
    /// </summary>

    public class TimeManager : MonoBehaviour
    {
        [Inject] private GameStateManager _gameStateManager;
        [Inject] private SendDataStateManager _sendDataStateManager;

        [SerializeField] private float _singalTimeLimit = 10.00f;         //サーバーからもらう
        [SerializeField] public readonly float _trajectoryTimeLimit = 5.00f;

        [SerializeField] private FloatReactiveProperty _signalTimer;
        [SerializeField] private FloatReactiveProperty _trajectoryTimer;

        public IReadOnlyReactiveProperty<float> signalTimer => _signalTimer;
        public IReadOnlyReactiveProperty<float> trajectoryTimer => _trajectoryTimer;

        private void Start()
        {

            _trajectoryTimer.Value = _trajectoryTimeLimit;
            _signalTimer.Value = _singalTimeLimit;

            _gameStateManager.CurrentGameState
                             .FirstOrDefault(x => x == GameState.WaitingSignal)
                             .Subscribe(_ => StartCoroutine(CountCroutine(_signalTimer)));

            _gameStateManager.CurrentGameState
                             .FirstOrDefault(x => x == GameState.Battle)
                             .Subscribe(_ =>StartCoroutine(CountCroutine(_trajectoryTimer)));

        }

        private IEnumerator CountCroutine(FloatReactiveProperty timer)
        {
            while(timer.Value >= 0)
            {
                timer.Value -= 0.01f;
                yield return new WaitForSecondsRealtime(0.01f);
            }

            if ((_gameStateManager.CurrentGameState.Value == GameState.Battle) && (_sendDataStateManager.battleSendState.Value != SendDataState.OnSent))
            {
                var data = new JsonManager.Send.BattleJson(attackTime: 5.00f);
                WSManager.Send(data.ToJson());
            }
        }
    }
}


