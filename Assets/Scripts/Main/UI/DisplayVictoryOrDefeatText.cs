using System.Collections;
using System.Collections.Generic;
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

    public class DisplayVictoryOrDefeatText : MonoBehaviour
    {

        [SerializeField] private Text _text;

        [Inject] private GameStateManager _gameStateManager;

        private void Start()
        {
            _text.enabled = false;

            _gameStateManager.CurrentGameState
                             .DistinctUntilChanged()
                             .Where(x => x == GameState.Result)
                             .Subscribe(_ =>
                             {
                                 _text.enabled = true;

                                 if (WSManager.giveBattle.Value.isJudge)
                                 {
                                     _text.text = "かち";
                                 } else {
                                     _text.text = "まけ";
                                 }
                             }
                                       );
        }
    }
}


