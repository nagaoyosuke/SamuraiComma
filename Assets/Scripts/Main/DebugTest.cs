using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Zenject;
using SamuraiComma.Main.WS;
using SamuraiComma.Main.Manager;


/// <summary>
/// 2019/11/19 toyoda
/// サーバーからもらうJsonを監視。
/// </summary>

public class DebugTest : MonoBehaviour
{
    [Inject] private GameStateManager _gameStateManager;
    [SerializeField] private Text _text;

    private void Start()
    {
        /*
        WSManager.ping
                 .DistinctUntilChanged()
                 .Subscribe(x => _text.text = x.ToString() + "ms");
                 */

        WSManager.giveInit
                 .SkipLatestValueOnSubscribe()
                 .DistinctUntilChanged()
                 .Subscribe(json => print(json.ToJson()));
                 
        WSManager.giveBattle
                 .SkipLatestValueOnSubscribe()
                 .DistinctUntilChanged()
                 .Subscribe(json => print(json.ToJson()));

        WSManager.giveLogin
                 .SkipLatestValueOnSubscribe()
                 .DistinctUntilChanged()
                 .Subscribe(json => print(json.ToJson()));

        WSManager.giveMatching
                 .SkipLatestValueOnSubscribe()
                 .DistinctUntilChanged()
                 .Subscribe(json => print(json.ToJson()));

        WSManager.giveAPI
                 .SkipLatestValueOnSubscribe()
                 .DistinctUntilChanged()
                 .Subscribe(json => print(json.ToJson()));



    }


}
