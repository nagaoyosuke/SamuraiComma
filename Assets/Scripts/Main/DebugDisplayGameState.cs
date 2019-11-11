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
    void Start()
    {
        _text = GetComponent<Text>();

        JsonManager.Receive.InitializingJson json = new JsonManager.Receive.InitializingJson("s", "gg", "こうじ");
        SamuraiComma.Main.WS.WSManager.MessageCheck(json.ToJson());
        //SamuraiComma.Main.WS.WSManager.Send(json.ToJson());
    }

    // Update is called once per frame
    void Update()
    {
        _text.text = _manager.CurrentGameState.Value.ToString();
    }
}
