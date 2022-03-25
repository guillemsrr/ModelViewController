using System;
using System.Collections.Generic;
using System.Linq;
using Mvc.Scripts.Controllers.Interfaces;
using Mvc.Scripts.Models.Interfaces;
using Mvc.Scripts.Signals;
using UnityEngine;

namespace Mvc.Scripts.Installers
{
    public abstract class Installer : ScriptableObject
    {
        private SignalHub _signalHub;
        private Dictionary<string, IModel> _models;
        private Dictionary<string, IModel> _installedModels = new Dictionary<string, IModel>();
        private List<ISignal> _signals = new List<ISignal>();

        public void Install(SignalHub signalHub, Dictionary<string, IModel> models)
        {
            _signalHub = signalHub;
            _models = models;

            Install();
        }

        protected abstract void Install();
        public abstract void Uninstall();

        protected void InstallModel<T>(string id = null) where T: IModel
        {
            Type interfaceType = typeof(T);
            Type implementationType = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .FirstOrDefault(type => interfaceType.IsAssignableFrom(type) && !type.IsInterface);

            if (implementationType == null)
            {
                throw new Exception("This model interface is not implemented!");
            }

            IModel modelInstance = Activator.CreateInstance(implementationType) as IModel;

            string modelId = string.IsNullOrEmpty(id) ? implementationType.ToString() : id;

            if (_models.ContainsKey(modelId))
            {
                Debug.LogWarning("This model already is installed!!!");
                return;
            }
            
            _models.Add(modelId, modelInstance);
            _installedModels.Add(modelId, modelInstance);
        }

        protected void InstallModel<T, U>(string id) where T : IModel where U : IModel, new()
        {
            Type interfaceType = typeof(T);
            Type implementationType = typeof(U);
            bool implementationTypeisValid = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Any(type => interfaceType.IsAssignableFrom(type) && type == implementationType && !type.IsInterface);

            if (!implementationTypeisValid)
            {
                throw new Exception("This model doesn't implement the interface!!");
            }
            
            IModel modelInstance = Activator.CreateInstance(implementationType) as IModel;

            string modelId = string.IsNullOrEmpty(id) ? implementationType.ToString() : id;

            if (_models.ContainsKey(modelId))
            {
                Debug.LogWarning("This model already is installed!!!");
                return;
            }
            
            _models.Add(modelId, modelInstance);
            _installedModels.Add(modelId, modelInstance);
        }

        protected void InstallAction<T, U>() where T : ASignal, new() where U : IController, new()
        {
            ASignal signal = _signalHub.AddSignal<T>();
            signal.AddListener(() => new U().Execute());
            _signals.Add(signal);
        }

        protected void InstallAction<T, U, V>() where T : ASignal<V>, new() where U : IController<V>, new() 
        {
            ASignal<V> signal = _signalHub.AddSignal<T>();
            signal.AddListener( poco => new U().Execute(poco));
            _signals.Add(signal);
        }

        protected void InstallNotification<T>() where T : ASignal, new()
        {
            ASignal signal = _signalHub.AddSignal<T>();
            _signals.Add(signal);
        }
        
        protected void InstallNotification<T, U>() where T : ASignal<U>, new()
        {
            ASignal<U> signal = _signalHub.AddSignal<T>();
            _signals.Add(signal);
        }
        
        protected void UninstallModels()
        {
            foreach (var model in _installedModels)
            {
                _models.Remove(model.Key);
            }

            _installedModels.Clear();
        }

        protected void UninstallSignals()
        {
            foreach (ISignal signal in _signals)
            {
                _signalHub.RemoveSignal(signal);
            }
            
            _signals.Clear();
        }
    }
}