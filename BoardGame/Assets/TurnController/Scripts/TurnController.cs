using System;
using Events;
using General.EventManager;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TurnController.Scripts
{
    public class TurnController : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private TurnView view;
        private Action inputAction;
        private bool canReceiveInput;

        private void Awake()
        {
            EventManager.Register<bool>(TurnEvents.CanReceiveInput, CanReceiveInput);
            EventManager.Register<Action>(TurnEvents.SetInputAction, SetInputAction);
            EventManager.Register<string>(TurnEvents.SetText, SetText);
            EventManager.Register(TurnEvents.Show, view.ShowInstantly);
        }

        private void Start()
        {
            view.HideInstantly();
        }

        private void OnDestroy()
        {
            EventManager.Unregister<bool>(TurnEvents.CanReceiveInput, CanReceiveInput);
            EventManager.Unregister<Action>(TurnEvents.SetInputAction, SetInputAction);
            EventManager.Unregister<string>(TurnEvents.SetText, SetText);
            EventManager.Unregister(TurnEvents.Show, view.ShowInstantly);
        }

        private void CanReceiveInput(bool canReceive)
        {
            canReceiveInput = canReceive;
        }

        private void SetInputAction(Action action)
        {
            inputAction = action;
        }

        private void SetText(string text)
        {
            view.SetText(text);
        }

        private void HandlePointerDown()
        {
            inputAction?.Invoke();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (canReceiveInput)
                HandlePointerDown();
        }
    }
}
