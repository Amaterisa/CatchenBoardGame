﻿using System;
using Events;
using General.EventManager;
using UnityEngine;

namespace Player.Scripts
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private ContextEventManager context;
        [SerializeField] private PlayerView view;

        private void Update()
        {
            view.RotateNameToCamera();
        }

        public void Move(float distance, Vector3 forward)
        {
            context.Trigger(PlayerMovementEvents.Move, distance, forward);
        }

        public void SetName(string text)
        {
            view.SetName(text);
        }

        public void SetColor(Color color)
        {
            view.SetColor(color);
        }
    }
}
