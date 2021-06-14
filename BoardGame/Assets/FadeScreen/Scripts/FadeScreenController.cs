using System;
using Events;
using General.EventManager;
using UnityEngine;

namespace FadeScreen.Scripts
{
    public class FadeScreenController : MonoBehaviour
    {
        [SerializeField] private FadeScreenView view;
        [SerializeField] private float duration = 0.5f;

        private void Awake()
        {
            EventManager.Register(FadeScreenEvents.Show, Show);
            EventManager.Register(FadeScreenEvents.Hide, Hide);
        }
        
        private void OnDestroy()
        {
            EventManager.Unregister(FadeScreenEvents.Show, Show);
            EventManager.Unregister(FadeScreenEvents.Hide, Hide);
        }

        private void OnEnable()
        {
            view.Hide();
        }

        private void Show()
        {
            view.Show();
            CancelInvoke();
            Invoke(nameof(Hide), duration);
        }

        private void Hide()
        {
            view.Hide();
        }
    }
}
