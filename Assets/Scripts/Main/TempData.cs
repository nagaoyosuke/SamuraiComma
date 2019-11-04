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

    //サーバーにデータを送る/もらうまでの間はtrueになる(仮)
    public BoolReactiveProperty tempserverflag =  new BoolReactiveProperty(false);


    private void Start()
    {
        TempServer(1 * 60);
        TempServer(5 * 60);

    }

    public void TempServer(int sec){
        StartCoroutine(DelayClass.DelayCoroutin(sec, () => tempserverflag.Value = !tempserverflag.Value));
    }
}
