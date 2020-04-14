using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーの部屋のコンポーネント
/// </summary>
public class LobbyPlayerStatus :MonoBehaviour
{
    /// <summary>
    /// 初期化済みかどうか
    /// </summary>
    private bool isInit = false;

    [SerializeField] private int _userID = 0;
    [SerializeField] private string _userName = null;
    [SerializeField] private string _nickname = null;
    [SerializeField] private string _streetAdress = null;

    public int userID => _userID;
    public string UserName => _userName;
    public string nickname => _nickname;
    public string streetAdress => _streetAdress;

    /// <summary>
    /// 多分安全に初期化できる。
    /// </summary>
    /// <param name="userID">User identifier.</param>
    /// <param name="userName">User name.</param>
    /// <param name="nickname">Nickname.</param>
    /// <param name="streetAdress">Street adress.</param>
    public void Init(int userID, string userName, string nickname, string streetAdress)
    {
        if (!isInit)
        {
            this._userID = userID;
            this._userName = userName;
            this._nickname = nickname;
            this._streetAdress = streetAdress;

            isInit = true;
        }
    }
}