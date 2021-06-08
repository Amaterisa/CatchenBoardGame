using System;
using System.Collections;
using System.Collections.Generic;
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
        private readonly Queue<IEnumerator> coroutineQueue = new Queue<IEnumerator> ();

        private void Awake()
        {
            context.Register<float, Vector3, Action>(PlayerMovementEvents.Move, Move);
        }
        
        void Start()
        {
            StartCoroutine(CoroutineCoordinator());
        }
        
        IEnumerator CoroutineCoordinator()
        {
            while (true)
            {
                while (coroutineQueue.Count >0)
                    yield return StartCoroutine(coroutineQueue.Dequeue());
                yield return null;
            }
        }

        private void Move(float distance, Vector3 forward, Action callback)
        {
            coroutineQueue.Enqueue(MoveCoroutine(distance, forward, callback));
        }

        private IEnumerator MoveCoroutine(float distance, Vector3 forward, Action callback)
        {
            transform.forward = -forward;
            var initialPosition = transform.localPosition;
            var finalZ = initialPosition.z + distance;
            var finalPosition = new Vector3(initialPosition.x, initialPosition.y, finalZ);
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
