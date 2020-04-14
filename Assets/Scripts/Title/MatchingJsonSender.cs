using UnityEngine;
using UnityEngine.UI;
using SamuraiComma.Main.Manager;
using SamuraiComma.Main.WS;
using UniRx;

namespace SamuraiComma.Title
{
    /// <summary>
    /// 親オブジェクトのLobbyPlayerStatusの情報を元にJsonを送信します。
    /// </summary>
    public class MatchingJsonSender : MonoBehaviour
    {
        /// <summary>
        /// "果たし状を送る"ボタンを押した場合の処理。サーバーからInitJsonが帰ってくる。
        /// </summary>
        public void OnPushedSendDuelLetterButton()
        {
            var oppStatus = this.gameObject.transform.parent.GetComponent<LobbyPlayerStatus>();
            var MatchingJson = new JsonManager.Send.MatchingJson(oppID: oppStatus.userID, oppName: oppStatus.name, isBattle: true);
            WSManager.Send(MatchingJson.ToJson());
            print("matchingJsonを送信しました。");
        }
    }
}
