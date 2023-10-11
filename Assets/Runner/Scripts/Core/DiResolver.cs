using System;
using System.Collections.Generic;
using Zenject;

namespace Runner.Core
{
    /// <summary>
    /// Абстрактный резолвер-обертка для получения injected экземпляров
    /// </summary>
    public abstract class DiResolver<T> where T : class
    {
        private readonly DiContainer _diContainer;

        public DiResolver(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }
        
        public T Resolve() => _diContainer.Resolve<T>();
        
        public List<T> ResolveAll() => _diContainer.ResolveAll<T>();
    }
}