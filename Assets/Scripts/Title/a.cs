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
        private bool tanomu = false;

        private void Start()
        {
            var loginJson = new JsonManager.Send.LoginJson(101, "Unity", "Unity", "Hirakata");
            WSManager.Send(loginJson.ToJson());

            WSManager.giveMatching
                     .SkipLatestValueOnSubscribe()
                     .DistinctUntilChanged()
                     .Subscribe(_ => tanomu = true);

        }

        private void Update()
        {
            //Unity Api(GameObject.Find()など)を別のthreadで使えないのでUpdateで処理してる。
            if (tanomu)
            {
                MySceneManager.GoMain();
                tanomu = false;
            }
        }
    }
}
