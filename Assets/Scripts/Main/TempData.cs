using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UniRx;
using SamuraiComma.Main.Manager;


//ネットまとまるまでのつなぎ
public class TempData : MonoBehaviour
{

    [Inject] private GameStateManager _manager;
    public float tempSignalLimitTime = 5.0f;
    public string tempName = "けんた";
    public int tempRate = 2512;
    public float signalTime;

    public BoolReactiveProperty tempdataflag =  new BoolReactiveProperty(false);
    public SetsunaSignal tempSignal;


    // Start is called before the first frame update
    private void Start()
    {
        signalTime = Random.RandomRange(5.0f, 15.0f);
        StartCoroutine(DelayClass.DelayCoroutin(180,() => tempdataflag.Value = true));
    }

    // Update is called once per frame
    void Update()
    {
        if(tempdataflag.Value && _manager.CurrentGameState.Value == GameState.WaitingSignal){
            print(signalTime + "秒後に合図");

            tempdataflag.Value = false;
            StartCoroutine(tempSignal.Until(signalTime));
        }
        
    }
}
