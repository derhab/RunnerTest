using UnityEngine;

namespace Runner.Level.Buffs
{
    [CreateAssetMenu(fileName = "DecelerateBuffConfig", menuName = "Runner/BuffConfigs/DecelerateBuff", order = 0)]
    public class DecelerateBuffConfig : BuffConfig
    {
        [Header("Speed decelerate multiplier")]
        [SerializeField] protected float _decelerateMultiplier;

        public float DecelerateMultiplier => _decelerateMultiplier;
    }
}