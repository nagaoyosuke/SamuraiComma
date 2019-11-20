using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using SamuraiComma.Main.Manager;
using Zenject;

namespace SamuraiComma.Main.Manager
{
    /// <summary>
    /// 2019/11/20 toyoda
    /// SceneごとのBGMを再生、停止する。
    /// </summary>

    public class SceneBGMController : MonoBehaviour
    {
        [Inject] private GameStateManager _gameStateManager;

        private void Start()
        {
            _gameStateManager.CurrentGameState
                             .DistinctUntilChanged()
                             .FirstOrDefault(x => x == GameState.Direction)
                             .Subscribe(_ => Sound.PlayBgm("wind"));

            _gameStateManager.CurrentGameState
                             .Where(x => x == GameState.Battle)
                             .Subscribe(_ => Sound.StopBgm());
        }

    }
}


