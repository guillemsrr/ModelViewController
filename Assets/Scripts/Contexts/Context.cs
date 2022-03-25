using System;
using System.Collections.Generic;
using System.Linq;
using Mvc.Scripts.Installers;
using Mvc.Scripts.Models;
using Mvc.Scripts.Models.Interfaces;
using Mvc.Scripts.Signals;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Mvc.Scripts.Contexts
{
    public class Context : MonoBehaviour
    {
        [SerializeField] protected Installer[] _installers;
        [SerializeField] protected List<ScriptableObject> _scriptableObjects;

        private static Context _instance;

        private Dictionary<string, ScriptableObject> _scriptableObjectsDictionary;
        public static Context Instance => _instance;
        public Dictionary<string, IModel> Models { get; private set; }
        public SignalHub SignalHub { get; private set; }
        
        protected virtual void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(this);
                InitializeDictionaries();
                SignalHub = new SignalHub();
                Install();
            }
            else
            {
                Destroy(this);
            }
        }

        private void InitializeDictionaries()
        {
            _scriptableObjectsDictionary = new Dictionary<string, ScriptableObject>();
            AddScriptableObjectsToContextDictionary(_scriptableObjects);
            
            Models = new Dictionary<string, IModel>();
        }

        private void Install()
        {
            for (int i = 0; i < _installers.Length; i++)
            {
                _installers[i].Install(SignalHub, Models);
            }
        }
        public void AddScriptableObjectsToContextDictionary(List<ScriptableObject> scriptableObjects)
        {
            foreach (var scriptableObject in scriptableObjects)
            {
                _scriptableObjectsDictionary.Add(scriptableObject.GetType().ToString(), scriptableObject);
            }
        }

        public void RemoveScriptableObjectsFromDictionary(List<ScriptableObject> scriptableObjects)
        {
            foreach (var scriptableObject in scriptableObjects)
            {
                _scriptableObjectsDictionary.Remove(scriptableObject.GetType().ToString());
            }
        }

        public T GetModel<T>(string id = null) where T: class, IModel
        {
            Type interfaceType = typeof(T);
            Type implementationType = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .FirstOrDefault(type => interfaceType.IsAssignableFrom(type) && !type.IsInterface);

            if (implementationType == null)
            {
                throw new Exception("This model interface is not implemented!");
            }
            
            string modelId = string.IsNullOrEmpty(id) ? implementationType.ToString() : id;

            if (Models.ContainsKey(modelId))
            {
                return Models[modelId] as T;
            }

            throw new Exception("Model not found");
        }

        public T GetSignal<T>() where T: ISignal, new()
        {
            return SignalHub.GetSignal<T>();
        }

        public ScriptableObject GetScriptableObject<T>() where T : class
        {
            if (_scriptableObjectsDictionary.ContainsKey(typeof(T).ToString()))
            {
                return _scriptableObjectsDictionary[typeof(T).ToString()];
            }

            throw new Exception(string.Format("Scrpitable Object {0} is not installed", typeof(T)));
        }

        private void OnApplicationQuit()
        {
            Uninstall();
        }

        private void Uninstall()
        {
            for (int i = 0; i < _installers.Length; i++)
            {
                _installers[i].Uninstall();
            }
        }
    }
}
