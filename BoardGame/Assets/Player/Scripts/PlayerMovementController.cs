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
            context.Register<float, Transform, Action>(PlayerMovementEvents.Move, Move);
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

        private void Move(float distance, Transform pieceTransform, Action callback)
        {
            coroutineQueue.Enqueue(MoveCoroutine(distance, pieceTransform, callback));
        }

        private IEnumerator MoveCoroutine(float distance, Transform pieceTransform, Action callback)
        {
            transform.forward = -pieceTransform.forward;
            var initialPosition = transform.position;
            var piecePosition = pieceTransform.position;
            piecePosition.y = initialPosition.y;
            var t = 0.0f;
            while (t < 1f)
            {
                t = Mathf.Clamp01(t + Time.deltaTime / duration);
                transform.localPosition = Vector3.Lerp(initialPosition, piecePosition, t);
                yield return null;
            }

            transform.localPosition = piecePosition;
            callback?.Invoke();
        }
    }
}
