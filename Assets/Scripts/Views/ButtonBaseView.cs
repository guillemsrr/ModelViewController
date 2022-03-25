using System;
using Mvc.Scripts.Signals;
using UnityEngine;
using UnityEngine.UI;

namespace Mvc.Scripts.Views
{
    public abstract class ButtonBaseView<T>: View where T:ASignal, new()
    {
        [SerializeField] private Button _button;
        
        private void Start()
        {
            _button.onClick.AddListener(SendAction);
        }

        private void SendAction()
        {
            SendAction<T>();
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(SendAction);
        }
    }
}