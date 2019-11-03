using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Zenject;
using SamuraiComma.Main.Manager;

namespace SamuraiComma.Main.UI
{
    /// <summary>
    /// 2019/11/03 toyoda
    /// 画面上に合図を表示する。
    /// Battle Canvas
    /// </summary>

    public class DisplaySignal : MonoBehaviour
    {
        [SerializeField] private Image _signalImage;
        [Inject] private GameStateManager _gameStateManager;
        [Inject] private TimeManager _timeManager;

        private void Start()
        {
            _gameStateManager.CurrentGameState
                             .DistinctUntilChanged()
                             .Where(x => x != GameState.WaitingSignal)
                             .Subscribe(_ => _signalImage.enabled = false);

            _timeManager.signalTimer
                        .SkipLatestValueOnSubscribe()
                        .DistinctUntilChanged()
                        .Where(x => x <= 0)
                        .Subscribe(_ => {
                            _signalImage.enabled = true;
                            Sound.PlaySe("taiko01");
                        });

        }
    }
}


