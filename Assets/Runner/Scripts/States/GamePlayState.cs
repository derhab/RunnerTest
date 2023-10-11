using Runner.Core;

namespace Runner.States
{
    /// <summary>
    /// Стейт процесса игры
    /// </summary>
    public class GamePlayState : GameState, IState
    {
        private readonly int _levelIndex;
        private readonly bool _needLoadLevel;

        public GamePlayState(LevelsManager levelsManager, int levelIndex) : base(levelsManager)
        {
            _levelIndex = levelIndex;
            _needLoadLevel = true;
        }

        public GamePlayState() { }

        public void Enter()
        {
            if(!_needLoadLevel)
                return;
            
            levelsManager?.LoadLevel(_levelIndex);
        }

        public void Exit()
        {
            
        }
    }
}