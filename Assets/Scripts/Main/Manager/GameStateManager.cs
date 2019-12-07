using System;
using System.Linq;
using UnityEngine;
using UniRx;
using UniRx.Async;
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
            //サーバーにデータを受信したら(送信はLogin時のプレイヤーIDなどのデータでサーバーサイドがdbから検索する)
            WSManager.giveInit
                     .SkipLatestValueOnSubscribe()
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
                        .DistinctUntilChanged()
                        .Where(_ => CurrentGameState.Value == GameState.Battle)
                        .Subscribe(_ => GameStateFinishedAsync().Forget());


            //アニメーション終了後
            _victoryTimelineTrigger.isLoopingPlayerAnim
                                   .DistinctUntilChanged()
                                   .Where(x => x == true && CurrentGameState.Value == GameState.Finished)
                                   .Subscribe(_ => _gameState.SetValueAndForceNotify(GameState.Result));


        }

        private async UniTaskVoid GameStateFinishedAsync()
        {

            await UniTask.WaitUntil(() => _sendDataStateManager.battleSendState.Value == SendDataState.OnSent);
            await UniTask.WaitWhile(() => _screenFader.isFadeOut);
            await UniTask.Delay(TimeSpan.FromSeconds(0.5f));

            _screenFader.isFadeIn = true;
            _gameState.SetValueAndForceNotify(GameState.Finished);
        }
    }
}