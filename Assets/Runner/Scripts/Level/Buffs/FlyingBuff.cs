using Runner.Level.Player;
using UnityEngine;

namespace Runner.Level.Buffs
{
    /// <summary>
    /// Эффект полета персонажа
    /// </summary>
    public class FlyingBuff : BuffObject<FlyingBuffConfig>
    {
        private Animator _animator;
        public FlyingBuff(FlyingBuffConfig data) : base(data) { }

        public override void ApplyBehaviourTo(ICharacter character)
        {
            base.ApplyBehaviourTo(character);
            
            _animator = character.GetAnimator();
            _animator.SetBool(Player.Player.Flying, true);
        }
        
        public override void DisposeBehaviour()
        {
            base.DisposeBehaviour();
            _animator.SetBool(Player.Player.Flying, false);
        }
    }
}