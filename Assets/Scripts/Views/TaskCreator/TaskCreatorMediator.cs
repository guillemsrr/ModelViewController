using System;
using Mvc.Scripts.POCOS;
using Mvc.Scripts.Signals;
using UnityEngine;

namespace Mvc.Scripts.Views
{
    public class TaskCreatorMediator: View
    {
        [SerializeField] private TaskCreatorView _taskCreatorView;

        private void Start()
        {
            AddSignalListener<OpenTaskCreatorNotification>(_taskCreatorView.Open);
            AddSignalListener<CloseTaskCreatorNotification>(_taskCreatorView.Close);
            AddSignalListener<EditTaskNotification, TaskIdNotificationPoco>(EditTask);
        }

        private void EditTask(TaskIdNotificationPoco taskNotificationPoco)
        {
            _taskCreatorView.EditTask(taskNotificationPoco);
        }

        private void OnDestroy()
        {
            RemoveSignalListener<OpenTaskCreatorNotification>(_taskCreatorView.Open);
            RemoveSignalListener<CloseTaskCreatorNotification>(_taskCreatorView.Close);
            RemoveSignalListener<EditTaskNotification, TaskIdNotificationPoco>(EditTask);
        }
    }
}