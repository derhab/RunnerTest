using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Runner.Level.Player;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Runner.Level.Behaviours.Character
{
    /// <summary>
    /// Абстрактный класс эффекта накладываемого на персонажа
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseCharacterBehaviour<T> : ICharacterBehaviour where T : CharacterBehaviourConfig
    {
        public event Action EndEffect;
        
        protected readonly T data;
        
        protected ICharacter character;
        protected GameObject view;
        
        public GameObject View => view;
        public CharacterBehaviourConfig Config => data;
        public bool IsApplied => _isApplied;
        
        private bool _isApplied;
        private CancellationTokenSource _cancelToken;

        protected BaseCharacterBehaviour(T data)
        {
            this.data = data;
        }

        public virtual void CreateView(Vector3 position, Quaternion rotation, Transform container = null)
        {
            if(data.ViewRef != null)
                view = Object.Instantiate(data.ViewRef, position, rotation, container);
        }
        
        public void RemoveView()
        {
            if(view != null)
                Object.Destroy(view);
        }
        
        public virtual void ApplyTo(ICharacter character)
        {
            this.character = character;
            AddBehaviourTo();

            if (view != null)
            {
                view.transform.DOScale(0, .25f)
                    .SetEase(Ease.OutSine)
                    .OnComplete(() =>
                    {
                        Object.Destroy(view);
                        view = null;
                    });
            }

            if (data.EffectDuration > 0)
                EnableTimer();
        }

        protected async void EnableTimer()
        {
            _cancelToken?.Cancel();
            _cancelToken = new CancellationTokenSource();
            await UniTask.Delay(TimeSpan.FromSeconds(data.EffectDuration), cancellationToken:_cancelToken.Token);
            DisposeBehaviour();
        }

        public virtual void DisposeBehaviour()
        {
            RemoveBehaviourFrom();
            EndEffect?.Invoke();
        }

        private void RemoveBehaviourFrom()
        {
            _isApplied = false;
            character.RemoveBehaviour(this);
        }

        private void AddBehaviourTo()
        {
            _isApplied = true;
            character.AddBehaviour(this);
        }
        
        public void Dispose()
        {
            _cancelToken?.Cancel();
        }

        public override string ToString()
        {
            return $"[View: {data.ViewRef}]";
        }
    }
}