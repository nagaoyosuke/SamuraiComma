using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using SamuraiComma.Main.Manager;

public class DebugDisplayGameState : MonoBehaviour
{
    [Inject] private GameStateManager _manager;
    private Text _text;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        _text = GetComponent<Text>();

        //ログインまで行けた
        //JsonManager.Send.LoginJson json = new JsonManager.Send.LoginJson(-1, "test1", "kouzi", "hirakata");
        //print(json.ToJson());

        ////JsonManager.Send.MatchingJson json2 = new JsonManager.Send.MatchingJson(-1,"test",false);
        ////print(json2.ToJson());

        ////JsonManager.Receive.LoginJson json3 = new JsonManager.Receive.LoginJson(-1);
        ////print(json3.ToJson());

        ////JsonManager.Receive.MatchingJson json4 = new JsonManager.Receive.MatchingJson(-1,"test");
        ////print(json4.ToJson());


        //SamuraiComma.Main.WS.WSManager.Send(json.ToJson());

        //yield return new WaitForSeconds(1.0f);

        //JsonManager.Send.MatchingJson json2 = new JsonManager.Send.MatchingJson(1, "ここ", true);
        //print(json2.ToJson());
        //SamuraiComma.Main.WS.WSManager.Send(json2.ToJson());

        //SamuraiComma.Main.WS.WSManager.Send(json.ToJson());
    }

    // Update is called once per frame
    void Update()
    {
        _text.text = _manager.CurrentGameState.Value.ToString();
    }
}
