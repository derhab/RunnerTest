using Runner;
using Runner.Core;
using Zenject;

/// <summary>
/// Инсталлер зависимостей в констексте проекта
/// </summary>
public class ProjectContextInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<LevelsManager>()
            .AsSingle()
            .NonLazy();
        
        Container.BindInterfacesAndSelfTo<GameManager>()
            .AsSingle()
            .NonLazy();
        
        Container.BindInterfacesAndSelfTo<GameStateMachine>()
            .AsSingle()
            .NonLazy();
        
    }
}