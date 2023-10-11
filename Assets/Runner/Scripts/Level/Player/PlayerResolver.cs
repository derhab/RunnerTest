using Runner.Core;
using Zenject;

namespace Runner.Level.Player
{
    /// <summary>
    /// Резолвер персонажа игрока
    /// </summary>
    public class PlayerResolver : DiResolver<Player>
    {
        public PlayerResolver(DiContainer diContainer) : base(diContainer) { }
    }
}