using System;
using System.Linq;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using SamuraiComma.Main.Manager;
using UniRx;
using UniRx.Async;
using UniRx.Async.Triggers;
using Zenject;

namespace SamuraiComma.Main.UI
{
    /// <summary>
    /// GameState.Direction時に自分と対戦相手の情報を表示する。
    /// </summary>
    public class DisplayMatchingInfo : MonoBehaviour
    {
        [SerializeField] private Text _playerName;
        [SerializeField] private Text _playerNickName;
        [SerializeField] private Text _playerUserID;
        [SerializeField] private Text _playerStreetAdress;

        [SerializeField] private Text _opponentName;
        [SerializeField] private Text _opponentNickName;
        [SerializeField] private Text _opponentUserID;
        [SerializeField] private Text _opponentStreetAdress;

        [Inject] private GameStateManager _gameStateManager;
        [Inject] private MatchingInfo _matchingInfo;

        /// <summary>
        /// 初期化処理
        /// </summary>
        private void Awake()
        {
            ChangeUIState(false);
        }

        private void Start()
        {
            _gameStateManager.CurrentGameState
                             .DistinctUntilChanged()
                             .Where(x => x == GameState.Direction)
                             .Subscribe(_ =>
            {
                UniAsync().Forget();
            });
        }

        //仮
        private void ChangeUIState(bool enabled)
        {
            _playerName.enabled = enabled;
            _playerNickName.enabled = enabled;
            _playerStreetAdress.enabled = enabled;

            _opponentName.enabled = enabled;
            _opponentNickName.enabled = enabled;
            _opponentStreetAdress.enabled = enabled;
        }

        private async UniTaskVoid UniAsync()//CancellationToken token)
        {
            _playerName.text = _matchingInfo.playerAccount.UserName;
            _playerNickName.text = _matchingInfo.playerAccount.nickname;
            _playerStreetAdress.text = _matchingInfo.playerAccount.streetAdress;

            _opponentName.text = MojibakeTranslater.ConvertLatinToUtf8(_matchingInfo.opponentAccount.UserName);
            _opponentNickName.text = MojibakeTranslater.ConvertLatinToUtf8(_matchingInfo.opponentAccount.nickname);
            _opponentStreetAdress.text = MojibakeTranslater.ConvertLatinToUtf8(_matchingInfo.opponentAccount.streetAdress);

            await UniTask.Delay(TimeSpan.FromSeconds(1.0f));//, cancellationToken: token);
            ChangeUIState(enabled: true);
            await UniTask.Delay(TimeSpan.FromSeconds(8.0f));//, cancellationToken: token);
            ChangeUIState(enabled: false);
        }
    }
}