
namespace Runner.States
{
    /// <summary>
    /// Базовый класс стейта игры
    /// </summary>
    public abstract class GameState
    {
        protected readonly LevelsManager levelsManager;

        protected GameState() { }

        protected GameState(LevelsManager levelsManager)
        {
            this.levelsManager = levelsManager;
        }
    }
}