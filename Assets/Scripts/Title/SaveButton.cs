using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SamuraiComma;
using Zenject;

namespace SamuraiComma.Title
{
    /// <summary>
    /// zenjectの使い方下手かも
    /// titleシーンのアカウント情報を保存するボタンにはっつけてます。
    /// </summary>
    public class SaveButton : MonoBehaviour
    {
        [Inject] private SaveDataManager _saveDataManager;

        public void OnPushedSaveButton() => _saveDataManager.SavePlayerDataFromInputField();
    }
}


