using System;
using Mvc.Scripts.Signals;
using UnityEngine;

namespace Mvc.Scripts.Views
{
    public class RemovePanelMediator : View
    {
        [SerializeField] private RemoveTaskView _removeTaskView;

        private void Start()
        {
            AddSignalListener<OpenRemoveTaskPanelNotification>(OpenRemoveTaskPanel);
            AddSignalListener<CloseTaskCreatorNotification>(CloseRemoveTaskPanel);
            AddSignalListener<CloseRemoveTaskPanelNotification>(CloseRemoveTaskPanel);
        }

        private void OpenRemoveTaskPanel()
        {
            _removeTaskView.Open();
        }
        
        private void CloseRemoveTaskPanel()
        {
            _removeTaskView.Close();
        }
    }
}