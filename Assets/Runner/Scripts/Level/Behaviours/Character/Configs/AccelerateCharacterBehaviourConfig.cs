using UnityEngine;

namespace Runner.Level.Behaviours.Character
{
    [CreateAssetMenu(fileName = "AccelerateBuffConfig", menuName = "Runner/BuffConfigs/AccelerateBuff", order = 0)]
    public class AccelerateCharacterBehaviourConfig : CharacterBehaviourConfig
    {
        [Header("Speed accelerate multiplier")]
        [SerializeField] protected float _accelerateMultiplier;
        
        public float AccelerateMultiplier => _accelerateMultiplier;
    }
}