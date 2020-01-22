namespace SamuraiComma.Main.Manager
{
    /// <summary>
    /// 2019/10/30 toyoda
    /// ゲームの進行状態のenum
    /// </summary>
    public enum GameState
    {
        ///<summary>
        /// サーバーから情報をもらったり二試合目なら初期化処理など。
        /// </summary>
        Initializing,
        ///<summary>
        /// バーチャルカメラの演出, OOOvsXXXなど表示。
        /// </summary>
        Direction,
        /// <summary>
        /// 固定カメラの位置で合図を待っている間。
        /// </summary>
        WaitingSignal,
        /// <summary> 
        /// 軌跡が表示され、それを斬る。
        /// </summary>
        Battle,
        /// <summary>
        /// 勝者のアニメーションが再生される。
        /// </summary>
        Finished,
        /// <summary>
        /// タイムの差を表示したり再戦するか選択できる。
        /// </summary>
        Result
    }
}