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

        private void Awake()
        {
            awawa(false);
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
        private void awawa(bool howa)
        {
            _playerName.enabled = howa;
            _playerNickName.enabled = howa;
            _playerStreetAdress.enabled = howa;

            _opponentName.enabled = howa;
            _opponentNickName.enabled = howa;
            _opponentStreetAdress.enabled = howa;
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
            awawa(howa: true);
            await UniTask.Delay(TimeSpan.FromSeconds(8.0f));//, cancellationToken: token);
            awawa(howa: false);
        }
    }
}