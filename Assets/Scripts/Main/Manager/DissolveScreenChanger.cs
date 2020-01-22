using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Zenject;

namespace SamuraiComma.Main.Manager
{
    /// <summary>
    /// 2019/11/04 toyoda
    /// 現在未使用
    /// </summary>

    public class DissolveScreenChanger : MonoBehaviour
    {

        [Inject] private ScreenFader _screenFader;
        [Inject] private GameStateManager _gameStateManager;
        [SerializeField] private int _waitingSeconds = 1;

        private void Start()
        {
            /*
            _gameStateManager.CurrentGameState
                             .DistinctUntilChanged()
                             .Where(x => x == GameState.Direction)
                             .Subscribe(_ =>_screenFader.isFadeIn = true);
            */
        }
    }
}


