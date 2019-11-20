using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SamuraiComma.Main.Manager;
using SamuraiComma.Main.WS;


using UniRx;
using Zenject;

//joy-conのやつができるまでの繋ぎ
public class TempBattle : MonoBehaviour
{

    public DebugLogin debugtes;
    public Text timeLimitText;
    public GameObject setsunaButton;
    public UnityChanAnimationController unityChanAnimation;
    public UnityChanAnimationController opponentUnityChanAnimation;
    public ScreenFader screenFader;
    public CommandManager commandManager;

    [Inject] private GameStateManager _gameStateManager;
    [Inject] private TimeManager _timeManager;

    private IntReactiveProperty _joycon = new IntReactiveProperty(0);
    public IReadOnlyReactiveProperty<int> joycon => _joycon;

    // Start is called before the first frame update
    private void Start()
    {

        //joyconを振ると動作(仮)
        _joycon
            .DistinctUntilChanged()
            .Where(_ => _gameStateManager.CurrentGameState.Value == GameState.Battle)
            .Subscribe(_ => OnClickedTempButton());

        WSManager.giveBattle
                 .SkipLatestValueOnSubscribe()
                 .Where(_ => _gameStateManager.CurrentGameState.Value == GameState.Battle || _gameStateManager.CurrentGameState.Value == GameState.Finished)
                 .DistinctUntilChanged()
                 .Delay(System.TimeSpan.FromSeconds(3))
                 .Subscribe(_ =>
                 {
                    screenFader.isFadeIn = true;
                    print(_timeManager._trajectoryTimeLimit - _timeManager.trajectoryTimer.Value);

                 });
                           

    }

    private void Update()
    {
        _joycon.Value = commandManager.SwingLine;

    }

    public void OnClickedTempButton(){
        Sound.PlaySe("hero01");
        timeLimitText.enabled = false;
        setsunaButton.SetActive(false);
        JsonManager.Send.BattleJson json3 = new JsonManager.Send.BattleJson(attackTime: _timeManager._trajectoryTimeLimit - _timeManager.trajectoryTimer.Value);
        //一度目必ずエラーが起きるので二度送信
        SamuraiComma.Main.WS.WSManager.Send(json3.ToJson());
        SamuraiComma.Main.WS.WSManager.Send(json3.ToJson());
        SamuraiComma.Main.WS.WSManager.Send(json3.ToJson());
        SamuraiComma.Main.WS.WSManager.Send(json3.ToJson());

        screenFader.isFadeOut = true;

    }

}
