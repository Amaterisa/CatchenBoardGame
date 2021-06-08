using System;
using Events;
using General.EventManager;
using UnityEngine;

namespace TextFieldPopup.Scripts
{
    public class TextFieldPopupController : MonoBehaviour
    {
        [SerializeField] private TextFieldPopupView view;

        private void Awake()
        {
            EventManager.Register(TextFieldPopupEvents.Show, Show);
            EventManager.Register(TextFieldPopupEvents.Hide, Hide);
        }

        private void Start()
        {
            view.HideInstantly();
            view.SetConfirmClickListener(ConfirmClick);
            view.SetCancelClickListener(CancelClick);
        }

        private void OnDestroy()
        {
            EventManager.Unregister(TextFieldPopupEvents.Show, Show);
            EventManager.Unregister(TextFieldPopupEvents.Hide, Hide);
        }

        private void Show()
        {
            view.Show();
            EventManager.Trigger(GameConfigurationMenuEvents.SetButtonsInteractable, false);
        }

        private void Hide()
        {
            view.Hide();
            EventManager.Trigger(GameConfigurationMenuEvents.SetButtonsInteractable, true);
        }

        private void CancelClick()
        {
            Hide();
            view.ClearTextField();
        }

        private void ConfirmClick()
        {
            Hide();
            EventManager.Trigger(PlayerCreationEvents.ConfirmPlayerCreation, view.GetText());
            view.ClearTextField();
        }
    }
}
