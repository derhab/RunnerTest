using System;
using System.Collections.Generic;
using DG.Tweening;
using Runner.Core;
using Runner.Level.Behaviours.Character;
using Runner.Level.Player;
using UnityEngine;

namespace Runner.Level
{
    /// <summary>
    /// Менеджер бафов
    /// Создает и инстанцирует объекты бафов, следит за столкновением их с персонажем игрока, применяет эффекты и удаляет их
    /// </summary>
    public class BehavioursManager : IUpdatable, IDisposable
    {
        private List<ICharacterBehaviour> _behaviours;

        private const float _distToMissedBehaviourView = 5f;
        
        private readonly LevelContextInstaller.LevelConfig _config;
        private readonly PlayerResolver _playerResolver;
        
        private ICharacter _player;
        private ICharacterBehaviour _currentBehaviour;
        private int _index;

        public BehavioursManager(LevelContextInstaller.LevelConfig config, PlayerResolver playerResolver)
        {
            _config = config;
            _playerResolver = playerResolver;
        }
        
        public void Initialze()
        {
            _behaviours = new List<ICharacterBehaviour>
            {
                new DecelerateCharacterBehaviour(_config.GetCharacterBehaviourConfig<DecelerateCharacterBehaviourConfig>()),
                new AccelerateCharacterBehaviour(_config.GetCharacterBehaviourConfig<AccelerateCharacterBehaviourConfig>()),
                new FlyingCharacterBehaviour(_config.GetCharacterBehaviourConfig<FlyingCharacterBehaviourConfig>()),
            };
            
            _player = _playerResolver.Resolve();
            _currentBehaviour = GetNextBuffObject();
        }

        private ICharacterBehaviour GetNextBuffObject()
        {
            var result = _behaviours[_index];
            var position = new Vector3(0, 5, _player.Position.z) + Vector3.forward * _config.GetBehaviourSpawnDist;
            result.CreateView(position, Quaternion.identity, _config.BaseViewContainer);
            result.View.transform.DOMoveY(0, .5f)
                .SetEase(Ease.OutBack);
            
            _index = _index + 1 >= _behaviours.Count ? 0 : _index + 1;
            return result;
        }

        public void OnUpdate()
        {
            if(_currentBehaviour.View == null)
                return;

            var buffViewPosition = _currentBehaviour.View.transform.position;
            var dist = Vector3.Distance(_player.Position, buffViewPosition);
            if (dist <= _currentBehaviour.Config.ViewInteractableRadius && !_currentBehaviour.IsApplied)
            {
                
                _currentBehaviour.ApplyTo(_player);
                _currentBehaviour.EndEffect += OnEndBehaviourEffect;
            }

            if (!_currentBehaviour.IsApplied && _player.Position.z > buffViewPosition.z && dist > _distToMissedBehaviourView)
            {
                _currentBehaviour.RemoveView();
                _currentBehaviour.EndEffect -= OnEndBehaviourEffect;
                _currentBehaviour = GetNextBuffObject();
            }
        }

        private void OnEndBehaviourEffect()
        {
            _currentBehaviour.EndEffect -= OnEndBehaviourEffect;
            _currentBehaviour = GetNextBuffObject();
        }

        public void Dispose()
        {
            if (_currentBehaviour != null)
            {
                _currentBehaviour.EndEffect -= OnEndBehaviourEffect;
                _currentBehaviour.Dispose();
            }
        }
    }
}