using System;
using System.Linq;
using AYellowpaper;
using AYellowpaper.SerializedCollections;
using Runner.Core;
using Runner.Level;
using Runner.Level.Buffs;
using Runner.Level.Platform;
using Runner.Level.Player;
using UnityEngine;
using Zenject;

/// <summary>
/// Инсталлер зависимостей в контексте сцены игры
/// </summary>
public class LevelContextInstaller : MonoInstaller
{
    [SerializeField] private LevelConfig _levelConfig;

    public override void InstallBindings()
    {
        Container.Bind<RuntimePrefabsFactory>()
            .AsSingle()
            .NonLazy();
        
        Container.Bind<PlayerResolver>()
            .AsSingle()
            .NonLazy();
        
        Container.BindInterfacesAndSelfTo<UpdatablesManager>().AsSingle();
        Container.BindInterfacesAndSelfTo<ClientInput>().AsSingle();
        Container.BindInterfacesAndSelfTo<LevelInitializer>().AsSingle();
        Container.BindInterfacesAndSelfTo<BuffsManager>().AsSingle();

        Container.BindInstance(_levelConfig).AsSingle();
    }

    /// <summary>
    /// Конфиг уровня
    /// </summary>
    [Serializable]
    public class LevelConfig
    {
        [SerializeField] 
        private InterfaceReference<ICharacter, MonoBehaviour> _playerRef;
        
        [SerializeField] 
        private Transform _gameContainer;
        
        [SerializeField] 
        private Platform _platform;

        [Space(10)]
        [Header("Дистанция на которой спавнится вью нового бафа")]
        [SerializeField] 
        private int _buffSpawnDist;
        
        [Space(5)]
        [SerializeField] private SerializedDictionary<BuffTypes, BuffConfig> _buffConfigs;

        public MonoBehaviour Player => _playerRef.Value as MonoBehaviour;
        public Transform BaseViewContainer => _gameContainer;
        public Platform Platform => _platform;
        
        public int GetBuffSpawnDist => _buffSpawnDist;
        
        public T GetConfig<T>() where T : BuffConfig => (T) _buffConfigs.Values.FirstOrDefault(x => x is T);
    }
}