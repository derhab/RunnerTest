using System;
using Runner.Level.Player;
using UnityEngine;

namespace Runner.Level.Behaviours.Character
{
    /// <summary>
    /// Интерфейс поведения, накладываемого на персонажа игрока
    /// </summary>
    public interface ICharacterBehaviour : IDisposable
    {
        event Action EndEffect;
        
        void CreateView(Vector3 position, Quaternion rotation, Transform container = null);
        
        /// <summary>
        /// Удалить визуал
        /// </summary>
        void RemoveView();

        GameObject View { get; }

        CharacterBehaviourConfig Config { get; }
        
        /// <summary>
        /// Был ли эффект применен
        /// </summary>
        bool IsApplied { get; }

        /// <summary>
        /// Удалить эффект
        /// </summary>
        void DisposeBehaviour();

        /// <summary>
        /// Применить эффект
        /// </summary>
        /// <param name="character"></param>
        void ApplyTo(ICharacter character);
    }
}