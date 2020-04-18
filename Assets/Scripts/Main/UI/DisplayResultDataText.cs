using UnityEngine;
using UnityEngine.UI;
using SamuraiComma.Main.Manager;
using SamuraiComma.Main.WS;
using UniRx;
using Zenject;

namespace SamuraiComma.Main.UI
{

    /// <summary>
    /// 2019/11/27 toyoda
    /// 試合後の勝敗結果のテキストを表示する。
    /// </summary>

    public class DisplayResultDataText : MonoBehaviour
    {
        [SerializeField] private Text _matchResulttext;
        [SerializeField] private Text _playerNameText;
        [SerializeField] private Text _playerTimeText;
        //[SerializeField] private Text _opponentTimeText;

        [Inject] private GameStateManager _gameStateManager;
        [Inject] private MatchingInfo _matchingInfo;

        private void Awake()
        {
            SwitchToEnableText(false);
        }

        private void Start()
        {
            _gameStateManager.CurrentGameState
                             .DistinctUntilChanged()
                             .Where(x => x == GameState.Result)
                             .Subscribe(_ =>
                             {
                                 SwitchToEnableText(true);
                                 UpdateResultDataText();
                             }
                                       );
        }

        /// <summary>
        /// 表示非表示
        /// </summary>
        /// <param name="flag">If set to <c>true</c> flag.</param>
        private void SwitchToEnableText(bool flag)
        {
            _matchResulttext.enabled = flag;
            _playerNameText.enabled = flag;
            _playerTimeText.enabled = flag;
        }

        /// <summary>
        /// resultDataTextを更新する。
        /// </summary>
        private void UpdateResultDataText()
        {
            if (WSManager.giveBattle.Value.isJudge == "Win")
            {
                _matchResulttext.text = "勝利";
            }
            else if (WSManager.giveBattle.Value.isJudge == "Lose")
            {
                _matchResulttext.text = "敗北";
            }
            else if (WSManager.giveBattle.Value.isJudge == "Draw")
            {
                _matchResulttext.text = "引き分け";
            }

            _playerNameText.text = _matchingInfo.playerAccount.UserName;
            _playerTimeText.text = "速度   "+ WSManager.giveBattle.Value.myTime + "秒";
        }
    }
}