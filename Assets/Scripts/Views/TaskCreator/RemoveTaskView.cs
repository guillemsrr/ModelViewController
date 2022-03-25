using Mvc.Scripts.Signals;
using UnityEngine;
using UnityEngine.UI;

namespace Mvc.Scripts.Views
{
    public class RemoveTaskView: View
    {
        [SerializeField] private GameObject _gameObject;
        [SerializeField] private Button _removeButton;
        [SerializeField] private Button _cancelButton;

        private void Start()
        {
            _removeButton.onClick.AddListener(RemoveButton);
            _cancelButton.onClick.AddListener(CancelButton);
        }

        private void RemoveButton()
        {
            SendAction<RemoveTaskAction>();
        }

        private void CancelButton()
        {
            SendAction<CancelRemoveTaskAction>();
        }

        public void Open()
        {
            _gameObject.SetActive(true);
        }

        public void Close()
        {
            _gameObject.SetActive(false);
        }
    }
}