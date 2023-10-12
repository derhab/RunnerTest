using UnityEngine;

namespace Runner.Level.Behaviours.Character
{
    [CreateAssetMenu(fileName = "DefaultCharacterBehaviourConfig", menuName = "Runner/BuffConfigs/DefaultCharacterBehaviourConfig", order = 0)]
    public class DefaultCharacterBehaviourConfig : CharacterBehaviourConfig
    {
        [Space(20)]
        [SerializeField] private float _defaultRunSpeed;
        [SerializeField] private float _defaultAnimationSpeed;

        public float DefaultRunSpeed => _defaultRunSpeed;
        public float DefaultAnimationSpeed => _defaultAnimationSpeed;
    }
}