using UnityEngine;
using UniRx;
using SamuraiComma.Main.WS;
using Zenject;
using System.Linq;

namespace SamuraiComma.Main.Manager
{
    /// <summary>
    /// 2019/11/28 toyoda
    /// SendDataStateの操作はここでのみ行う。
    /// </summary>
    public class SendDataStateManager : MonoBehaviour
    {
        private ReactiveProperty<SendDataState> _loginSendState = new ReactiveProperty<SendDataState>(SendDataState.UnSent);
        private ReactiveProperty<SendDataState> _initSendState = new ReactiveProperty<SendDataState>(SendDataState.UnSent);
        private ReactiveProperty<SendDataState> _battleSendState = new ReactiveProperty<SendDataState>(SendDataState.UnSent);
        private ReactiveProperty<SendDataState> _rematchSendState = new ReactiveProperty<SendDataState>(SendDataState.UnSent);

        /// <summary>
        /// ログイン時の通信状態
        /// </summary>
        /// <value>The state of the login send.</value>
        public IReadOnlyReactiveProperty<SendDataState> loginSendState => _loginSendState;
        /// <summary>
        /// 対戦準備時の通信状態
        /// </summary>
        /// <value>The state of the init send.</value>
        public IReadOnlyReactiveProperty<SendDataState> initSendState => _initSendState;
        /// <summary>
        /// 斬ったタイムを送受信している時の通信状態
        /// </summary>
        /// <value>The state of the battle send.</value>
        public IReadOnlyReactiveProperty<SendDataState> battleSendState => _battleSendState;
        /// <summary>
        /// 再戦を申し込むときの通信状態
        /// </summary>
        /// <value>The state of the rematch send.</value>
        public IReadOnlyReactiveProperty<SendDataState> rematchSendState => _rematchSendState;

        [Inject] private GameStateManager _gameStateManager;

        private void Start()
        {
            WSManager.onSent
                     .Where(_ => _gameStateManager.CurrentGameState.Value == GameState.Initializing)
                     .Subscribe(_ => _initSendState.Value = SendDataState.OnSent);

            WSManager.onSent
                     .Where(_ => _gameStateManager.CurrentGameState.Value == GameState.Battle)
                     .Subscribe(_ => _battleSendState.Value = SendDataState.OnSent);

            WSManager.onSent
                     .Where(_ => _gameStateManager.CurrentGameState.Value == GameState.Result)
                     .Subscribe(_ => _rematchSendState.Value = SendDataState.OnSent);

        }
    }
}