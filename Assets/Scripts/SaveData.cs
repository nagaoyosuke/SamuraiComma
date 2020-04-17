using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SamuraiComma
{
    /// <summary>
    /// セーブデータ。
    /// </summary>
    [System.Serializable]
    public class SaveData
    {
        public int userID;
        public string userName;
        public string nickname;
        public string streetAdress;
    }
}

