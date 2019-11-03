using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Zenject;
using SamuraiComma.Main.Manager;

namespace SamuraiComma.Main.Manager
{
    /// <summary>
    /// 2019/11/03 toyoda
    /// 軌跡を斬るまでの制限時間の処理。
    /// </summary>

    public class TrajectoryTimeLimitCounter : MonoBehaviour
    {
        [Inject] private GameStateManager _gameStateManager;
        [SerializeField] private readonly float _timeLimit = 5.00f;
        [SerializeField] private FloatReactiveProperty _signalTimer;

        public IReadOnlyReactiveProperty<float> signalTimer => _signalTimer;

        private void Start()
        {
            _gameStateManager.CurrentGameState
                             .FirstOrDefault(x => x == GameState.Battle)
                             .Subscribe(_ =>StartCoroutine(CountCroutine()));
        }

        private IEnumerator CountCroutine()
        {
            _signalTimer.Value = _timeLimit;

            while(_signalTimer.Value >= 0)
            {
                _signalTimer.Value -= 0.01f;
                yield return new WaitForSecondsRealtime(0.01f);
            }
        }
    }
}


