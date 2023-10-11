using Runner.Level.Player;

namespace Runner.Level.Buffs
{
    /// <summary>
    /// Эффект замедления персонажа
    /// </summary>
    public class DecelerateBuff : BuffObject<DecelerateBuffConfig>
    {
        private float _speed;
        private float _animatorSpeed;

        public DecelerateBuff(DecelerateBuffConfig data) : base(data) { }

        public override void ApplyBehaviourTo(ICharacter character)
        {
            base.ApplyBehaviourTo(character);
            
            _animatorSpeed = character.GetAnimator().speed;
            
            character.ChangeRunSpeed(data.DecelerateMultiplier, out _speed);
            character.ChangeAnimationSpeed(data.DecelerateMultiplier, out _animatorSpeed);
        }
        
        public override void DisposeBehaviour()
        {
            base.DisposeBehaviour();
            character.SetRunSpeed(_speed);
            character.SetAnimationSpeed(_animatorSpeed);
        }
    }
}