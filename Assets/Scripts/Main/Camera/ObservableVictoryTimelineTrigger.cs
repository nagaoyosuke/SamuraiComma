using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UniRx;

namespace SamuraiComma.Main.Camera
{
    /// <summary>
    /// 2019/10/30 toyoda
    /// GameState.Finished時に再生されるタイムライン。
    /// GameStateManagerで観察する。
    /// </summary>

    public class ObservableVictoryTimelineTrigger : MonoBehaviour,ITimeControl
    {
        [SerializeField] private const int _startLoopingAnimTime = 11;
        [SerializeField] private BoolReactiveProperty _isFinishedDirection = new BoolReactiveProperty(false);
        [SerializeField] private BoolReactiveProperty _isLoopingPlayerAnim = new BoolReactiveProperty(false);

        public IReadOnlyReactiveProperty<bool> isFinishedDirection => _isFinishedDirection;
        public IReadOnlyReactiveProperty<bool> isLoopingPlayerAnim => _isLoopingPlayerAnim;



        public void OnControlTimeStart()
        {
            _isFinishedDirection.SetValueAndForceNotify(false);
        }

        public void OnControlTimeStop()
        {
            _isFinishedDirection.SetValueAndForceNotify(true);
        }

        public void SetTime(double time)
        {
            if (time > _startLoopingAnimTime)
                _isLoopingPlayerAnim.SetValueAndForceNotify(true);
        }

    }
}