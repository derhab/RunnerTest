using Runner.Level.Player;

namespace Runner.Level.Behaviours.Character
{
    /// <summary>
    /// Поведение замедления персонажа
    /// </summary>
    public class DecelerateCharacterBehaviour : BaseChangeCharacterSpeedBehaviour<DecelerateCharacterBehaviourConfig>
    {
        public DecelerateCharacterBehaviour(DecelerateCharacterBehaviourConfig data) : base(data) { }

        public override void ApplyTo(ICharacter character)
        {
            base.ApplyTo(character);
            ChangeRunSpeed(data.DecelerateMultiplier);
            ChangeAnimationSpeed(data.DecelerateMultiplier);
        }
    }
}