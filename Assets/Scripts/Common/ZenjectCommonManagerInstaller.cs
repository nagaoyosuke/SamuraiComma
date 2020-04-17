using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using SamuraiComma;
/// <summary>
/// zenjectのインストーラー
/// </summary>
public class ZenjectCommonManagerInstaller : MonoInstaller
{

    [SerializeField] private SaveDataManager saveDataManagerPrefab;

    public override void InstallBindings()
    {
        Container.Bind<SaveDataManager>().FromComponentInNewPrefab(saveDataManagerPrefab).AsSingle();
    }
}
