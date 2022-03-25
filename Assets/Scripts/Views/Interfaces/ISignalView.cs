using System;
using Mvc.Scripts.Signals;

namespace Mvc.Scripts.Views.Interfaces
{
    public interface ISignalView
    {
        void AddSignalListener<T>(Action action) where T : ASignal, new();
        void AddSignalListener<T, U>(Action<U> action) where T : ASignal<U>, new();

        void RemoveSignalListener<T>(Action action) where T : ASignal, new();
        void RemoveSignalListener<T, U>(Action<U> action) where T : ASignal<U>, new();

        void SendAction<T>() where T : ASignal, new();
        void SendAction<T, U>(U payload) where T : ASignal<U>, new();
    }
}