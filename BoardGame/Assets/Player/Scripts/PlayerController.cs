using System;
using Events;
using General.EventManager;
using UnityEngine;

namespace Player.Scripts
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private ContextEventManager context;
        [SerializeField] private PlayerProperties properties;
        [SerializeField] private PlayerView view;

        public int Piece
        {
            get => properties.Piece;
            set => properties.Piece = value;
        }
        
        public bool CanPlay
        {
            get => properties.CanPlay;
            set => properties.CanPlay = value;
        }

        private void LateUpdate()
        {
            view.RotateNameToCamera();
        }

        public void StartMove(int count, Action callback)
        {
            context.Trigger(PlayerMovementEvents.StartMove, count, callback);
        }
        
        public void GoToPosition(Transform point)
        {
            context.Trigger(PlayerMovementEvents.GoToPosition, point);
        }

        public void SetName(string text)
        {
            view.SetName(text);
        }
        
        public string GetName()
        {
            return view.GetName();
        }

        public void SetColor(Color color)
        {
            view.SetColor(color);
        }
    }
}
