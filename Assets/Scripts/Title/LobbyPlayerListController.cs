using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
            WSManager.Send(MemberListJson.ToJson());
        }

        private void UpdateLobbyPlayerList()
        {
            WSManager.Send(MemberListJson.ToJson());


            //サーバー上のデータに存在しないのに、画面にアカウントが表示されている場合
            foreach (RectTransform content in onlinePlayerList.transform)
            {
                //サーバーのメンバーリストの中の名前が１つもunity上のobjectの名前と一致しない場合
                if (!WSManager.giveMemberList.Value.Member.Any(value => value.nickName + "/" + value.streetAddress == content.name))
                {
                    Destroy(content.gameObject);
                }
            }

            //サーバー上にデータが存在するが、画面にアカウントが表示されていない場合
            foreach (var m in WSManager.giveMemberList.Value.Member)
            {
                if (GameObject.Find(m.nickName + "/" + m.streetAddress) == null)
                {
                    var playerContent = Instantiate(onlinePlayerListContent, transform.position, Quaternion.identity);

                    playerContent.name = m.nickName + "/" + m.streetAddress;
                    playerContent.transform.SetParent(onlinePlayerList.transform, false);
                    playerContent.transform.Find("PlayerName").GetComponent<Text>().text = m.nickName + "/" + m.streetAddress;

                    var playerStatus = playerContent.GetComponent<LobbyPlayerStatus>();

                    playerStatus.Init(m.userID, m.userName, m.nickName, m.streetAddress);
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
