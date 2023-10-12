using UnityEngine;

namespace Runner.Level.Behaviours.Character
{
    /// <summary>
    /// Конфиг объекта эффекта
    /// </summary>
    public abstract class CharacterBehaviourConfig : ScriptableObject
    {
        [Header("Effect duration in seconds")]
        [SerializeField] 
        protected int effectDuration;

        [Header("Reference of view")]
        [SerializeField]
        protected GameObject _viewRef;
        
        [Header("Interactable radius of view")]
        [SerializeField]
        protected float _viewInteractableRadius;
        
        public int EffectDuration => effectDuration;

        public GameObject ViewRef => _viewRef;
        
        public float ViewInteractableRadius => _viewInteractableRadius;
    }
}