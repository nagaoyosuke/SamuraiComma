using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UniRx;
using Zenject;

namespace SamuraiComma.Main.Manager
{
    /// <summary>
    /// 2019/10/31 toyoda
    /// GameStateに対応してタイムラインを変更して再生する。
    /// </summary>

    public class TimelineSwitcher : MonoBehaviour
    {
        [SerializeField] private PlayableDirector _preparePlayableDirector;
        [SerializeField] private PlayableDirector _fixPlayableDirector;
        [SerializeField] private PlayableDirector _victoryPlayableDirector;

        [Inject] private GameStateManager _gameStateManager;

        private void Start()
        {
            _gameStateManager.CurrentGameState
                             .FirstOrDefault(x => x == GameState.Direction)
                             .Delay(System.TimeSpan.FromSeconds(0.01f))
                             .Subscribe(_ => _preparePlayableDirector.Play(_preparePlayableDirector.playableAsset));

            _gameStateManager.CurrentGameState
                             .FirstOrDefault(x => x == GameState.WaitingSignal)
                             .Subscribe(_ => _fixPlayableDirector.Play(_fixPlayableDirector.playableAsset));

            _gameStateManager.CurrentGameState
                             .FirstOrDefault(x => x == GameState.Finished)
                             //delayかけないとなぜか動作しない。。
                             .Delay(System.TimeSpan.FromSeconds(0.01f))
                             .Subscribe(_ => _fixPlayableDirector.Stop());

            //ここでplayerStateのisDeathで条件分岐
            _gameStateManager.CurrentGameState
                             .FirstOrDefault(x => x == GameState.Finished)
                             .Delay(System.TimeSpan.FromSeconds(3))
                             .Subscribe(_ => _victoryPlayableDirector.Play(_victoryPlayableDirector.playableAsset));
        }
    }
}