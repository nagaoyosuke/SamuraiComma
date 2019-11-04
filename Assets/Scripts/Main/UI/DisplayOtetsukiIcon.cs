using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;
using Zenject;
using SamuraiComma.Main.Manager;
using SamuraiComma.Main.Player;

namespace SamuraiComma.Main.UI
{
    /// <summary>
    /// 2019/11/03 toyoda
    /// 画面上にお手つきアイコンを表示する。
    /// Battle Canvas
    /// </summary>

    public class DisplayOtetsukiIcon : MonoBehaviour
    {
        [SerializeField] private Image _otetsukiImage;
        [SerializeField] private PlayerState _playerState;
        [Inject] private GameStateManager _gameStateManager;

        private void Start()
        {
            _playerState.canQuickDraw
                        .Where(x => x == true)
                        .Subscribe(_ => _otetsukiImage.enabled = false);

            this.UpdateAsObservable()
                .Where(_ =>
                       _playerState.canQuickDraw.Value == false
                       && _gameStateManager.CurrentGameState.Value == GameState.WaitingSignal
                      )
                .Subscribe(_ => _otetsukiImage.enabled = true);
        }
    }
}
