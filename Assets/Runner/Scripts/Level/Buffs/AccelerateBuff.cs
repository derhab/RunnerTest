using Runner.Level.Player;

namespace Runner.Level.Buffs
{
    /// <summary>
    /// Эффект ускорения персонажа
    /// </summary>
    public class AccelerateBuff : BuffObject<AccelerateBuffConfig>
    {
        private float _speed;
        private float _animatorSpeed;
        
        public AccelerateBuff(AccelerateBuffConfig data) : base(data) { }

        public override void ApplyBehaviourTo(ICharacter character)
        {
            base.ApplyBehaviourTo(character);
            
            _animatorSpeed = character.GetAnimator().speed;
            
            character.ChangeRunSpeed(data.AccelerateMultiplier, out _speed);
            character.ChangeAnimationSpeed(data.AccelerateMultiplier, out _animatorSpeed);
        }
        
        public override void DisposeBehaviour()
        {
            base.DisposeBehaviour();
            character.SetRunSpeed(_speed);
            character.SetAnimationSpeed(_animatorSpeed);
        }
    }
}