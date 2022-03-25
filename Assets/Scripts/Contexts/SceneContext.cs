using System.Collections.Generic;
using System.Net;
using Mvc.Scripts.Installers;
using UnityEngine;

namespace Mvc.Scripts.Contexts
{
    public class SceneContext : MonoBehaviour
    {
        [SerializeField] protected Installer[] _installers;
        [SerializeField] protected List<ScriptableObject> _scriptableObjects;
        
        [Header("Global context for contents")]
        [SerializeField] protected Context _context;

        private void Awake()
        {
            GuardContextIsNull();
          
            Context.Instance.AddScriptableObjectsToContextDictionary(_scriptableObjects);
            
            for (int i = 0; i < _installers.Length; i++)
            {
                _installers[i].Install(Context.Instance.SignalHub, Context.Instance.Models);
            }

        }

        private void GuardContextIsNull()
        {
            if (_context != null)
            {
                Context globalContext = FindObjectOfType<Context>();

                if (globalContext == null)
                {
                    Instantiate(_context);
                }
            }
        }

        private void OnDestroy()
        {
            Context.Instance.RemoveScriptableObjectsFromDictionary(_scriptableObjects);
            for (int i = 0; i < _installers.Length; i++)
            {
                _installers[i].Uninstall();
            }
        }
    }
}