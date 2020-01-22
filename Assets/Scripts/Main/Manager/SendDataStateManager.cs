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

        public IReadOnlyReactiveProperty<SendDataState> loginSendState => _loginSendState;
        public IReadOnlyReactiveProperty<SendDataState> initSendState => _initSendState;
        public IReadOnlyReactiveProperty<SendDataState> battleSendState => _battleSendState;

        [Inject] private GameStateManager _gameStateManager;

        private void Start()
        {
            WSManager.onSent
                     .Where(_ => _gameStateManager.CurrentGameState.Value == GameState.Initializing)
                     .Subscribe(_ => _initSendState.Value = SendDataState.OnSent);

            WSManager.onSent
                     .Where(_ => _gameStateManager.CurrentGameState.Value == GameState.Battle)
                     .Subscribe(_ => _battleSendState.Value = SendDataState.OnSent);
        }
    }
}
