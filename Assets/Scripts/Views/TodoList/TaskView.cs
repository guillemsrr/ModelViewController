using System;
using Mvc.Scripts.POCOS;
using Mvc.Scripts.Signals;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Mvc.Scripts.Views
{
    public class TaskView: View, IPointerClickHandler
    {
        [SerializeField] private GameObject _gameObject;
        [SerializeField] private Text _numberText;
        [SerializeField] private Text _titleText;
        [SerializeField] private Text _descriptionText;
        [SerializeField] private Button _temporalButton;

        private int _id;

        private void Start()
        {
            _temporalButton.onClick.AddListener(Select);
            AddSignalListener<UpdateTaskNotification, TaskIdNotificationPoco>(UpdateItself);
            AddSignalListener<RemoveTaskNotification, ViewID>(Remove);
        }

        private void UpdateItself(TaskIdNotificationPoco taskNotificationPoco)
        {
            if (taskNotificationPoco.ViewID.Id != _id)
            {
                return;
            }
            UpdateTask(taskNotificationPoco);
        }

        public void UpdateTask(TaskIdNotificationPoco taskNotificationPoco)
        {
            SetViewId(taskNotificationPoco.ViewID);
            SetTitle(taskNotificationPoco.Title);
            SetDescription(taskNotificationPoco.Description);
        }

        private void SetViewId(ViewID viewId)
        {
            _id = viewId.Id;
            _numberText.text = viewId.Id.ToString();
        }

        private void SetTitle(string title)
        {
            _titleText.text = title;
        }

        private void SetDescription(string description)
        {
            _descriptionText.text = description;
        }

        private void Select()
        {
            SendAction<TaskViewSelectAction, ViewID>(new ViewID(_id));
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Select();
        }
        
        private void Remove(ViewID viewId)
        {
            if (_id != viewId.Id)
            {
                return;
            }
            
            Destroy(_gameObject);
        }

        private void OnDestroy()
        {
            RemoveSignalListener<UpdateTaskNotification, TaskIdNotificationPoco>(UpdateItself);
            RemoveSignalListener<RemoveTaskNotification, ViewID>(Remove);
        }
    }
}