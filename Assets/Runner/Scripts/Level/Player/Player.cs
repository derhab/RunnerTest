using System;
using System.Collections.Generic;
using Runner.Core;
using Runner.Level.Behaviours.Character;
using UnityEngine;
using Zenject;

namespace Runner.Level.Player
{
    /// <summary>
    /// Класс персонажа игрока
    /// </summary>
    public class Player : MonoBehaviour, ICharacter, IUpdatable
    {
        public static readonly int Speed = Animator.StringToHash("speed");
        public static readonly int Flying = Animator.StringToHash("flying");
        
        private const float _xOffsetMult = 20f;
        private const int _xMaxOffset = 3;

        [SerializeField] private Animator _animator;
        
        public Vector3 Position => transform.position;
        public List<ICharacterBehaviour> Behaviours { get; } = new();

        private float _speed;
        private ClientInput _clientInput;
        private LevelContextInstaller.LevelConfig _config;
        private DefaultCharacterBehaviour _defaultBehaviour;

        [Inject]
        private void Inject(ClientInput сlientInput, LevelContextInstaller.LevelConfig config)
        {
            _clientInput = сlientInput;
            _config = config;
        }
        
        public void Initialize()
        {
            _clientInput.MouseXDeltaChanged += OnMouseXDeltaChanged;
            CreateDefaultBehaviour();
            _defaultBehaviour.ApplyTo(this);
        }

        private void OnDestroy()
        {
            _clientInput.MouseXDeltaChanged -= OnMouseXDeltaChanged;
        }

        public Animator GetAnimator() => _animator;
        
        public void SetRunSpeed(float value)
        {
            _animator.SetFloat(Speed, value);
            _speed = value;
        }

        public void SetAnimationSpeed(float value) => _animator.speed = value;
        
        public float GetRunSpeed() => _speed;

        public float GetAnimationSpeed() => _animator.speed;
        
        public void AddBehaviour(ICharacterBehaviour behaviour)
        {
            if (!behaviour.IsApplied || behaviour is DefaultCharacterBehaviour) 
                return;

            _defaultBehaviour?.DisposeBehaviour();
            Behaviours.Add(behaviour);
        }
        
        public void RemoveBehaviour(ICharacterBehaviour behaviour)
        {
            if(Behaviours.Contains(behaviour))
                Behaviours.Remove(behaviour);
            
            if (Behaviours.Count == 0)
                _defaultBehaviour.ApplyTo(this);
        }

        private void CreateDefaultBehaviour()
        {
            var config = _config.GetCharacterBehaviourConfig<DefaultCharacterBehaviourConfig>();
            _defaultBehaviour = new DefaultCharacterBehaviour(config);
        }

        private void OnMouseXDeltaChanged(float delta)
        {
            var v = delta > 0 ? Vector3.right : Vector3.left;
            var newPos = v * _xOffsetMult * Time.deltaTime;
            var pos = transform.position;
            
            if (pos.x < -_xMaxOffset)
            {
                transform.position = new Vector3(-_xMaxOffset, 0, pos.z);
                return;
            }

            if (pos.x > _xMaxOffset)
            {
                transform.position = new Vector3(_xMaxOffset, 0, pos.z);
                return;
            }
            
            transform.Translate(newPos);
        }
        
        public void OnUpdate()
        {
            transform.position += Vector3.forward * _speed * Time.deltaTime;
        }
    }
}