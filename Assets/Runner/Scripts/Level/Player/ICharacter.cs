using System.Collections.Generic;
using Runner.Level.Behaviours.Character;
using UnityEngine;

namespace Runner.Level.Player
{
    /// <summary>
    /// Интерфейс персонажа 
    /// </summary>
    public interface ICharacter
    {
        void Initialize();
        
        Vector3 Position { get; }

        Animator GetAnimator();
        
        List<ICharacterBehaviour> Behaviours { get; }

        void AddBehaviour(ICharacterBehaviour behaviour); 
        
        void RemoveBehaviour(ICharacterBehaviour behaviour); 
        
        /// <summary>
        /// Установка скорости персонажа
        /// </summary>
        /// <param name="value"></param>
        void SetRunSpeed(float value);

        /// <summary>
        /// Установка скорости анимации модели персонажа
        /// </summary>
        /// <param name="value"></param>
        void SetAnimationSpeed(float value);
        
        /// <summary>
        /// Получить скорость бега персонажа
        /// </summary>
        /// <returns></returns>
        float GetRunSpeed();
        
        /// <summary>
        /// Получить скорость воспроизведения анимации модели персонажа
        /// </summary>
        /// <returns></returns>
        float GetAnimationSpeed();
    }
}