using Runner.Level.Player;

namespace Runner.Level.Behaviours.Character
{
    /// <summary>
    /// Поведение ускорения персонажа
    /// </summary>
    public class AccelerateCharacterBehaviour : BaseChangeCharacterSpeedBehaviour<AccelerateCharacterBehaviourConfig>
    {
        public AccelerateCharacterBehaviour(AccelerateCharacterBehaviourConfig data) : base(data) { }

        public override void ApplyTo(ICharacter character)
        {
            base.ApplyTo(character);
            ChangeRunSpeed(data.AccelerateMultiplier);
            ChangeAnimationSpeed(data.AccelerateMultiplier);
        }
    }
}