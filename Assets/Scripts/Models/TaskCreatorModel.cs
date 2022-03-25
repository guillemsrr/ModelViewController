using System;
using System.Collections.Generic;
using Mvc.Scripts.POCOS;
using Mvc.Scripts.Signals;
using Mvc.Scripts.Views;

namespace Mvc.Scripts.Models
{
    public class TaskCreatorModel : Model
    {
        private int _lastTaskID;
        private readonly Dictionary<ViewID, TaskIdNotificationPoco> _savedTasks = new Dictionary<ViewID, TaskIdNotificationPoco>();
        
        /// <summary>
        /// TODO: CREATE STATES, this !null would be editing state 
        /// </summary>
        private ViewID _currentlyEditingTask;

        public void SaveTaskPressed()
        {
            SendNotification<SaveTaskPressedNotification>();
        }

        public void SaveTask(TaskActionPoco taskActionPoco)
        {
            if (string.IsNullOrEmpty(taskActionPoco.Title) && string.IsNullOrEmpty(taskActionPoco.Description))
            {
                SendNotification<CloseTaskCreatorNotification>();
                return;
            }

            if (!string.IsNullOrEmpty(taskActionPoco.Title))
            {
                if (_currentlyEditingTask==null)
                {
                    SaveNewTask(taskActionPoco);
                }
                else
                {
                    _savedTasks[_currentlyEditingTask] = new TaskIdNotificationPoco(_currentlyEditingTask.Id, taskActionPoco.Title, taskActionPoco.Description);
                    SendNotification<UpdateTaskNotification, TaskIdNotificationPoco>(_savedTasks[_currentlyEditingTask]);
                    _currentlyEditingTask = null;
                }
                
                SendNotification<CloseTaskCreatorNotification>();
                return;
            }

            if (!string.IsNullOrEmpty(taskActionPoco.Description))
            {
                SendNotification<ErrorSavingTaskNotification>();
            }
        }

        private void SaveNewTask(TaskActionPoco taskActionPoco)
        {
            TaskIdNotificationPoco taskIdNotificationPoco = CreateNewTask(taskActionPoco);
            SendNotification<SaveTaskNotification, TaskIdNotificationPoco>(taskIdNotificationPoco);
        }

        private TaskIdNotificationPoco CreateNewTask(TaskActionPoco taskActionPoco)
        {
            TaskIdNotificationPoco taskIdNotificationPoco = new TaskIdNotificationPoco(_lastTaskID, taskActionPoco.Title, taskActionPoco.Description);
            _savedTasks.Add(taskIdNotificationPoco.ViewID, taskIdNotificationPoco);
            _lastTaskID++;
            return taskIdNotificationPoco;
        }

        public void OpenRemovePanel()
        {
            SendNotification<OpenRemoveTaskPanelNotification>();
        }
        
        public void RemoveTask()
        {
            if (_currentlyEditingTask != null)
            {
                _savedTasks.Remove(_currentlyEditingTask);
                SendNotification<RemoveTaskNotification, ViewID>(_currentlyEditingTask);
                _currentlyEditingTask = null;
            }
            
            SendNotification<CloseTaskCreatorNotification>();
        }

        public void EditTask(ViewID viewId)
        {
            if (!_savedTasks.ContainsKey(viewId))
            {
                return;
            }
            
            _currentlyEditingTask = viewId;
            OpenTaskCreator();
            SendNotification<EditTaskNotification, TaskIdNotificationPoco>(_savedTasks[_currentlyEditingTask]);
        }

        public void OpenTaskCreator()
        {
            SendNotification<OpenTaskCreatorNotification>();
        }

        public void CloseTask()
        {
            SendNotification<CloseTaskCreatorNotification>();
        }

        public void CancelRemoveTask()
        {
            SendNotification<CloseRemoveTaskPanelNotification>();
        }
    }
}