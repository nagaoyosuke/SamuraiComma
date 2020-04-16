using System;
using System.Threading;
using System.Linq;
using UnityEngine;
using UniRx;
using UniRx.Async;
using UniRx.Async.Triggers;
using SamuraiComma.Main.Camera;
using SamuraiComma.Main.Player;
using SamuraiComma.Main.WS;
using Zenject;

namespace SamuraiComma.Main.Manager
{
    /// <summary>
    /// 2019/10/30 toyoda
    /// GameStateの操作はここでのみ行う。
    /// </summary>

    class GameStateManager : MonoBehaviour
    {

        private ReactiveProperty<GameState> _gameState = new ReactiveProperty<GameState>(GameState.Initializing);
        public IReadOnlyReactiveProperty<GameState> CurrentGameState => _gameState;

        [SerializeField] private ObservablePrepareTimelineTrigger _prepareTimelineTrigger;
        [SerializeField] private ObservableFixTimelineTrigger _fixTimelineTrigger;
        [SerializeField] private ObservableVictoryTimelineTrigger _victoryTimelineTrigger;
        [SerializeField] private PlayerState _playerState;

        [Inject] private TimelineSwitcher _timelineSwitcher;
        [Inject] private SendDataStateManager _sendDataStateManager;
        [Inject] private ScreenFader _screenFader;

        private void Start()
        {
            //サーバーからデータを受信したら
            WSManager.giveInit
                     .Where(_ => CurrentGameState.Value == GameState.Initializing)

                     .Subscribe(_ =>
            {
                _screenFader.isFadeIn = true;
                _gameState.SetValueAndForceNotify(GameState.Direction);
            });


            //directionの演出が終わったら
            _prepareTimelineTrigger.isFinishedDirection
                                   .DistinctUntilChanged()
                                   .Where(x => x == true && CurrentGameState.Value == GameState.Direction)
                                   .Subscribe(_ => _gameState.SetValueAndForceNotify(GameState.WaitingSignal));

            //aボタンが押されたら
            _playerState.canBattle
                        .DistinctUntilChanged()
                        .Where(x => x == true && CurrentGameState.Value == GameState.WaitingSignal)
                        .Subscribe(_ => _gameState.SetValueAndForceNotify(GameState.Battle));

            //サーバーからデータを受信したら
            WSManager.giveBattle
                        //.DistinctUntilChanged()
                        .Where(_ => CurrentGameState.Value == GameState.Battle)
                     .Subscribe(_ => GameStateFinishedAsync().Forget());
            //this.GetCancellationTokenOnDestroy()

            //アニメーション終了後
            _victoryTimelineTrigger.isLoopingPlayerAnim
                                   .DistinctUntilChanged()
                                   .Where(x => x == true && CurrentGameState.Value == GameState.Finished)
                                   .Subscribe(_ => _gameState.SetValueAndForceNotify(GameState.Result));


        }

        /// <summary>
        /// バトルデータをサーバーに送信するまで待機し、画面暗転した後、GameStateをFinishedにする。
        /// </summary>
        /// <returns>The state finished async.</returns>
        private async UniTaskVoid GameStateFinishedAsync()//CancellationToken token)
        {
            print("1");
            await UniTask.WaitUntil(() => _sendDataStateManager.battleSendState.Value == SendDataState.OnSent);
            print("2");
            await UniTask.WaitWhile(() => _screenFader.isFadeOut);
            print("3");

            await UniTask.Delay(TimeSpan.FromSeconds(0.5f));
            print("4");

            _screenFader.isFadeIn = true;
            _gameState.SetValueAndForceNotify(GameState.Finished);
        }
    }
}