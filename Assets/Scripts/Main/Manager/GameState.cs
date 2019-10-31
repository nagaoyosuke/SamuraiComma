namespace SamuraiComma.Main.Manager
{
    /// <summary>
    /// 2019/10/30 toyoda
    /// ゲームの進行状態のenum
    /// </summary>

    public enum GameState
    {
        Initializing,//ネットなどから情報をもらったり二試合目なら初期化処理など、画面は真っ暗
        Direction,//バーチャルカメラの演出, OOOvsXXXなど表示　     ◯
        WaitingSignal,//固定カメラの位置で合図を待っている間       ◯  
        Battle,//軌跡が表示され、それを斬る
        Finished,//勝者のアニメーション               ◯
        Result//リザルト                            
    }
}