namespace Runner.Core
{
    /// <summary>
    /// Интрефейс сущности, которую нужно апдейтить каждый фрейм
    /// </summary>
    public interface IUpdatable
    {
        void OnUpdate();
    }
}