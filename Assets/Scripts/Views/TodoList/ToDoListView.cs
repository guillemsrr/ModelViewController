using Mvc.Scripts.POCOS;
using Mvc.Scripts.Signals;
using UnityEngine;

namespace Mvc.Scripts.Views
{
    public class ToDoListView: View
    {
        [SerializeField] private Transform _tasksParent;
        [SerializeField] private TaskView _taskViewTemplate;

        private void Start()
        {
            AddSignalListener<SaveTaskNotification, TaskIdNotificationPoco>(AddTask);
        }

        private void AddTask(TaskIdNotificationPoco taskNotificationPoco)
        {
            TaskView task = Instantiate(_taskViewTemplate, _tasksParent);
            task.UpdateTask(taskNotificationPoco);
        }

        private void OnDestroy()
        {
            RemoveSignalListener<SaveTaskNotification, TaskIdNotificationPoco>(AddTask);
        }
    }
}