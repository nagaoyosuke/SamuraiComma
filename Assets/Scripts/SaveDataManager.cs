using System.IO;
using UnityEngine;
using SamuraiComma.Title;
using Zenject;

namespace SamuraiComma
{
    /// <summary>
    /// セーブデータを管理する。
    /// </summary>
    public class SaveDataManager : MonoBehaviour
    {
        private string filePath;
        private bool _isLoadedSaveData = false;
        private SaveData _save;
        public SaveData save => _save;

        [Inject] private InputFieldProvider _inputFieldProvider;

        /// <summary>
        /// 初期化
        /// </summary>
        private void Awake()
        {
            //filePath = Application.persistentDataPath + "/" + ".savedata.json";
            filePath = "/Users/hw17a114/SamuraiComma" + "/" + ".savedata.json";
            _save = new SaveData();
        }

        /// <summary>
        /// アカウントのプレイヤー情報を保存する。
        /// </summary>
        /// <param name="userID">User identifier.</param>
        /// <param name="userName">User name.</param>
        /// <param name="nickname">Nickname.</param>
        /// <param name="streetAdress">Street adress.</param>
        public void SavePlayerData(int userID, string userName, string nickname, string streetAdress)
        {
            _save.userID = userID;
            _save.userName = userName;
            _save.nickname = nickname;
            _save.streetAdress = streetAdress;

            Save();
        }

        /// <summary>
        /// inputfieldのデータを保存する。
        /// </summary>
        public void SavePlayerDataFromInputField()
        {
            if (_inputFieldProvider.userNameField.text != null)
                _save.userName = _inputFieldProvider.userNameField.text;

            if (_inputFieldProvider.nicknameField.text != null)
                _save.nickname = _inputFieldProvider.nicknameField.text;

            if (_inputFieldProvider.streetAdressField.text != null)
                _save.streetAdress = _inputFieldProvider.streetAdressField.text;

            Save();
        }

        /// <summary>
        /// jsonに保存する。
        /// </summary>
        private void Save()
        {
            string json = JsonUtility.ToJson(_save);

            StreamWriter streamWriter = new StreamWriter(filePath);
            streamWriter.Write(json);
            streamWriter.Flush();
            streamWriter.Close();

            print(json + "を保存しました。/Save()");
        }

        /// <summary>
        /// jsonを読み込む。
        /// </summary>
        public void Load()
        {
            if (File.Exists(filePath))
            {
                StreamReader streamReader;
                streamReader = new StreamReader(filePath);
                string data = streamReader.ReadToEnd();
                streamReader.Close();

                _save = JsonUtility.FromJson<SaveData>(data);
                _isLoadedSaveData = true;
            }
        }
    }
}

