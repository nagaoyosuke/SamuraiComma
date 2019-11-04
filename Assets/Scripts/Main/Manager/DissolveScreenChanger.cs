using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Zenject;

namespace SamuraiComma.Main.Manager
{
    /// <summary>
    /// 2019/11/04 toyoda
    /// 
    /// </summary>

    public class DissolveScreenChanger : MonoBehaviour
    {

        [SerializeField] private ScreenFader _screenFader;
        [Inject] private GameStateManager _gameStateManager;

        //仮
        [SerializeField] private TempData temp;


        void Start()
        {
            temp.tempserverflag
                             .DistinctUntilChanged()
                .Where(x => x == true)
                .Subscribe(_ =>_screenFader.isFadeOut = true);

            temp.tempserverflag
                .DistinctUntilChanged()
                .Where(x => x == false)
                .Subscribe(_ => _screenFader.isFadeIn = true);
        }
    }
}


