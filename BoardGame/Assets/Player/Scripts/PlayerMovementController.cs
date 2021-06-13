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
        [SerializeField] private float movementDuration = 0.5f;
        private float delayToCallback = 0.1f;
        private readonly Queue<IEnumerator> coroutineQueue = new Queue<IEnumerator> ();

        private void Awake()
        {
            context.Register<Transform, Action>(PlayerMovementEvents.Move, Move);
            context.Register<Transform>(PlayerMovementEvents.GoToPosition, GoToPosition);
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

        private void GoToPosition(Transform position)
        {
            
            coroutineQueue.Enqueue(GoToPositionCoroutine(position, movementDuration / 2));
        }
        
        private IEnumerator GoToPositionCoroutine(Transform pieceTransform, float duration)
        {
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
        }

        private void Move(Transform pieceTransform, Action callback)
        {
            coroutineQueue.Enqueue(MoveCoroutine(pieceTransform, movementDuration, callback));
        }

        private IEnumerator MoveCoroutine(Transform pieceTransform, float duration, Action callback = null)
        {
            var initialPosition = transform.position;
            var piecePosition = pieceTransform.position;
            piecePosition.y = initialPosition.y;
            SetRotation(initialPosition, piecePosition);

            var t = 0.0f;
            while (t < 1f)
            {
                t = Mathf.Clamp01(t + Time.deltaTime / duration);
                transform.localPosition = Vector3.Lerp(initialPosition, piecePosition, t);
                yield return null;
            }

            transform.localPosition = piecePosition;
            yield return new WaitForSeconds(delayToCallback);
            callback?.Invoke();
        }

        private void SetRotation(Vector3 initialPos, Vector3 finalPos)
        {
            transform.forward = initialPos - finalPos;
        }
    }
}
