using System;
using System.Collections.Generic;
using DG.Tweening;
using Runner.Core;
using Runner.Level.Buffs;
using Runner.Level.Player;
using UnityEngine;

namespace Runner.Level
{
    /// <summary>
    /// Менеджер бафов
    /// Создает и инстанцирует объекты бафов, следит за столкновением их с персонажем игрока, применяет эффекты и удаляет их
    /// </summary>
    public class BuffsManager : IUpdatable, IDisposable
    {
        private List<IBuffObject> _buffs;
        
        private readonly LevelContextInstaller.LevelConfig _config;
        private readonly PlayerResolver _playerResolver;
        private ICharacter _player;
        private IBuffObject _currentBuff;
        private int _index;

        public BuffsManager(LevelContextInstaller.LevelConfig config, PlayerResolver playerResolver)
        {
            _config = config;
            _playerResolver = playerResolver;
        }
        
        public void Initialze()
        {
            _buffs = new List<IBuffObject>
            {
                new DecelerateBuff(_config.GetConfig<DecelerateBuffConfig>()),
                new AccelerateBuff(_config.GetConfig<AccelerateBuffConfig>()),
                new FlyingBuff(_config.GetConfig<FlyingBuffConfig>()),
            };
            
            _player = _playerResolver.Resolve();
            _currentBuff = GetNextBuffObject();
        }

        private IBuffObject GetNextBuffObject()
        {
            var result = _buffs[_index];
            var position = new Vector3(0, 5, _player.Position.z) + Vector3.forward * _config.GetBuffSpawnDist;
            result.CreateView(position, Quaternion.identity, _config.BaseViewContainer);
            result.View.transform.DOMoveY(0, .5f)
                .SetEase(Ease.OutBack);
            
            _index = _index + 1 >= _buffs.Count ? 0 : _index + 1;
            return result;
        }

        public void OnUpdate()
        {
            if(_currentBuff.View == null)
                return;

            var buffViewPosition = _currentBuff.View.transform.position;
            var dist = Vector3.Distance(_player.Position, buffViewPosition);
            if (dist <= _currentBuff.Config.ViewInteractableRadius && !_currentBuff.IsApplied)
            {
                _currentBuff.ApplyBehaviourTo(_player);
                _currentBuff.EndEffect += OnEndBuffEffect;
            }

            if (!_currentBuff.IsApplied && _player.Position.z > buffViewPosition.z && dist > 5)
            {
                _currentBuff.RemoveView();
                _currentBuff.EndEffect -= OnEndBuffEffect;
                _currentBuff = GetNextBuffObject();
            }
        }

        private void OnEndBuffEffect()
        {
            _currentBuff.EndEffect -= OnEndBuffEffect;
            _currentBuff = GetNextBuffObject();
        }

        public void Dispose()
        {
            if (_currentBuff != null)
            {
                _currentBuff.EndEffect -= OnEndBuffEffect;
                _currentBuff.Dispose();
            }
        }
    }
}