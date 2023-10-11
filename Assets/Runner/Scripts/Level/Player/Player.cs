using System;
using System.Collections.Generic;
using Runner.Core;
using Runner.Level.Buffs;
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
        
        private const float _defaultSpeed = 3f;
        private const float _xOffsetMult = 20f;
        private const int _xMaxOffset = 3;

        [SerializeField] private Animator _animator;
        
        public Vector3 Position => transform.position;
        public readonly List<IBuffObject> Buffs = new();

        private float _speed;
        private ClientInput _clientInput;
        private IBuffObject _currentBuff;

        [Inject]
        private void Inject(ClientInput сlientInput)
        {
            _clientInput = сlientInput;
        }

        private void OnDestroy()
        {
            _clientInput.MouseXDeltaChanged -= OnMouseXDeltaChanged;
        }

        public Animator GetAnimator() => _animator;
        
        public void AddBuff(IBuffObject buff)
        {
            if(!buff.IsApplied)
                return;
            Buffs.Add(buff);
        }
        
        public void RemoveBuff(IBuffObject buff)
        {
            if(Buffs.Contains(buff))
                Buffs.Remove(buff);
        }

        public void SetRunSpeed(float value) => _speed = value;
        public void SetAnimationSpeed(float value) => _animator.speed = value;

        public void ChangeRunSpeed(float multiplier, out float currentSpeed)
        {
            currentSpeed = _speed;
            _speed *= multiplier;
        }

        public void ChangeAnimationSpeed(float multiplier, out float currentSpeed)
        {
            currentSpeed = _animator.speed;
            _animator.speed *= multiplier;
        }
        
        public void Initialize()
        {
            _clientInput.MouseXDeltaChanged += OnMouseXDeltaChanged;
            SetRunSpeed(_defaultSpeed);
            _animator.SetFloat(Speed, 1);
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