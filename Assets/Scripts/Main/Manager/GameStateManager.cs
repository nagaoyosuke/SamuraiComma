using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using SamuraiComma.Main.Camera;
using SamuraiComma.Main.Player;
using Zenject;
using UnityEngine.Timeline;
using UnityEngine.Assertions;
using System.Linq;

namespace SamuraiComma.Main.Manager
{
    /// <summary>
    /// 2019/10/30 toyoda
    /// GameStateの操作はここでのみ行う。
    /// </summary>

    class GameStateManager : MonoBehaviour
    {

        private ReactiveProperty<GameState> _gameState = new ReactiveProperty<GameState>(GameState.Direction);
        public IReadOnlyReactiveProperty<GameState> CurrentGameState => _gameState;

        [SerializeField] private ObservablePrepareTimelineTrigger _prepareTimelineTrigger;
        [SerializeField] private ObservableFixTimelineTrigger _fixTimelineTrigger;
        [SerializeField] private ObservableVictoryTimelineTrigger _victoryTimelineTrigger;
        [SerializeField] private PlayerState _playerState;

        [Inject] private TimelineSwitcher _timelineSwitcher;

        private void Start()
        {

            //サーバーにデータを送受信したら
            /*
            tempData.tempserverflag
                    .SkipLatestValueOnSubscribe()
                    .DistinctUntilChanged()
                    .Where(x => x == false && CurrentGameState.Value == GameState.Initializing)
                    .Subscribe(_ => _gameState.SetValueAndForceNotify(GameState.Direction));
                    */

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

            //サーバーにデータを送受信したら
            WS.WSManager.giveBattle
                        .DistinctUntilChanged()
                        .Where(_ =>
                           CurrentGameState.Value == GameState.Battle || CurrentGameState.Value == GameState.WaitingSignal
                               //&&自分がデータを送信したことを通知するflag(send()とかで監視)
                          )
                        .Subscribe(_ => _gameState.SetValueAndForceNotify(GameState.Finished));

            //アニメーション終了後
            //今はplayabledirectorのlap modeをholdにしてるので通知されない(resultに移動しない)
            _victoryTimelineTrigger.isFinishedDirection
                                   .DistinctUntilChanged()
                                   .Where(x => x == true && CurrentGameState.Value == GameState.Finished)
                                   .Subscribe(_ => _gameState.SetValueAndForceNotify(GameState.Result));
        }
    }
}


