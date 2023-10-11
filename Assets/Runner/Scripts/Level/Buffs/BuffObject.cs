using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Runner.Level.Player;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Runner.Level.Buffs
{
    /// <summary>
    /// Абстрактный класс эффекта накладываемого на персонажа
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BuffObject<T> : IBuffObject where T : BuffConfig
    {
        protected readonly T data;
        protected ICharacter character;
        protected GameObject view;
        
        public event Action EndEffect;
        
        public GameObject View => view;
        public BuffConfig Config => data;
        
        public bool IsApplied => _isApplied;
        
        private bool _isApplied;
        private CancellationTokenSource _cancelToken;

        protected BuffObject(T data)
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
        
        public virtual void ApplyBehaviourTo(ICharacter character)
        {
            this.character = character;
            AddBuffToCharacter();

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

            EnableTimer();
        }

        private async void EnableTimer()
        {
            _cancelToken?.Cancel();
            _cancelToken = new CancellationTokenSource();
            await UniTask.Delay(TimeSpan.FromSeconds(data.EffectDuration), cancellationToken:_cancelToken.Token);
            DisposeBehaviour();
        }

        public virtual void DisposeBehaviour()
        {
            RemoveBuffFromCharacter();
            character.RemoveBuff(this);
            EndEffect?.Invoke();
        }

        private void RemoveBuffFromCharacter()
        {
            _isApplied = false;
            character.RemoveBuff(this);
        }

        private void AddBuffToCharacter()
        {
            _isApplied = true;
            character.AddBuff(this);
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