using System.Linq;
using System;
using UnityEngine;
using UnityEngine.UI;
using SamuraiComma.Main.Manager;
using UniRx;
using Zenject;

namespace SamuraiComma.Main.UI
{
    /// <summary>
    /// 2019/12/06 toyoda
    /// 操作コマンドを画面に表示する。
    /// </summary>
    public class DisplayOperationCommand : MonoBehaviour
    {

        [SerializeField] private GameObject _waitingSignalOperationCommands;
        [SerializeField] private GameObject _battleOperationCommands;
        [SerializeField] private GameObject _resultOperationCommands;

        [Inject] private GameStateManager _gameStateManager;
        [Inject] private SendDataStateManager _sendStateManager;

        private void Start()
        {

            _gameStateManager.CurrentGameState
                             .DistinctUntilChanged()
                             .Where(x => x == GameState.WaitingSignal)
                             .Subscribe(_ =>
            {
                _waitingSignalOperationCommands.SetActive(true);
                _battleOperationCommands.SetActive(false);
                _resultOperationCommands.SetActive(false);
            });

            _gameStateManager.CurrentGameState
                             .DistinctUntilChanged()
                             .Where(x => x == GameState.Battle)
                             .Subscribe(_ =>
            {
                _waitingSignalOperationCommands.SetActive(false);
                _battleOperationCommands.SetActive(true);
                _resultOperationCommands.SetActive(false);
            });

            _sendStateManager.battleSendState
                             .DistinctUntilChanged()
                             .Where(x => x == SendDataState.OnSent)
                             .Subscribe(_ =>
            {
                _waitingSignalOperationCommands.SetActive(false);
                _battleOperationCommands.SetActive(false);
                _resultOperationCommands.SetActive(false);
            });

            _gameStateManager.CurrentGameState
                             .DistinctUntilChanged()
                             .Where(x => x == GameState.Result)
                             .Subscribe(_ =>
            {
                _waitingSignalOperationCommands.SetActive(false);
                _battleOperationCommands.SetActive(false);
                _resultOperationCommands.SetActive(true);
            });
        }
    }
}