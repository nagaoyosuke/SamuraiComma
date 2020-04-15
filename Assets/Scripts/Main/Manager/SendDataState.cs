namespace SamuraiComma.Main.Manager
{
    /// <summary>
    /// 2019/11/27 toyoda
    /// サーバーへのデータ送信状態のenum
    /// </summary>
    public enum SendDataState
    {
        /// <summary>
        /// まだサーバーにデータを送っていない状態。
        /// </summary>
        UnSent,
        /// <summary>
        /// サーバーにデータを送信している状態。2020/01/13現在未使用。
        /// </summary>
        Sending,
        /// <summary>
        /// サーバーに必要なデータを送信した状態。
        /// </summary>
        OnSent,
    }
}