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
            EventManager.Register(FadeScreenEvents.SetAlpha, SetAlpha);
            EventManager.Register(FadeScreenEvents.HideInstantly, HideInstantly);
        }
        
        private void OnDestroy()
        {
            EventManager.Unregister(FadeScreenEvents.Show, Show);
            EventManager.Unregister(FadeScreenEvents.Hide, Hide);
            EventManager.Unregister(FadeScreenEvents.SetAlpha, SetAlpha);
            EventManager.Unregister(FadeScreenEvents.HideInstantly, HideInstantly);
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

        private void HideInstantly()
        {
            view.HideInstantly();
        }

        private void SetAlpha()
        {
            var alpha = 0.5f;
            view.SetAlpha(alpha);
        }
    }
}
