using System;
using System.Collections;
using System.Collections.Generic;
using Board.Scripts;
using Events;
using General.EventManager;
using General.ParabolicCalculator;
using UnityEngine;

namespace Player.Scripts
{
    public class PlayerMovementController : MonoBehaviour
    {
        [SerializeField] private ContextEventManager context;
        [SerializeField] private PlayerProperties properties;
        [SerializeField] private float movementDuration = 0.5f;
        private float delayToCallback = 0.1f;
        private readonly Queue<IEnumerator> coroutineQueue = new Queue<IEnumerator> ();

        private void Awake()
        {
            context.Register<int, Action>(PlayerMovementEvents.StartMove, StartMove);
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
            coroutineQueue.Enqueue(MoveCoroutine(position, movementDuration / 2, false, false));
        }
        
        private void StartMove(int count, Action callback)
        {
            var moveForward = count > 0;
            var movementNumber = Mathf.Abs(count);
            while (movementNumber > 1)
            {
                Move(moveForward);
                movementNumber--;
            }

            Move(moveForward, callback);
        }
        
        private void Move(bool moveForward, Action callback = null)
        {
            Transform piece = null;
            properties.Piece += moveForward ? 1 : -1;
            EventManager.Trigger<int, Action<BoardPieceController>>(BoardEvents.GetBoardPiece, properties.Piece,
                pieceController => piece = pieceController.transform);
            Move(piece, () => callback?.Invoke());
        }

        private void Move(Transform pieceTransform, Action callback)
        {
            coroutineQueue.Enqueue(MoveCoroutine(pieceTransform, movementDuration, true, true, callback));
        }

        private IEnumerator MoveCoroutine(Transform pieceTransform, float duration, bool delay = true, bool setRotation = true, Action callback = null)
        {
            var initialPosition = transform.position;
            var piecePosition = pieceTransform.position;
            piecePosition.y = initialPosition.y;
            if (setRotation)
                SetRotation(initialPosition, piecePosition);

            var t = 0.0f;
            while (t < 1f)
            {
                t = Mathf.Clamp01(t + Time.deltaTime / duration);
                transform.localPosition = Vector3.Lerp(initialPosition, piecePosition, t);
                yield return null;
            }

            transform.localPosition = piecePosition;
            if (delay)
                yield return new WaitForSeconds(delayToCallback);
            callback?.Invoke();
        }

        private void SetRotation(Vector3 initialPos, Vector3 finalPos)
        {
            transform.forward = initialPos - finalPos;
        }
    }
}
