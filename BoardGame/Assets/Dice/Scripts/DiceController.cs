﻿using System;
using Events;
using General.EventManager;
using General.View;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Dice.Scripts
{
    public class DiceController : MonoBehaviour
    {
        [SerializeField] private View3D view;
        [SerializeField] private Rigidbody rb;
        [SerializeField] private Vector3 offset = new Vector3(0, -0.5f, 3f);
        private float delay = 0.5f;

        private void Awake()
        {
            EventManager.Register(DiceEvents.Show, Show);
            EventManager.Register(DiceEvents.Hide, Hide);
        }

        private void OnDestroy()
        {
            EventManager.Unregister(DiceEvents.Show, Show);
            EventManager.Unregister(DiceEvents.Hide, Hide);
        }

        private void Start()
        {
            view.HideInstantly();
            EventManager.Trigger<Action>(TurnEvents.SetInputAction, OnInputDown);
        }

        private void OnInputDown()
        {
            Show();
            EventManager.Trigger(TurnEvents.SetText, "");
        }

        private void Update()
        {
            Reset();
        }

        private void Show()
        {
            Reset();
            view.Show();
            CancelInvoke();
            Invoke(nameof(RollDice), delay);
        }

        private void Hide()
        {
            view.Hide();
        }
        
        private void RollDice()
        {
            rb.transform.localPosition = Vector3.zero;
            var dirX = Random.Range(0, 500f);
            var dirY = Random.Range(0, 500f);
            var dirZ = Random.Range(0, 500f);
            rb.AddForce(transform.up * 800);
            rb.AddTorque(dirX, dirY, dirZ);
        }

        private void Reset()
        {
            transform.rotation = Quaternion.identity;
            var cameraTransform = Camera.main.transform;
            transform.position = cameraTransform.position + offset.y * cameraTransform.up +
                                 offset.z * cameraTransform.forward;
        }
    }
}