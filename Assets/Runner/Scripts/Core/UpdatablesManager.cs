using System.Collections.Generic;
using Zenject;

namespace Runner.Core
{
    /// <summary>
    /// Управляет списком сущностей, которые реализуют интерфейс <see cref="IUpdatable"/>>
    /// Добавляет и удаляет сущность из списка по необходимости.
    /// </summary>
    public class UpdatablesManager : ITickable
    {
        private readonly List<IUpdatable> _updatables = new();

        public void Add(IUpdatable entity)
        {
            if (!_updatables.Contains(entity))
            {
                _updatables.Add(entity);
            }
        }
        
        public void Remove(IUpdatable entity)
        {
            if (_updatables.Contains(entity))
            {
                _updatables.Remove(entity);
            }
        }
        
        public void Tick()
        {
            if (_updatables.Count == 0)
                return;
            foreach (var entity in _updatables)
                entity.OnUpdate();
        }
    }
}