using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SamuraiComma.Main.Manager
{
    public class JsonManager
    {
        public class Send
        {

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

                public InitializingJson(string name, string nickName, string streetAddress)
                {
                    this.state = "Init";
                    this.name = name;
                    this.nickName = nickName;
                    this.streetAddress = streetAddress;
                }
            }

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
        }

        public class Receive
        {
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

                /// <summary>
                /// 対戦が開始されるまでの時間
                /// </summary>
                public float startTime;

                public InitializingJson(string name, string nickName, string streetAddress)
                {
                    this.state = "Init";
                    this.name = name;
                    this.nickName = nickName;
                    this.streetAddress = streetAddress;
                }
            }

            public class BattleJson : JsonMethod
            {
                //サーバが判断するために
                //サーバが判断するために
                public string state;

                public bool isJudge;

                public BattleJson(bool isJudge)
                {
                    this.state = "Battle";
                    this.state = state.ToString();
                    this.isJudge = isJudge;
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
