using UnityEngine;
using SamuraiComma.Main.Manager;
using SamuraiComma.Main.WS;
using UniRx;

namespace SamuraiComma.Title
{
    /// <summary>
    /// 仮、ログインとマッチングしたらシーン飛ばす処理
    /// </summary>
    public class a : MonoBehaviour
    {
        private bool isMatched = false;

        private void Start()
        {
            var loginJson = new JsonManager.Send.LoginJson(2, "田中政志", "コンマに？", "日本/Japan");
            //var loginJson = new JsonManager.Send.LoginJson(1, "Umehara", "The Comma01", "Japan");

            WSManager.Send(loginJson.ToJson());

            WSManager.giveMatching
                     .SkipLatestValueOnSubscribe()
                     .DistinctUntilChanged()
                     .Subscribe(_ => isMatched = true);

        }

        private void Update()
        {
            //Unity Api(GameObject.Find()など)を別のthreadで使えないのでUpdateで処理してる。
            if (isMatched)
            {
                MySceneManager.GoMain();
                isMatched = false;
            }
        }
    }
}
