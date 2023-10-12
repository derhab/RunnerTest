using Runner.Level.Player;

namespace Runner.Level.Behaviours.Character
{
    public class DefaultCharacterBehaviour : BaseCharacterBehaviour<DefaultCharacterBehaviourConfig>
    {
        public DefaultCharacterBehaviour(DefaultCharacterBehaviourConfig data) : base(data) { }

        public override void ApplyTo(ICharacter character)
        {
            base.ApplyTo(character);
            character.SetRunSpeed(data.DefaultRunSpeed);
            character.SetAnimationSpeed(data.DefaultAnimationSpeed);
            character.GetAnimator().SetBool(Player.Player.Flying, false);
        }
    }
}