using Runner.Level.Player;
using UnityEngine;

namespace Runner.Level.Behaviours.Character
{
    /// <summary>
    /// Поведение полета персонажа
    /// </summary>
    public class FlyingCharacterBehaviour : BaseCharacterBehaviour<FlyingCharacterBehaviourConfig>
    {
        private Animator _animator;
        public FlyingCharacterBehaviour(FlyingCharacterBehaviourConfig data) : base(data) { }

        public override void ApplyTo(ICharacter character)
        {
            base.ApplyTo(character);
            
            _animator = character.GetAnimator();
            _animator.SetBool(Player.Player.Flying, true);
        }
        
        public override void DisposeBehaviour()
        {
            base.DisposeBehaviour();
            if(character.Behaviours.Count > 0)
                _animator.SetBool(Player.Player.Flying, false);
        }
    }
}