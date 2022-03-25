using System;
using Mvc.Scripts.POCOS;
using Mvc.Scripts.Signals;
using UnityEngine;
using UnityEngine.UI;

namespace Mvc.Scripts.Views
{
    public class TaskCreatorView: View
    {
        [SerializeField] private GameObject _gameObject;
        [SerializeField] private InputField _titleText;
        [SerializeField] private InputField _descriptionText;
        [SerializeField] private GameObject _errorObject;
        
        private void Start()
        {
            AddSignalListener<SaveTaskPressedNotification>(Save);
            AddSignalListener<ErrorSavingTaskNotification>(ShowError);
        }

        public void Open()
        {
            HideError();
            _gameObject.SetActive(true);
        }
        
        public void Close()
        {
            ClearInputs();
            _gameObject.SetActive(false);
        }

        public void EditTask(TaskIdNotificationPoco taskNotificationPoco)
        {
            _titleText.text = taskNotificationPoco.Title;
            _descriptionText.text = taskNotificationPoco.Description;
        }

        private void Save()
        {
            TaskActionPoco taskActionPoco = new TaskActionPoco(_titleText.text, _descriptionText.text);
            SendAction<SaveTaskAction,TaskActionPoco>(taskActionPoco);
        }

        private void ShowError()
        {
            _errorObject.SetActive(true);
        }

        private void HideError()
        {
            _errorObject.SetActive(false);
        }

        private void ClearInputs()
        {
            _titleText.text = String.Empty;
            _descriptionText.text = String.Empty;
        }

        private void OnDestroy()
        {
            RemoveSignalListener<SaveTaskPressedNotification>(Save);
            RemoveSignalListener<ErrorSavingTaskNotification>(ShowError);
        }
    }
}