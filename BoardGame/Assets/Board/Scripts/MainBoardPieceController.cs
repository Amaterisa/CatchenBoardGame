using System;
using Events;
using General.EventManager;
using UnityEngine;

namespace Board.Scripts
{
    public class MainBoardPieceController : MonoBehaviour
    {
        [SerializeField] private BoardPieceView view;
        [SerializeField] private float distanceToCamera = 2f;

        private void Awake()
        {
            EventManager.Register(MainBoardPieceEvents.Show, Show);
            EventManager.Register(MainBoardPieceEvents.Hide, Hide);
            EventManager.Register<Texture, string>(MainBoardPieceEvents.Setup, Setup);
        }

        private void OnDestroy()
        {
            EventManager.Unregister(MainBoardPieceEvents.Show, Show);
            EventManager.Unregister(MainBoardPieceEvents.Hide, Hide);
            EventManager.Unregister<Texture, string>(MainBoardPieceEvents.Setup, Setup);
        }

        private void LateUpdate()
        {
            SetPositionAndRotationToCamera();
        }

        private void Setup(Texture texture, string text)
        {
            view.SetTexture(texture);
            view.SetText(text);
        }

        private void Show()
        {
            view.Show();
            SetPositionAndRotationToCamera();
        }

        private void Hide()
        {
            view.HideInstantly();
        }

        private void SetPositionAndRotationToCamera()
        {
            var cameraTransform = Camera.main.transform;
            transform.position = cameraTransform.position + cameraTransform.forward * distanceToCamera;
            transform.rotation = cameraTransform.rotation;
        }
    }
}
