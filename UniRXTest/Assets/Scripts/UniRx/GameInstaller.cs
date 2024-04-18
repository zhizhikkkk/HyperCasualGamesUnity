using R3;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using Zenject;

public class GameInstaller : MonoInstaller
{
    public Health HealthPrefab;

    public override void InstallBindings()
    {
        Container.Bind<Health>().FromComponentInNewPrefab(HealthPrefab).AsSingle();
        Container.Bind<Player>().FromComponentInHierarchy().AsSingle();
    }
}
