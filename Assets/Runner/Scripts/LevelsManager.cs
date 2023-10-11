using System;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

namespace Runner
{
    /// <summary>
    /// Управляет загрузкой сцен уровней или юай сцены для выбора или рестарта уровня
    /// Используются addressables для того, чтобы можно было при необходимости легко перейти на ремоут загрузку
    /// </summary>
    public class LevelsManager
    {
        private const string levelPrefix = "Level_";
        
        public event Action SceneLoaded;
        public event Action<float> LevelLoadProgress;

        public Scene Current { get; private set; }

        /// <summary>
        /// Сцена выбора уровня и возможно, каких-то дополнительных настроек.
        /// В текущей реализации, т.к. уровень у нас только 1, в ui сцены будет только кнопка старта игры
        /// </summary>
        public async void LoadSettingsScene()
        {
            var operation = Addressables.LoadSceneAsync("SettingsScene", LoadSceneMode.Single, false);
            
            await UniTask.WaitUntil(() => operation.IsDone);
            await operation.Task.Result.ActivateAsync();
            
            Current = operation.Task.Result.Scene;
            SceneLoaded?.Invoke();
        }
        
        /// <summary>
        /// Загрузка уровня
        /// В случае загрузки ремоут уровней можно организовать отображение процессе закгрузки, подписывашись на LevelLoadProgress
        /// В текущей версии это не предусмотрено
        /// </summary>
        /// <param name="levelIndex"></param>
        public async void LoadLevel(int levelIndex)
        {
            var operation = Addressables.LoadSceneAsync($"{levelPrefix}{levelIndex.ToString()}", LoadSceneMode.Single, false);
            
            await UniTask.WaitUntil(() =>
            {
                var status = operation.GetDownloadStatus();
                LevelLoadProgress?.Invoke(status.Percent);
                return operation.IsDone;
            });
            
            await operation.Task.Result.ActivateAsync();
            
            Current = operation.Task.Result.Scene;
            SceneLoaded?.Invoke();
        }
    }
}