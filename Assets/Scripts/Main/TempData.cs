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
    public string tempName = "けんた";
    public int tempRate = 2512;

    //サーバーからでーたをもらったらtrueになる(仮)
    public BoolReactiveProperty tempdataflag =  new BoolReactiveProperty(false);


    private void Start()
    {
        StartCoroutine(DelayClass.DelayCoroutin(180,() => tempdataflag.Value = true));
        StartCoroutine(DelayClass.DelayCoroutin(182, () => tempdataflag.Value = false));
    }
}
