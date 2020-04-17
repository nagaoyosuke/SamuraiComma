using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SamuraiComma;
using Zenject;

namespace SamuraiComma.Title
{
    /// <summary>
    /// titleシーンのinputfieldをまとめてる
    /// </summary>
    public class InputFieldProvider : MonoBehaviour
    {
        [SerializeField] private InputField _userNameField;
        [SerializeField] private InputField _nicknameField;
        [SerializeField] private InputField _streetAdressField;

        public InputField userNameField => _userNameField;
        public InputField nicknameField => _nicknameField;
        public InputField streetAdressField => _streetAdressField;
    }
}

