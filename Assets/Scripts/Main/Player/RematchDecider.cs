using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SamuraiComma.Main.Manager;
using SamuraiComma.Main.Inputs;
using SamuraiComma.Main.WS;

using UniRx;
using UniRx.Triggers;
using UniRx.Async;
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
        [Inject] private MatchingInfo _matchingInfo;
        [Inject] private SendDataStateManager _sendDataStateManager;

        private JsonManager.Send.DirectMessageJson _messageJson;
        /// <summary>
        /// ボタン入力が実行済みであるか
        /// </summary>
        private bool isExecuted = false;
        /// <summary>
        /// 対戦相手からの返答を受信したか
        /// </summary>
        private bool isReceivedOpponentMessage = false;

        private void Start()
        {
            //再戦を押した時の処理
            this.UpdateAsObservable()
                .Where(_ => (_gameStateManager.CurrentGameState.Value == GameState.Result) && (_inputProvider.enterTrigger.Value) && !isExecuted)
                .Subscribe(_ =>
                {
                    _screenFader.isFadeOut = true;
                    isExecuted = true;

                    _messageJson = new JsonManager.Send.DirectMessageJson(_matchingInfo.opponentAccount.userID, _matchingInfo.opponentAccount.UserName, "Accept");
                    WSManager.Send(_messageJson.ToJson());
                    RematchAsync().Forget();
                });

            //タイトルへを押した時の処理
            this.UpdateAsObservable()
                .Where(_ => (_gameStateManager.CurrentGameState.Value == GameState.Result) && (_inputProvider.cancelTrigger.Value) && !isExecuted)
                .Subscribe(_ =>
                {
                    _screenFader.isFadeOut = true;
                    isExecuted = true;

                    _messageJson = new JsonManager.Send.DirectMessageJson(_matchingInfo.opponentAccount.userID, _matchingInfo.opponentAccount.UserName, "Decline");
                    WSManager.Send(_messageJson.ToJson());

                    var resetBattleJson = new JsonManager.Send.APIJson("MyBattleReSet");
                    WSManager.Send(resetBattleJson.ToJson());

                    StartCoroutine(DelayClass.DelayCoroutin(60 * 1, () => MySceneManager.GoTitle()));
                });

            WSManager.giveDirectMessage
                     .Where(_ => _gameStateManager.CurrentGameState.Value == GameState.Result)
                     .Subscribe(_ => isReceivedOpponentMessage = true);

        }

        /// <summary>
        /// 相手も再戦を希望していたらもう一度勝負する。
        /// </summary>
        /// <returns>The async.</returns>
        private async UniTaskVoid RematchAsync()
        {
            await UniTask.WaitUntil(() => _sendDataStateManager.rematchSendState.Value == SendDataState.OnSent);
            await UniTask.WaitUntil(() => isReceivedOpponentMessage == true);

            isReceivedOpponentMessage = false;

            if (WSManager.giveDirectMessage.Value.text.Contains("Accept"))
            {
                Sound.PlaySe("syakin");
                MySceneManager.GoMain();
            }
            else
            {
                var resetBattleJson = new JsonManager.Send.APIJson("MyBattleReSet");
                WSManager.Send(resetBattleJson.ToJson());
                MySceneManager.GoTitle();
            }
        }

    }
}