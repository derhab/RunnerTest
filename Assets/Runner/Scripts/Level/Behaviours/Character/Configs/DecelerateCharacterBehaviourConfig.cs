using UnityEngine;

namespace Runner.Level.Behaviours.Character
{
    [CreateAssetMenu(fileName = "DecelerateBuffConfig", menuName = "Runner/BuffConfigs/DecelerateBuff", order = 0)]
    public class DecelerateCharacterBehaviourConfig : CharacterBehaviourConfig
    {
        [Header("Speed decelerate multiplier")]
        [SerializeField] protected float _decelerateMultiplier;

        public float DecelerateMultiplier => _decelerateMultiplier;
    }
}