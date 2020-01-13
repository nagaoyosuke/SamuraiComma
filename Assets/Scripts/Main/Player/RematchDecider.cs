using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SamuraiComma.Main.Manager;
using SamuraiComma.Main.Inputs;
using UniRx;
using UniRx.Triggers;
using Zenject;

namespace SamuraiComma.Main.Player
{
    /// <summary>
    /// 2020/01/11 toyoda
    /// 試合後にAボタンで再戦する、Bボタンでタイトルに戻る処理。
    /// </summary>

    public class RematchDecider : MonoBehaviour
    {
        [SerializeField] private KeyboardInputProvider _inputProvider;
        [Inject] private GameStateManager _gameStateManager;
        [Inject] private ScreenFader _screenFader;

        private void Start()
        {
            this.UpdateAsObservable()
                .Where(_ => (_gameStateManager.CurrentGameState.Value == GameState.Result) && (_inputProvider.enterTrigger.Value))
                .Subscribe(_ =>
                {
                    _screenFader.isFadeOut = true;
                    StartCoroutine(DelayClass.DelayCoroutin(60 * 1, () => MySceneManager.GoMain()));
                });

            this.UpdateAsObservable()
                .Where(_ => (_gameStateManager.CurrentGameState.Value == GameState.Result) && (_inputProvider.cancelTrigger.Value))
                .Subscribe(_ =>
                {
                    _screenFader.isFadeOut = true;
                    StartCoroutine(DelayClass.DelayCoroutin(60 * 1, () => MySceneManager.GoTitle()));
                });
        }

    }
}