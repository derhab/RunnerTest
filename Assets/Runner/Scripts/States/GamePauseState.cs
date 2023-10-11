using Runner.Core;
using UnityEngine;

namespace Runner.States
{
    /// <summary>
    /// Стейт паузы в игре
    /// </summary>
    public class GamePauseState: GameState, IState
    {
        public void Enter()
        {
            Time.timeScale = 0;
        }

        public void Exit()
        {
            Time.timeScale = 1;
        }
    }
}