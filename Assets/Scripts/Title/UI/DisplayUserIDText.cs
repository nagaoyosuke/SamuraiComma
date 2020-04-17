using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SamuraiComma;
using Zenject;

namespace SamuraiComma.Title.UI
{
    /// <summary>
    /// 画面左上にアカウントのIDを表示する。 toyoda 0418
    /// </summary>
    public class DisplayUserIDText : MonoBehaviour
    {
        [SerializeField] private Text _text;
        [Inject] private SaveDataManager _saveDataManager;

        private void Start()
        {
            _text.text = "Account ID:" + _saveDataManager.saveData.userID;
        }
    }
}


