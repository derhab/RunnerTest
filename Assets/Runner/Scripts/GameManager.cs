using System;
using Runner.Core;
using Runner.States;
using UnityEngine;
using Zenject;

namespace Runner
{
    /// <summary>
    /// Является точкой входа
    /// Инициализирует игру, устанавливает стартовый стейт
    /// Следит за фокусом аппа и меняет соотв. стейт
    /// </summary>
    public class GameManager : IInitializable, IDisposable
    {
        private readonly LevelsManager _levelsManager;
        private readonly GameStateMachine _gameStateMachine;

        public GameManager(LevelsManager levelsManager, GameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
            _levelsManager = levelsManager;
        }
        
        public void Initialize()
        {
            Application.focusChanged += OnFocusChanged;
            _gameStateMachine.Initialize(new SelectLevelState(_levelsManager));
        }

        private void OnFocusChanged(bool focused)
        {
            if(_gameStateMachine.Current is SelectLevelState)
                return;

            var newState = !focused ? (IState)new GamePauseState() : new GamePlayState();
            _gameStateMachine.ChangeState(newState);
        }

        public void Dispose()
        {
            Application.focusChanged -= OnFocusChanged;
        }
    }
}