using Mvc.Scripts.Contexts;
using Mvc.Scripts.Signals;

namespace Mvc.Scripts.Models
{
    public abstract class Notifier
    {
        protected void SendNotification<T>() where T : ASignal, new()
        {
            Context.Instance.GetSignal<T>().Dispatch();
        }

        protected void SendNotification<T, U>(U payload) where T : ASignal<U>, new()
        {
            Context.Instance.GetSignal<T>().Dispatch(payload);
        }
    }
}