using UnityEngine;
using UnityEngine.UI;

using SamuraiComma.Main.Manager;
using UniRx;
using Zenject;

//joy-conのやつができるまでの繋ぎ
public class TempBattle : MonoBehaviour
{
    public GameObject setsunaButton;
    //public CommandManager commandManager;
    public Text text;
    public bool isSlashed = false;

    [Inject] private ScreenFader _screenFader;
    [Inject] private GameStateManager _gameStateManager;
    [Inject] private TimeManager _timeManager;
    [Inject] private SendDataStateManager _sendDataStateManager;

    //private IntReactiveProperty _joycon = new IntReactiveProperty(0);
    //public IReadOnlyReactiveProperty<int> joycon => _joycon;

    private void Start()
    {
        //joyconを振ると動作(仮)
        /*
        _joycon
            .DistinctUntilChanged()
            .Where(_ => _gameStateManager.CurrentGameState.Value == GameState.Battle)
            .Subscribe(_ =>
        {
            OnClickedTempButton();
        }
                      );
                      */

        _sendDataStateManager.battleSendState
                             .DistinctUntilChanged()
                             .Where(x => x == SendDataState.OnSent)
                             .Subscribe(_ => _screenFader.isFadeOut = true);

        _timeManager.trajectoryTimer
                    .Where(x => x <= 0.0f)
                    .Subscribe(_=>
        {
            setsunaButton.SetActive(false);
            text.enabled = false;
        }
                                            );
    }

    private void Update()
    {
        //_joycon.Value = commandManager.SwingLineR;
    }

    /// <summary>
    /// joy-conで斬る処理(仮)ボタンを押した時の処理。
    /// </summary>
    public void OnClickedTempButton(){

        if (!isSlashed)
        {
            isSlashed = true;
            Sound.PlaySe("hero01");
            setsunaButton.SetActive(false);
            text.enabled = false;
            var battleJson = new JsonManager.Send.BattleJson(attackTime: _timeManager._trajectoryTimeLimit - _timeManager.trajectoryTimer.Value);
            SamuraiComma.Main.WS.WSManager.Send(battleJson.ToJson());
        }
    }

}
