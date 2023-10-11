using Runner.Level.Buffs;
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

        void AddBuff(IBuffObject buff); 
        
        void RemoveBuff(IBuffObject buff); 

        /// <summary>
        /// Изменение скорости бега персонажа
        /// </summary>
        /// <param name="multiplier">коэффициент изменения скорости</param>
        /// <param name="currentSpeed">значение скорости до изменения</param>
        void ChangeRunSpeed(float multiplier, out float currentSpeed);
        
        /// <summary>
        /// Изменение скорости анимации модели персонажа
        /// </summary>
        /// <param name="multiplier">коэффициент изменения скорости</param>
        /// <param name="currentSpeed">значение скорости до изменения</param>
        void ChangeAnimationSpeed(float multiplier, out float currentSpeed);
        
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
    }
}