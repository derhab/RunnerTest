using System;
using Runner.Core;
using Runner.States;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Runner.UI
{
    /// <summary>
    /// Кнопка на экране игры, позволяет вернуться на стартовый экран для выбора уровня и доп. настроек (в текущей версии не реализовано)
    /// </summary>
    public class RestartGameButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        
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
            _button.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            _button.onClick.RemoveListener(OnClick);
            _gameStateMachine.ChangeState(new SelectLevelState(_levelsManager));
        }
    }
}