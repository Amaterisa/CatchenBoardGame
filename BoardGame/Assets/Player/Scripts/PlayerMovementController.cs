using System;
using System.Collections;
using Events;
using General.EventManager;
using General.ParabolicCalculator;
using UnityEngine;

namespace Player.Scripts
{
    public class PlayerMovementController : MonoBehaviour
    {
        [SerializeField] private ContextEventManager context;
        [SerializeField] private float duration = 0.5f;
        private Coroutine moveCoroutine;

        private void Awake()
        {
            context.Register<float, Vector3, Action>(PlayerMovementEvents.Move, Move);
        }

        private void OnDestroy()
        {
            context.Register<float, Vector3, Action>(PlayerMovementEvents.Move, Move);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                var finalPos = transform.localPosition - transform.forward * 2f;
                var rot = transform.forward;
                StartCoroutine(MoveCoroutine(transform.localPosition, finalPos, null));
            }
        }

        private void Move(float distance, Vector3 forward, Action callback)
        {
            transform.forward = forward;
            var initialPosition = transform.localPosition;
            var finalX = initialPosition.x + distance;
            var finalPosition = new Vector3(finalX, initialPosition.y, initialPosition.z);
            if (moveCoroutine != null)
                StopCoroutine(moveCoroutine);
            moveCoroutine = StartCoroutine(MoveCoroutine(initialPosition, finalPosition, callback));
        }

        private IEnumerator MoveCoroutine(Vector3 initialPosition, Vector3 finalPosition, Action callback)
        {
            var k = ParabolicCalculator.CalculateParabolicConstant(initialPosition.x, finalPosition.x);
            var t = 0.0f;
            while (t < 1f)
            {
                t = Mathf.Clamp01(t + Time.deltaTime / duration);
                //TODO: refine parabolic calculator equation
                transform.localPosition = Vector3.Lerp(initialPosition, finalPosition, t);
                //transform.localPosition = ParabolicCalculator.ParabolicFunction(initialPosition, finalPosition, 
                    //transform.localPosition, k, t);
                yield return null;
            }

            transform.localPosition = finalPosition;
            callback?.Invoke();
        }
    }
}
