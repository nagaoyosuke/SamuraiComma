using UnityEngine;
using UnityEngine.UI;

using SamuraiComma.Main.Inputs;
using SamuraiComma.Main.Manager;
using UniRx;
using UniRx.Triggers;
using Zenject;

namespace SamuraiComma.Main.Player
{
    /// <summary>
    /// 2019/11/02 toyoda
    /// プレイヤーの居合に関する処理。
    /// </summary>

    public class PlayerQuickDrawer : MonoBehaviour
    {

        [SerializeField] private KeyboardInputProvider _inputProvider;
        [SerializeField] private PlayerState _playerState;
        [Inject] private ScreenFader _screenFader;
        [SerializeField] private readonly int _otetsukiTime = 2;
        [Inject] private GameStateManager _gameStateManager;
        [Inject] private TimeManager _timeManager;

        private BoolReactiveProperty _canBattle = new BoolReactiveProperty(false);
        public IReadOnlyReactiveProperty<bool> canBattle => _canBattle;
        public Text text;

        //仮
        [SerializeField] private TempBattle temp;

        private void Start()
        {
            this.UpdateAsObservable()
                .Where(_ =>
                       (_gameStateManager.CurrentGameState.Value == GameState.WaitingSignal)
                       && (_inputProvider.enterTrigger.Value) 
                       && (_playerState.canQuickDraw.Value))
                .Subscribe(_ => QuickDraw());
        }

        private void QuickDraw()
        {
            _playerState.canQuickDraw.Value = false;

            if (_timeManager.signalTimer.Value <= 0){
                Sound.PlaySe("syakin");
                _screenFader.isFadeOutTranslucent = true;
                temp.setsunaButton.SetActive(true);
                text.enabled = true;
                _playerState.canBattle.Value = true;

            } else {
                StartCoroutine(DelayClass.DelayCoroutin(60*_otetsukiTime, () => _playerState.canQuickDraw.Value = true));
            }
        }
    }
}