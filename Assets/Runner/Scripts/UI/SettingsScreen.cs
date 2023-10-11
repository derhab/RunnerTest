using System;
using Runner.Core;
using Runner.States;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Runner.UI
{
    /// <summary>
    /// Экран старта игры и доп. настроек, например выбор урповня (в текущей версии не реализовано)
    /// </summary>
    public class SettingsScreen : MonoBehaviour
    {
        [SerializeField] private Button _startButton;
        
        private GameStateMachine _gameStateMachine;
        private LevelsManager _levelsManager;

        [Inject]
        private void Inject(GameStateMachine gameStateMachine, LevelsManager levelsManager)
        {
            _gameStateMachine = gameStateMachine;
            _levelsManager = levelsManager;
        }

        private void Awake()
        {
            _startButton.onClick.AddListener(OnStartClick);
        }

        private void OnStartClick()
        {
            _startButton.onClick.RemoveListener(OnStartClick);
            _gameStateMachine.ChangeState(new GamePlayState(_levelsManager, 1));
        }
    }
}