using System;
using UnityEngine;
using Zenject;

namespace Runner.Core
{
    /// <summary>
    /// Клиентский ввод, отслеживает горизонтальное перемещение курсора/тача
    /// </summary>
    public class ClientInput : IInitializable, IUpdatable
    {
        public event Action<float> MouseXDeltaChanged;
        
        private readonly UpdatablesManager _updatablesManager;

        public ClientInput(UpdatablesManager updatablesManager)
         {
             _updatablesManager = updatablesManager;
         }
        
         public void Initialize()
         {
             _updatablesManager.Add(this);
         }
        
        public void OnUpdate()
        {
            if (Input.GetButton("Fire1"))
            {
                var delta = Input.GetAxis("Mouse X");
                if(delta != 0)
                    MouseXDeltaChanged?.Invoke(delta);
            }
        }
    }
}