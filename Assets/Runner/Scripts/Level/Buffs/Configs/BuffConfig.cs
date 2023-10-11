using UnityEngine;

namespace Runner.Level.Buffs
{
    /// <summary>
    /// Конфиг объекта эффекта
    /// </summary>
    public abstract class BuffConfig : ScriptableObject
    {
        [Header("Effect duration in seconds")]
        [SerializeField] 
        protected int effectDuration;

        [Header("Reference of view")]
        [SerializeField]
        private GameObject _viewRef;
        
        [Header("Interactable radius of view")]
        [SerializeField]
        private float _viewInteractableRadius;
        
        public int EffectDuration => effectDuration;

        public GameObject ViewRef => _viewRef;
        
        public float ViewInteractableRadius => _viewInteractableRadius;
    }
}