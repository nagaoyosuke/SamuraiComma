using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SamuraiComma.Main.Inputs;
using SamuraiComma.Main.Manager;
using UniRx;
using UniRx.Triggers;
using Zenject;

namespace SamuraiComma.Main.Player
{
    /// <summary>
    /// 2019/11/02 toyoda
    /// プレイヤーの居合に関する処理。
    /// </summary>

    public class PlayerQuickDrawer : MonoBehaviour
    {

        [SerializeField] private IInputProvider _inputProvider;
        [SerializeField] private PlayerState _playerState;
        [Inject] private GameStateManager _gameStateManager;
        //仮
        [SerializeField] private TempBattle temp;
        private bool tempFlag;

        private void Start()
        {
            this.UpdateAsObservable()
                .Where(_ => (_gameStateManager.CurrentGameState.Value == GameState.WaitingSignal) && (_inputProvider.enterTrigger.Value))
                .Subscribe(_ => QuickDraw());
        }

        private void QuickDraw()
        {
            if (!tempFlag)//もしも、合図がでてないのに剣を抜いたら
            {
                _playerState.canQuickDraw.Value = false;
                StartCoroutine(DelayClass.DelayCoroutin(180, () => _playerState.canQuickDraw.Value = true));

            } else {
                //仮
                temp.OnClickedTempButton();
            }
        }

    }
}


