using UnityEngine;
using SamuraiComma.Main.Manager;
using SamuraiComma.Main.WS;
using UniRx;
using Zenject;

namespace SamuraiComma.Title
{
    /// <summary>
    /// 仮、ログインとマッチングしたらシーン飛ばす処理
    /// </summary>
    public class a : MonoBehaviour
    {
        private bool isMatched = false;
        [Inject] private SaveDataManager _saveDataManager;

        private void Start()
        {
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
