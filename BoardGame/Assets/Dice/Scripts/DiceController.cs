using System;
using System.Collections;
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
        [SerializeField] private DiceCollisionHandler collisionHandler;
        [SerializeField] private Rigidbody rb;
        [SerializeField] private Vector3 offset = new Vector3(0, -0.5f, 3f);
        private float delay = 0.5f;
        private bool rolling;

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
            collisionHandler.TriggerStay = HandleCollision;
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
            StopAllCoroutines();
            StartCoroutine(DelayCoroutine(RollDice));
        }

        private void Hide()
        {
            view.Hide();
        }
        
        private IEnumerator DelayCoroutine(Action action)
        {
            yield return new WaitForSeconds(delay);
            action?.Invoke();
        }
        
        private void RollDice()
        {
            rolling = true;
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

        private void HandleCollision(GameObject side)
        {
            if (!rolling)
                return;
            var velocity = rb.velocity;
            var sideNumber = 0;
            if (velocity == Vector3.zero)
            {
                switch (side.name)
                {
                    case "Side1":
                        sideNumber = 6;
                        break;
                    case "Side2":
                        sideNumber = 5;
                        break;
                    case "Side3":
                        sideNumber = 4;
                        break;
                    case "Side4":
                        sideNumber = 3;
                        break;
                    case "Side5":
                        sideNumber = 2;
                        break;
                    case "Side6":
                        sideNumber = 1;
                        break;
                }
                FinishCollision(sideNumber);
            }
        }

        private void FinishCollision(int side)
        {
            rolling = false;
            StopAllCoroutines();
            StartCoroutine(DelayCoroutine(Hide));
            EventManager.Trigger(PlayerManagerEvents.StartMove, side);
        }
    }
}
