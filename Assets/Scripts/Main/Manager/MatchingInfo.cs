using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

using UniRx;
using UniRx.Async;

using SamuraiComma.Main.WS;
using SamuraiComma.Main.Manager;
using Zenject;

namespace SamuraiComma.Main.Manager
{
    /// <summary>
    /// 対戦中のプレイヤーのアカウント情報
    /// </summary>
    public class MatchingInfo : MonoBehaviour
    {
        [Inject] private GameStateManager _gameStateManager;

        [SerializeField] private UserAccountStatus _playerAccount;
        [SerializeField] private UserAccountStatus _opponentAccount;

        public UserAccountStatus playerAccount => _playerAccount;
        public UserAccountStatus opponentAccount => _opponentAccount;

        public Text a;


        private void Awake()
        {
            //セーブデータから自分の情報をもってくる処理。


            _playerAccount.Init(1, "Unity", "uniuniUnity", "Earth");
        }

        private void Start()
        {
            _gameStateManager.CurrentGameState
                             .DistinctUntilChanged()
                             .Where(x => x == GameState.Direction)
                             .Subscribe(_ =>
                             {
                                 var initJson = WSManager.giveInit.Value;
                                 var matchingJson = WSManager.giveMatching.Value;
                                 _opponentAccount.Init(matchingJson.oppID, matchingJson.oppName, initJson.nickName, initJson.streetAddress);
                             });
        }

        private void Update()
        {
            a.text = opponentAccount.UserName + "/" + opponentAccount.nickname + "/" + opponentAccount.userID + "/" + opponentAccount.streetAdress;
        }

    }
}

