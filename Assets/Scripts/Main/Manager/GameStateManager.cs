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

        public ReactiveProperty<GameState> _gameState = new ReactiveProperty<GameState>(GameState.Direction);
        public IReadOnlyReactiveProperty<GameState> CurrentGameState => _gameState;

        [SerializeField] private ObservablePrepareTimelineTrigger _prepareTimelineTrigger;
        [SerializeField] private ObservableFixTimelineTrigger _fixTimelineTrigger;
        [SerializeField] private ObservableVictoryTimelineTrigger _victoryTimelineTrigger;
        [SerializeField] private PlayerState _playerState;

        [Inject] private TimelineSwitcher _timelineSwitcher;

        //仮
        [SerializeField] private TempData tempData;

        private void Start()
        {

            //サーバーからデータが送られてきたらdirectionの処理
            tempData.tempdataflag
                  .DistinctUntilChanged()
                  .Where(x => x == true)
                  .Subscribe(_ => _gameState.SetValueAndForceNotify(GameState.Direction));

            //directionの演出が終わったら waitingsignal
            _prepareTimelineTrigger.isFinishedDirection
                  .DistinctUntilChanged()
                  .Where(x => x == true)
                  .Subscribe(_ => _gameState.SetValueAndForceNotify(GameState.WaitingSignal));

            //aボタンが押されたら　gamestate.battle
            _playerState.canBattle
                  .DistinctUntilChanged()
                  .Where(x => x == true)
                  .Subscribe(_ => _gameState.SetValueAndForceNotify(GameState.Battle));

            //ネットで処理がなされて、データが帰ってきたら .finished
            //.Subscribe(_ => _gameState.SetValueAndForceNotify(GameState.Finished));

            //アニメーション終了後 result
        }
    }
}


