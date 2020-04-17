using System.IO;
using System;
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
        private SaveData _saveData;
        public SaveData saveData => _saveData;
        
        /// <summary>
        /// 初期化
        /// </summary>
        private void Awake()
        {
            //macosビルド後とunity editor上では動作する。
            filePath = Application.dataPath + "/" + ".savedata.json";
            _saveData = new SaveData();
            Load();
        }

        private void Start()
        {
            //サーバーにdbがない場合はこっちで生成する。
            if (_saveData.userID == 0)
            {
                _saveData.userID = GenerateRandomUserID();
                Save();
            }
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
            _saveData.userID = userID;
            _saveData.userName = userName;
            _saveData.nickname = nickname;
            _saveData.streetAdress = streetAdress;

            Save();
        }

        /// <summary>
        /// inputfieldのデータを保存する。
        /// </summary>
        public void SavePlayerDataFromInputField()
        {
            //TitleScene以外だとzenjectでとれないのでここで宣言する。
            InputFieldProvider inputFieldProvider = GameObject.Find("InputField Panel").GetComponent<InputFieldProvider>();

            if (inputFieldProvider.userNameField.text != null)
                _saveData.userName = inputFieldProvider.userNameField.text;

            if (inputFieldProvider.nicknameField.text != null)
                _saveData.nickname = inputFieldProvider.nicknameField.text;

            if (inputFieldProvider.streetAdressField.text != null)
                _saveData.streetAdress = inputFieldProvider.streetAdressField.text;

            Save();
        }

        private int GenerateRandomUserID()
        {
            string userIDString = "";

            for (int i = 0; i <= 7; i++)
            {
                userIDString += UnityEngine.Random.Range(0, 10).ToString();
            }

            return Convert.ToInt32(userIDString);
        }

        /// <summary>
        /// jsonに保存する。
        /// </summary>
        private void Save()
        {
            string json = JsonUtility.ToJson(_saveData);

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

                _saveData = JsonUtility.FromJson<SaveData>(data);
                _isLoadedSaveData = true;
            }
        }
    }
}