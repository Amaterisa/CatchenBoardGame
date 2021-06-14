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
            EventManager.Register<string, Action>(TurnEvents.SetupInputAction, SetupInputAction);
            EventManager.Register<string>(TurnEvents.SetText, SetText);
            EventManager.Register<string>(TurnEvents.SetCurrentPlayer, SetCurrentPlayer);
            EventManager.Register(TurnEvents.Show, view.ShowInstantly);
        }

        private void Start()
        {
            view.HideInstantly();
        }

        private void OnDestroy()
        {
            EventManager.Unregister<bool>(TurnEvents.CanReceiveInput, CanReceiveInput);
            EventManager.Unregister<string, Action>(TurnEvents.SetupInputAction, SetupInputAction);
            EventManager.Unregister<string>(TurnEvents.SetText, SetText);
            EventManager.Unregister<string>(TurnEvents.SetCurrentPlayer, SetCurrentPlayer);
            EventManager.Unregister(TurnEvents.Show, view.ShowInstantly);
        }

        private void CanReceiveInput(bool canReceive)
        {
            canReceiveInput = canReceive;
        }

        private void SetupInputAction(string text, Action action)
        {
            CanReceiveInput(true);
            SetText(text);
            inputAction = action;
        }

        private void SetText(string text)
        {
            view.SetText(text);
        }
        
        private void SetCurrentPlayer(string text)
        {
            view.SetPlayerName(text);
        }

        private void HandlePointerDown()
        {
            CanReceiveInput(false);
            inputAction?.Invoke();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (canReceiveInput)
                HandlePointerDown();
        }
    }
}
