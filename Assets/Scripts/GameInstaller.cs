using UnityEngine;
using Zenject;
using Assets.Lib;

public class GameInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<IInputReader>().To<InputReader>().AsSingle();
        Container.BindInterfacesTo<TextDisplayer>().AsSingle();
    }
}