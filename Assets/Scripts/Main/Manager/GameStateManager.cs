using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using SamuraiComma.Main.Camera;
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

        [Inject] private TimelineSwitcher _timelineSwitcher;

        private void Start()
        {
            /*
            スクリプトで生成してもなぜか再生終了時に自動でdestoryされる別のassetが生成、再生される
            var track = _timelineSwitcher.prepareDirectionPlayableAsset as TimelineAsset;
            TrackAsset controlTrack = track.GetOutputTracks().First(c => c.name.Equals("Control Track"));
            var controlPlayableAsset = controlTrack.GetClips().First(c => c.displayName == "PrepareDirectionControllTrack").asset as ControlPlayableAsset;
            //var trigger = controlPlayableAsset.prefabGameObject.GetComponent(typeof(ITimeControl)) as ITimeControl;
            a = controlPlayableAsset.prefabGameObject.GetComponent<ObservablePrepareTimelineTrigger>();
            */

            //サーバーからデータが送られてきたらdirectionの処理

            //directionの演出が終わったら waitingsignal
               _prepareTimelineTrigger.isFinishedDirection
                                      .DistinctUntilChanged()
                                      .Where(x => x == true)
                                      .Subscribe(_ => _gameState.SetValueAndForceNotify(GameState.WaitingSignal));

            //aボタンが押されたら　gamestate.battle
            //ネットで処理がなされて、データが帰ってきたら .finished
            //アニメーション終了後 result
        }
    }
}


