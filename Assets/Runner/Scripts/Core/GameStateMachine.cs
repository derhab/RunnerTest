namespace Runner.Core
{
    /// <summary>
    /// Стейт машина игры
    /// </summary>
    public class GameStateMachine
    {
        public IState Current { get; private set; }

        public void Initialize(IState state)
        {
            Current = state;
            Current.Enter();
        }

        public void ChangeState(IState state)
        {
            Current?.Exit();
            Current = state;
            Current.Enter();
        }

        public void ExitState()
        {
            Current?.Exit();
        }

        public override string ToString()
        {
            return $"[Current State: {Current}]";
        }
    }
}