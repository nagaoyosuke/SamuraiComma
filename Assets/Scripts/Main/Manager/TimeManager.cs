using System.Collections;
using UnityEngine;
using UnityEngine.UI;

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

        /// <summary>
        /// 合図が出るまでの制限時間。サーバーからもらう。
        /// </summary>
        [SerializeField] private float _singalTimeLimit = 10.00f;
        /// <summary>
        /// 斬るまでの制限時間。時間切れだと負け。
        /// </summary>
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
                             .Subscribe(_ =>
            {
                _singalTimeLimit = WSManager.giveInit.Value.startTime;
                StartCoroutine(CountCroutine(_signalTimer));
            });

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
                var battleJson = new JsonManager.Send.BattleJson(attackTime: 5.00f);
                WSManager.Send(battleJson.ToJson());
            }
        }
    }
}


