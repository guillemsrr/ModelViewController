using Mvc.Scripts.Controllers;
using Mvc.Scripts.Models;
using Mvc.Scripts.POCOS;
using Mvc.Scripts.Signals;
using Mvc.Scripts.Views;
using UnityEngine;

namespace Mvc.Scripts.Installers
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Installer/ModelsInstaller", fileName = "ModelsInstaller")]
    public class ToDoListInstaller: Installer
    {
        protected override void Install()
        {
            InstallActions();
            InstallModels();
            InstallNotifications();
        }

        private void InstallActions()
        {
            InstallAction<AddTaskAction, AddTaskController>();
            InstallAction<CloseTaskAction, CloseTaskController>();
            InstallAction<SaveTaskPressedAction, SaveTaskPressedController>();
            InstallAction<SaveTaskAction, SaveTaskController, TaskActionPoco>();
            InstallAction<TaskViewSelectAction, TaskEditorController, ViewID>();
            InstallAction<RemoveTaskPressedAction, RemoveTaskPressedController>();
            InstallAction<RemoveTaskAction, RemoveTaskController>();
            InstallAction<CancelRemoveTaskAction, CancelRemoveTaskController>();
        }

        private void InstallModels()
        {
            InstallModel<TaskCreatorModel>();
        }

        private void InstallNotifications()
        {
            InstallNotification<OpenTaskCreatorNotification>();
            InstallNotification<CloseTaskCreatorNotification>();
            InstallNotification<SaveTaskPressedNotification>();
            InstallNotification<SaveTaskNotification, TaskIdNotificationPoco>();
            InstallNotification<UpdateTaskNotification, TaskIdNotificationPoco>();
            InstallNotification<EditTaskNotification, TaskIdNotificationPoco>();
            InstallNotification<ErrorSavingTaskNotification>();
            InstallNotification<RemoveTaskNotification, ViewID>();
            InstallNotification<OpenRemoveTaskPanelNotification>();
            InstallNotification<CloseRemoveTaskPanelNotification>();
        }

        public override void Uninstall()
        {
            UninstallModels();
            UninstallSignals();
        }
    }
}