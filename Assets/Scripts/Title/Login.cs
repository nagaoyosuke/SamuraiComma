using System.Collections;
using System.Collections.Generic;
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
            var loginJson = new JsonManager.Send.LoginJson(_saveDataManager.saveData.userID, _saveDataManager.saveData.userName, _saveDataManager.saveData.nickname, _saveDataManager.saveData.streetAdress);
            WSManager.Send(loginJson.ToJson());
            print("サーバーにログインしました。");
        }
    }
}


