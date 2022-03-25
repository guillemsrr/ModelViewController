using Mvc.Scripts.Contexts;
using Mvc.Scripts.Models.Interfaces;
using UnityEngine;

namespace Mvc.Scripts.Models
{
    public abstract class Model : Notifier, IModel
    {
        protected T GetScriptableObject<T>() where T : ScriptableObject
        {
            return (T)Context.Instance.GetScriptableObject<T>();
        }
    }
}