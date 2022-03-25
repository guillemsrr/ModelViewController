using System;
using Mvc.Scripts.Contexts;
using Mvc.Scripts.Signals;
using Mvc.Scripts.Views.Interfaces;
using UnityEngine;

namespace Mvc.Scripts.Views
{
    public abstract class View : MonoBehaviour, ISignalView
    {
        public void AddSignalListener<T>(Action action) where T : ASignal, new()
        {
            Context.Instance.GetSignal<T>().AddListener(action);
        }

        public void AddSignalListener<T, U>(Action<U> action) where T : ASignal<U>, new()
        {
            Context.Instance.GetSignal<T>().AddListener(action);
        }
        
        public void RemoveSignalListener<T>(Action action) where T: ASignal, new()
        {
            try
            {
                Context.Instance.GetSignal<T>().RemoveListener(action);
            }
            catch
            {
                Debug.LogWarning($"{typeof(T)} has been destroyed already");
            }
        }
        
        public void RemoveSignalListener<T, U>(Action<U> action) where T : ASignal<U>, new()
        {
            try
            {
                Context.Instance.GetSignal<T>().RemoveListener(action);
            }
            catch
            {
                Debug.LogWarning($"{typeof(T)} has been destroyed already");
            }
        }

        public void SendAction<T>() where T : ASignal, new()
        {
            Context.Instance.GetSignal<T>().Dispatch();
        }

        public void SendAction<T, U>(U payload) where T : ASignal<U>, new()
        {
            Context.Instance.GetSignal<T>().Dispatch(payload);
        }
    }
}