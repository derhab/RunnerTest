namespace Runner.Level.Behaviours.Character
{
    /// <summary>
    /// Базовый класс для поведений меняющих скорость персонажа
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseChangeCharacterSpeedBehaviour<T> : BaseCharacterBehaviour<T> where T : CharacterBehaviourConfig
    {
        private float _speedBefore;
        private float _animatorSpeedBefore;
        
        protected BaseChangeCharacterSpeedBehaviour(T data) : base(data) { }

        /// <summary>
        /// Изменение скорости бега персонажа
        /// </summary>
        /// <param name="multiplier">коэффициент изменения скорости</param>
        protected void ChangeRunSpeed(float multiplier)
        {
            _speedBefore = character.GetRunSpeed();
            var newSpeed = _speedBefore * multiplier;
            character.SetRunSpeed(newSpeed);
        }

        /// <summary>
        /// Изменение скорости анимации модели персонажа
        /// </summary>
        /// <param name="multiplier">коэффициент изменения скорости</param>
        protected void ChangeAnimationSpeed(float multiplier)
        {
            _animatorSpeedBefore = character.GetAnimationSpeed();
            var animator = character.GetAnimator();
            var newSeed = animator.speed * multiplier;
            character.SetAnimationSpeed(newSeed);
        }
        
        public override void DisposeBehaviour()
        {
            base.DisposeBehaviour();
            
            if (character.Behaviours.Count > 0)
            {
                character.SetRunSpeed(_speedBefore);
                character.SetAnimationSpeed(_animatorSpeedBefore);
            }
        }
    }
}