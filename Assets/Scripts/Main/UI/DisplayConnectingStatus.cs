using UnityEngine;
using UnityEngine.UI;
using SamuraiComma.Main.Manager;
using SamuraiComma.Main.WS;
using UniRx;
using Zenject;

namespace SamuraiComma.Main.UI
{
    /// <summary>
    /// 2020/01/13 toyoda
    /// サーバーと長時間通信している場合テキストを表示する。
    /// </summary>

    public class DisplayConnectingStatus : MonoBehaviour
    {
        [SerializeField] private Text _text;

        [Inject] private GameStateManager _gameStateManager;
        [Inject] private SendDataStateManager _sendDataManager;

        private void Start()
        {
            _text.enabled = false;

            _sendDataManager.initSendState
                            .DistinctUntilChanged()
                            .Where(x => x == SendDataState.OnSent)
                            .Delay(System.TimeSpan.FromSeconds(0.01f))
                            .Subscribe(_ => _text.enabled = true);

            _sendDataManager.battleSendState
                            .DistinctUntilChanged()
                            .Where(x => x == SendDataState.OnSent)
                            .Delay(System.TimeSpan.FromSeconds(0.01f))
                            .Subscribe(_ => _text.enabled = true);

            _sendDataManager.rematchSendState
                            .DistinctUntilChanged()
                            .Where(x => x == SendDataState.OnSent)
                            .Delay(System.TimeSpan.FromSeconds(0.01f))
                            .Subscribe(_ => _text.enabled = true);

            // データの送受信が完了したらGameStateはDirectionになるので同時に非表示にする。
            _gameStateManager.CurrentGameState
                             .DistinctUntilChanged()
                             .Where(x => x == GameState.Direction)
                             .Delay(System.TimeSpan.FromSeconds(0.01f))
                             .Subscribe(_ => _text.enabled = false);

            // データの送受信が完了したらGameStateはFinishedになるので同時に非表示にする。
            _gameStateManager.CurrentGameState
                             .DistinctUntilChanged()
                             .Where(x => x == GameState.Finished)
                             .Delay(System.TimeSpan.FromSeconds(0.01f))
                             .Subscribe(_ => _text.enabled = false);

            //再戦したらシーンを読み込み直し、GameStateはInitになるので同時に非表示にする。
            _gameStateManager.CurrentGameState
                             .DistinctUntilChanged()
                             .Where(x => x == GameState.Initializing)
                             .Delay(System.TimeSpan.FromSeconds(0.01f))
                             .Subscribe(_ => _text.enabled = false);

        }
    }
}