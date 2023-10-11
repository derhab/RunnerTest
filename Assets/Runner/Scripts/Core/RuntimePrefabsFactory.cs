using UnityEngine;
using Zenject;

namespace Runner.Core
{
    /// <summary>
    /// Фабрика префабов. Инкапсулирует в себе DiContainer
    /// Нужен для того, чтобы экземпляр префаба имел доступ к нужным зависимостям и биндился в контейнер
    /// </summary>
    public class RuntimePrefabsFactory
    {
        private readonly DiContainer _diContainer;

        protected RuntimePrefabsFactory(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }

        public T Create<T>(MonoBehaviour reference, Transform parent) where T : MonoBehaviour
        {
            var instance = _diContainer.InstantiatePrefabForComponent<T>(reference, parent);
            _diContainer.BindInstance(instance);
            return instance;
        }
    }
}