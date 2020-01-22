using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;
using System;
using SamuraiComma.Main.Manager;
using UniRx;

namespace SamuraiComma.Main.WS
{
    /// <summary>
    /// 対戦中に使われるWS通信の処理
    /// </summary>
    public class WSManager : SingletonMonoBehaviour<WSManager>
    {

        private static WebSocket ws;
        private static System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();

        private static string giveJson;

        private static ReactiveProperty<JsonManager.Receive.InitializingJson> _giveInit = new ReactiveProperty<JsonManager.Receive.InitializingJson>();
        public static IReadOnlyReactiveProperty<JsonManager.Receive.InitializingJson> giveInit => _giveInit;

        private static ReactiveProperty<JsonManager.Receive.BattleJson> _giveBattle = new ReactiveProperty<JsonManager.Receive.BattleJson>();
        public static IReadOnlyReactiveProperty<JsonManager.Receive.BattleJson> giveBattle => _giveBattle;

        private static ReactiveProperty<JsonManager.Receive.LoginJson> _giveLogin = new ReactiveProperty<JsonManager.Receive.LoginJson>();
        public static IReadOnlyReactiveProperty<JsonManager.Receive.LoginJson> giveLogin => _giveLogin;

        private static ReactiveProperty<JsonManager.Receive.MatchingJson> _giveMatching = new ReactiveProperty<JsonManager.Receive.MatchingJson>();
        public static IReadOnlyReactiveProperty<JsonManager.Receive.MatchingJson> giveMatching => _giveMatching;


        private static bool isInit;

        void Awake()
        {
            DontDestroyOnLoad(transform.gameObject);
            Connect();
        }

        /// <summary>
        /// 初期化
        /// </summary>
        private static void Initialization()
        {
            if (isInit)
                return;

            ws.OnOpen += OnOpen;
            ws.OnMessage += OnMessage;
            ws.OnClose += OnClose;
            ws.OnError += OnError;
            isInit = true;
        }

        /// <summary>
        /// コネクションを確保
        /// </summary>
        /// <param name="url"></param>
        public static void Connect(string url = "ws://127.0.0.1:12345")
        {
            ws = new WebSocket(url);

            Initialization();

            //ws = new WebSocket("wss://pythonwebsockettest.herokuapp.com");

            ws.Connect();

            //ws.ConnectAsync();

            //ws.SendAsync("BALUS",value => print("send" + value));

        }

        private static void OnOpen(object sender, EventArgs e)
        {
            Debug.Log("Opended");

        }

        private static void OnMessage(object sender, MessageEventArgs e)
        {
            time();
            MonoBehaviour.print(e.Data);
            giveJson = e.Data;

            MessageCheck(e.Data);
            //data = e.Data;
            //give = JsonUtility.FromJson<give_stasu>(e.Data);
            //isGivedate = true;
            //if (give.name == "server")
            //{
            //    serverMessage = give.server;
            //}
            //if (lobby != null)
            //{
            //    lobby.GiveDataCheck();
            //}
            //if (Silentcave != null)
            //{
            //    Silentcave.GiveDataCheck();

            //    //サーバから送られてきた情報がアイテムの情報の場合
            //    if (give.name == "item")
            //    {
            //        if (item != null)
            //        {
            //            Debug.Log(give.isChanged);
            //            item.ItemSet(give.job, new Vector3(give.x, give.y, give.z), give.isChanged);
            //        }
            //    }
            //}
        }

        private static void OnClose(object sender, CloseEventArgs e)
        {
            Debug.Log(e);
        }

        private static void OnError(object sender, ErrorEventArgs e)
        {
            Debug.Log(e.Message);
        }

        private static void time()
        {
            sw.Stop();
            Debug.Log(sw.ElapsedMilliseconds + "ms");
        }

        public static void Send(string json)
        {
            try
            {
                sw.Restart();
                ws.Send(json);
                //isErr = false;
            }
            catch (Exception e)
            {
                //isErr = true;
                //if (isReco)
                //{
                //    Connect();
                //}
                Debug.LogError(e);
            }
        }

        
        public static void MessageCheck(string json)
        {
            //stateでどのクラスでシリアライズするかみてる
            string state = JsonUtility.FromJson<JsonManager.Receive.BattleJson>(json).state;
            switch (state)
            {
                case "Init":
                    _giveInit.Value = JsonUtility.FromJson<JsonManager.Receive.InitializingJson>(json);
                    break;
                case "Battle":
                    _giveBattle.Value = JsonUtility.FromJson<JsonManager.Receive.BattleJson>(json);
                    break;
                case "Login":
                    _giveLogin.Value = JsonUtility.FromJson<JsonManager.Receive.LoginJson>(json);
                    break;
                case "Matching":
                    _giveMatching.Value = JsonUtility.FromJson<JsonManager.Receive.MatchingJson>(json);
                    break;
                case "MemberList":
                    var a = JsonUtility.FromJson<JsonManager.Receive.MemberListJson>(json);
                    print(a.state);
                    print(a.Member);
                    foreach(JsonManager.Receive.MemberJson m in a.Member)
                    {
                        print(m.nickName);
                    }
                    break;
                default:
                    print(JsonUtility.FromJson<JsonManager.Receive.APIJson>(json));
                    break;
            }

            //Debug.Log(state);
        }

        void OnApplicationQuit()
        {

            //send.ready = "no";
            //send.ID = kari_ID;
            //send.job = myjob;
            //json = JsonUtility.ToJson(send);
            //NetSynthesis.Send(json);

            Resources.UnloadUnusedAssets();

            ws.Close();
            Debug.Log("正常にサーバから切断できました");

        }
    }
}