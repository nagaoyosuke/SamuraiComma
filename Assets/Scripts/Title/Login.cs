using UnityEngine;
using SamuraiComma.Main.Manager;
using SamuraiComma.Main.WS;
using Zenject;

namespace SamuraiComma.Title
{
    /// <summary>
    /// サーバーにログインする。
    /// </summary>
    public class Login : MonoBehaviour
    {
        [Inject] private SaveDataManager _saveDataManager;

        private void Start()
        {
            _saveDataManager.Load();
            var loginJson = new JsonManager.Send.LoginJson(_saveDataManager.save.userID, _saveDataManager.save.userName, _saveDataManager.save.nickname, _saveDataManager.save.streetAdress);
            WSManager.Send(loginJson.ToJson());
        }
    }
}


