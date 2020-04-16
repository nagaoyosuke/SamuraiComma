using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using SamuraiComma.Main.Manager;
using SamuraiComma.Main.WS;
using UniRx;

namespace SamuraiComma.Title
{
    /// <summary>
    /// ロビー一覧を操作するスクリプト toyoda
    /// </summary>
    public class LobbyPlayerListController : MonoBehaviour
    {

        [SerializeField] private GameObject onlinePlayerList;
        [SerializeField] private GameObject onlinePlayerListContent;
        private JsonManager.Send.APIJson MemberListJson = new JsonManager.Send.APIJson(state: "MemberList", userID: -1, userName: "NoName");

        private void Start()
        {
            UpdateLobbyPlayerList();
        }

        private void UpdateLobbyPlayerList()
        {
            WSManager.Send(MemberListJson.ToJson());


            //サーバー上のデータに存在しないのに、画面にアカウントが表示されている場合
            foreach (RectTransform content in onlinePlayerList.transform)
            {
                //サーバーのメンバーリストの中の名前が１つもunity上のobjectの名前と一致しない場合
                if (!WSManager.giveMemberList.Value.Member.Any(d => MojibakeTranslater.ConvertLatinToUtf8(d.userName) + "/" + d.userID.ToString() == content.name))
                {
                    //一致しないオブジェクトをdestroy
                    Destroy(content.gameObject);
                }
            }

            //サーバー上のプレイヤー一覧
            foreach (var m in WSManager.giveMemberList.Value.Member)
            {
                string userName = MojibakeTranslater.ConvertLatinToUtf8(m.userName);
                string nickName = MojibakeTranslater.ConvertLatinToUtf8(m.nickName);
                string streetAddress = MojibakeTranslater.ConvertLatinToUtf8(m.streetAddress);

                //のIDと一致するゲームオブジェクトが存在しない場合、生成する。
                if (GameObject.Find(userName + "/" + m.userID) == null)
                {
                    var playerObject = Instantiate(onlinePlayerListContent, transform.position, Quaternion.identity);

                    playerObject.name = userName + "/" + m.userID;
                    playerObject.transform.SetParent(onlinePlayerList.transform, false);

                    playerObject.transform.Find("PlayerName").GetComponent<Text>().text = userName + " / " + streetAddress;
                    playerObject.GetComponent<UserAccountStatus>().Init(m.userID, userName, nickName, streetAddress);
                }
            }
        }

        /// <summary>
        /// 更新ボタンを押した時の処理
        /// </summary>
        public void OnPushedUpdateLobbyPlayerListButton()
        {
            UpdateLobbyPlayerList();
        }
    }

}
