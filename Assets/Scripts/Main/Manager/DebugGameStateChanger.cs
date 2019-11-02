using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using SamuraiComma.Main.Manager;
using UniRx;

/// <summary>
/// 2019/11/02 toyoda
/// デバッグ用enum変更
/// gamestatemanager.csのReactiveProperty<GameState> _gameStateをpublicにしないと動作しない
/// </summary>

public class DebugGameStateChanger : MonoBehaviour
{
    [Inject] private GameStateManager _manager;

    private void Start()
    {
        _manager._gameState
                .DistinctUntilChanged()
                .Subscribe(_ => Debug.Log("debugEnum:" + _manager._gameState.Value));
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
            _manager._gameState.Value = GameState.Initializing;

        if (Input.GetKeyDown(KeyCode.W))
            _manager._gameState.Value = GameState.Direction;

        if (Input.GetKeyDown(KeyCode.E))
            _manager._gameState.Value = GameState.WaitingSignal;

        if (Input.GetKeyDown(KeyCode.R))
            _manager._gameState.Value = GameState.Battle;

        if (Input.GetKeyDown(KeyCode.T))
            _manager._gameState.Value = GameState.Finished;

        if (Input.GetKeyDown(KeyCode.Y))
            _manager._gameState.Value = GameState.Result;
            

    }
}
