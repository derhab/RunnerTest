namespace Runner.Core
{
    /// <summary>
    /// Интерфейс стейт машины
    /// </summary>
    public interface IState
    {
        void Enter();
        void Exit();
    }
}