using System;
using System.Collections;
using Events;
using General.Consts;
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
            EventManager.Register(DiceEvents.SetRollDiceInputAction, SetRollDiceInputAction);
        }

        private void OnDestroy()
        {
            EventManager.Unregister(DiceEvents.Show, Show);
            EventManager.Unregister(DiceEvents.Hide, Hide);
            EventManager.Unregister(DiceEvents.SetRollDiceInputAction, SetRollDiceInputAction);
        }

        private void Start()
        {
            view.HideInstantly();
            collisionHandler.TriggerStay = HandleCollision;
            SetRollDiceInputAction();
        }

        private void SetRollDiceInputAction()
        {
            EventManager.Trigger<string, Action>(TurnEvents.SetupInputAction, Consts.TouchToRollTheDice, OnInputDown);
        }

        private void OnInputDown()
        {
            Show();
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
            StartCoroutine(OnFinishRoll(side));
        }
        
        private IEnumerator OnFinishRoll(int count)
        {
            yield return new WaitForSeconds(delay);
            view.Hide(() => EventManager.Trigger(PlayerManagerEvents.StartMove, count));
        }
    }
}
