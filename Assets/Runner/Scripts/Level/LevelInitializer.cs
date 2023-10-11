using System;
using Runner.Core;
using Runner.Level.Player;
using Zenject;

namespace Runner.Level
{
    /// <summary>
    /// Инициализирует левел, точка входа для запуска необходимых процессов на уровне
    /// </summary>
    public class LevelInitializer : IInitializable
    {
        private readonly LevelContextInstaller.LevelConfig _config;
        private readonly RuntimePrefabsFactory _prefabsFactory;
        private readonly UpdatablesManager _updatablesManager;
        private readonly BuffsManager _buffsManager;
        private ICharacter _player;

        public LevelInitializer(LevelContextInstaller.LevelConfig config, 
            UpdatablesManager updatablesManager,
            BuffsManager buffsManager,
            RuntimePrefabsFactory prefabsFactory)
        {
            _config = config;
            _updatablesManager = updatablesManager;
            _buffsManager = buffsManager;
            _prefabsFactory = prefabsFactory;
        }

        public void Initialize()
        {
            InitPlayer();
            InitPlatform();
            InitBuffsManager();
        }

        private void InitPlayer()
        {
            _player = _prefabsFactory.Create<Player.Player>(_config.Player, _config.BaseViewContainer);
            _player.Initialize();
            _updatablesManager.Add((IUpdatable)_player);
        }
        
        private void InitPlatform()
        {
            var platform = _config.Platform;
            platform.Initialize(_player);
            _updatablesManager.Add(platform);
        }
        
        private void InitBuffsManager()
        {
            _buffsManager.Initialze();
            _updatablesManager.Add(_buffsManager);
        }
    }
}