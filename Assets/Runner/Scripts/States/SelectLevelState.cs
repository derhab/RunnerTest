using IState = Runner.Core.IState;

namespace Runner.States
{
    /// <summary>
    /// Стейт экрана настроек и старта игры
    /// </summary>
    public class SelectLevelState : GameState, IState
    {
        private readonly int _levelIndex;

        public SelectLevelState(LevelsManager levelsManager) : base(levelsManager) { }

        public void Enter()
        {
            levelsManager.LoadSettingsScene();
        }

        public void Exit() { }
    }
}