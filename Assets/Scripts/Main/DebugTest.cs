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
    //テキスト表示よう仮
    [SerializeField] private bool isJudge;
    private bool a = false;

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
                 .Subscribe(json => 
        {
            a = true;
            isJudge = json.isJudge;
            print(json.ToJson());

        }
                    );

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

    private void Update()
    {
        if (a)
        {
            if (isJudge)
            {
                _text.text = "かち";
            }
            else
            {
                _text.text = "まけ";
            }
        }
        else{
            _text.text = "";

        }

    }

}
