using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace SamuraiComma.Main.Manager
{
    public class JsonManager
    {
        public class Send
        {
            /// <summary>
            /// ログイン接続するとき
            /// </summary>
            public class LoginJson : JsonMethod
            {
                public string state;

                /// <summary>
                /// データベース接続用
                /// 初回接続時は-1
                /// </summary>
                public int userID;

                /// <summary>
                /// 名前
                /// </summary>
                public string userName;

                /// <summary>
                /// 二つ名
                /// </summary>
                public string nickName;
                /// <summary>
                /// 住所
                /// </summary>
                public string streetAddress;

                public LoginJson(int userID,string userName,string nickName,string streetAddress)
                {
                    state = "Login";
                    this.userID = userID;
                    this.userName = userName;
                    this.nickName = nickName;
                    this.streetAddress = streetAddress;
                }
            }

            /// <summary>
            /// 部屋に入るときにサーバに送る情報
            /// 未使用
            /// </summary>
            public class EnterRoomJson : JsonMethod
            {
                //サーバが判断するために
                public string state;
                /// <summary>
                /// 名前
                /// </summary>
                public string userName;
                /// <summary>
                /// 二つ名
                /// </summary>
                public string nickName;
                /// <summary>
                /// 住所
                /// </summary>
                public string streetAddress;

                /// <summary>
                /// 入りたい相手のidを入れる、部屋を立てる場合は-1にする
                /// </summary>
                public int inRoomNum;

                public EnterRoomJson(string userName, string nickName, string streetAddress, int inRoomNum)
                {
                    this.state = "EnterRoom";
                    this.userName = userName;
                    this.nickName = nickName;
                    this.streetAddress = streetAddress;
                    this.inRoomNum = inRoomNum;
                }
            }

            /// <summary>
            /// 対戦したい奴に送る情報
            /// </summary>
            public class MatchingJson : JsonMethod
            {
                //サーバが判断するために
                public string state;

                /// <summary>
                /// 対戦したい人のID
                /// </summary>
                public int oppID;

                /// <summary>
                /// 対戦したい人の名前入れる
                /// </summary>
                public string oppName;

                /// <summary>
                /// 対戦を受けるかどうか
                /// 自分から送る場合はtrueにする
                /// </summary>
                public bool isBattle;

                public MatchingJson(int oppID,string oppName,bool isBattle)
                {
                    this.state = "Matching";
                    this.oppID = oppID;
                    this.oppName = oppName;
                    this.isBattle = isBattle;
                }

            }


            /// <summary>
            /// 未使用
            /// </summary>
            public class InitializingJson : JsonMethod
            {
                //サーバが判断するために
                public string state;
                /// <summary>
                /// 名前
                /// </summary>
                public string name;
                /// <summary>
                /// 二つ名
                /// </summary>
                public string nickName;
                /// <summary>
                /// 住所
                /// </summary>
                public string streetAddress;

                public InitializingJson(string name, string nickName, string streetAddress                                          )
                {
                    this.state = "Init";
                    this.name = name;
                    this.nickName = nickName;
                    this.streetAddress = streetAddress;
                }
            }

            /// <summary>
            /// メインの対戦の情報
            /// </summary>
            public class BattleJson : JsonMethod
            {
                //サーバが判断するために
                public string state;

                public float attackTime;

                public BattleJson(float attackTime)
                {
                    this.state = "Battle";
                    this.attackTime = attackTime;
                }
            }

            /// <summary>
            /// メッセージを相手のクライアントに送ることができる。
            /// </summary>
            public class DirectMessageJson : JsonMethod
            {
                public string state;
                /// <summary>
                /// 送信先のUserID
                /// </summary>
                public int destinationUserID;
                /// <summary>
                /// 送信先のUserName
                /// </summary>
                public string destinationUserName;
                /// <summary>
                /// メッセージ内容
                /// </summary>
                public string messageText;

                public DirectMessageJson(int userID, string userName, string text)
                {
                    this.state = "DirectChat";
                    this.destinationUserID = userID;
                    this.destinationUserName = userName;
                    this.messageText = text;
                }
            }

            /// <summary>
            /// その他のAPIを使うよう
            /// </summary>
            public class APIJson : JsonMethod
            {
                /// <summary>
                /// サーバ側に処理してほしいAPIを送る
                /// </summary>
                public string state;

                public int userID;

                /// <summary>
                /// 名前
                /// </summary>
                public string userName;

                public APIJson(string state,int userID,string userName)
                {
                    this.state = state;
                    this.userID = userID;
                    this.userName = userName;
                }
            }
        }

        public class Receive
        {
            /// <summary>
            /// ログイン接続するとき
            /// </summary>
            public class LoginJson : JsonMethod
            {
                public string state;

                /// <summary>
                /// データベース接続用
                /// </summary>
                public int userID;

                public LoginJson(int userID)
                {
                    state = "Login";
                    this.userID = userID;
                }
            }

            /// <summary>
            /// 部屋を立てたときにサーバから送られてくる情報
            /// 未使用
            /// </summary>
            public class RoomJson : JsonMethod
            {
                /// <summary>
                /// 立てた部屋のID
                /// </summary>
                public int roomID;

                public RoomJson(int roomID)
                {
                    this.roomID = roomID;
                }

            }

            /// <summary>
            /// 対戦がしたいやつから送られてきた情報
            /// </summary>
            public class MatchingJson : JsonMethod
            {
                //サーバが判断するために
                public string state;

                /// <summary>
                /// 対戦がしたい人のID
                /// </summary>
                public int oppID;

                /// <summary>
                /// 対戦がしたい人の名前
                /// </summary>
                public string oppName;

                public MatchingJson(int oppID, string oppName)
                {
                    this.state = "Matching";
                    this.oppID = oppID;
                    this.oppName = oppName;
                }
            }


            /// <summary>
            /// 対戦が始まる前
            /// </summary>
            public class InitializingJson : JsonMethod
            {
                //サーバが判断するために
                public string state;

                /// <summary>
                /// 相手の名前
                /// </summary>
                public string userName;
                /// <summary>
                /// 相手の二つ名
                /// </summary>
                public string nickName;
                /// <summary>
                /// 相手の住所
                /// </summary>
                public string streetAddress;

                /// <summary>
                /// 対戦が開始されるまでの時間
                /// </summary>
                public float startTime;

                public InitializingJson(string userName, string nickName, string streetAddress,float startTime)
                {
                    this.state = "Init";
                    this.userName = userName;
                    this.nickName = nickName;
                    this.streetAddress = streetAddress;
                    this.startTime = startTime;
                }
            }

            /// <summary>
            /// サーバ側から送られてきた勝敗
            /// </summary>
            public class BattleJson : JsonMethod
            {
                //サーバが判断するために
                public string state;

                /// <summary>
                /// 自分の勝敗
                /// </summary>
                public string isJudge;

                public float myTime;
                public float oppTime;

                public BattleJson(string isJudge,float myTime,float oppTime)
                {
                    this.state = "Battle";
                    this.isJudge = isJudge;
                    this.myTime = myTime;
                    this.oppTime = oppTime;
                }
            }

            /// <summary>
            /// オンラインのユーザー一覧を取得
            /// </summary>
            public class MemberListJson : JsonMethod
            {
                //サーバが判断するために
                public string state;

                public MemberJson[] Member;
                public MemberListJson(MemberJson[] Member)
                {
                    this.state = "MemberList";
                    this.Member = Member;
                }
            }

            /// <summary>
            /// オンラインのユーザーを取得
            /// </summary>
            [Serializable]
            public class MemberJson : JsonMethod
            {
                //サーバが判断するために
                public int userID;

                /// <summary>
                /// 相手の名前
                /// </summary>
                public string userName;
                /// <summary>
                /// 相手の二つ名
                /// </summary>
                public string nickName;
                /// <summary>
                /// 相手の住所
                /// </summary>
                public string streetAddress;


                public MemberJson(int userID, string userName, string nickName, string streetAddress)
                {
                    this.userID = userID;
                    this.userName = userName;
                    this.nickName = nickName;
                    this.streetAddress = streetAddress;
                }
            }

            /// <summary>
            /// メッセージが送られてくる。
            /// </summary>
            public class DirectMessageJson : JsonMethod
            {
                /// <summary>
                /// 送信元のUserID
                /// </summary>
                public int senderUserID;
                /// <summary>
                /// 送信元のUserName
                /// </summary>
                public string senderUserName;
                /// <summary>
                /// メッセージ内容
                /// </summary>
                public string messageText;
            }

            /// <summary>
            /// API使った時のサーバからの返事
            /// </summary>
            [Serializable]
            public class APIJson : JsonMethod
            {
                /// <summary>
                /// サーバの返事
                /// </summary>
                public string state;

                public int userID;

                /// <summary>
                /// 名前
                /// </summary>
                public string userName;

                public APIJson(string state, int userID, string userName)
                {
                    this.state = state;
                    this.userID = userID;
                    this.userName = userName;
                }
            }

            /// <summary>
            /// サーバからのエラーメッセージ
            /// </summary>
            public class ErrorJson : JsonMethod
            {
                /// <summary>
                /// サーバの返事
                /// </summary>
                public string state;

                public string message;

                public ErrorJson(string state, string message)
                {
                    this.state = state;
                    this.message = message;
                }
            }
        }
    }

    public class JsonMethod
    {
        public string ToJson()
        {
            return JsonUtility.ToJson(this);
        }
    }
}
