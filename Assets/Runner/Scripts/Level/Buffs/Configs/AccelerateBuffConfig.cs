using UnityEngine;

namespace Runner.Level.Buffs
{
    [CreateAssetMenu(fileName = "AccelerateBuffConfig", menuName = "Runner/BuffConfigs/AccelerateBuff", order = 0)]
    public class AccelerateBuffConfig : BuffConfig
    {
        [Header("Speed accelerate multiplier")]
        [SerializeField] protected float _accelerateMultiplier;
        
        public float AccelerateMultiplier => _accelerateMultiplier;
    }
}