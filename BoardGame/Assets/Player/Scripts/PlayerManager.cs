using System;
using System.Collections.Generic;
using System.Linq;
using Board.Scripts;
using Events;
using General.EventManager;
using UnityEngine;

namespace Player.Scripts
{
    public class PlayerManager: MonoBehaviour
    {
        private readonly List<PlayerController> playerList = new List<PlayerController>();
        private PlayerController currentPlayer;
        private BoardPieceData currentPiece;
        private float distanceToMove = 3.3f;

        private void Awake()
        {
            EventManager.Register<PlayerController>(PlayerManagerEvents.AddPlayer, AddPlayer);
            EventManager.Register<PlayerController>(PlayerManagerEvents.RemovePlayer, RemovePlayer);
            EventManager.Register<int>(PlayerManagerEvents.StartMove, StartMove);
            EventManager.Register(PlayerManagerEvents.GoToNextPlayer, GoToNextPlayer);
            EventManager.Register(PlayerManagerEvents.PositionPlayers, PositionPlayers);
            EventManager.Register(PlayerManagerEvents.ShowPlayerCurrentPiece, ShowPlayerCurrentPiece);
        }

        private void OnDestroy()
        {
            EventManager.Unregister<PlayerController>(PlayerManagerEvents.AddPlayer, AddPlayer);
            EventManager.Unregister<PlayerController>(PlayerManagerEvents.RemovePlayer, RemovePlayer);
            EventManager.Unregister<int>(PlayerManagerEvents.StartMove, StartMove);
            EventManager.Unregister(PlayerManagerEvents.GoToNextPlayer, GoToNextPlayer);
            EventManager.Unregister(PlayerManagerEvents.PositionPlayers, PositionPlayers);
            EventManager.Unregister(PlayerManagerEvents.ShowPlayerCurrentPiece, ShowPlayerCurrentPiece);
        }

        private void AddPlayer(PlayerController player)
        {
            if (!playerList.Contains(player))
            {
                playerList.Add(player);
                player.transform.SetParent(transform);
            }
            if (currentPlayer == default)
                SetCurrentPlayer(player);
        }
        
        private void RemovePlayer(PlayerController player)
        {
            if (playerList.Contains(player))
            {
                playerList.Add(player);
                Destroy(player.gameObject);
            }
        }

        private void StartMove(int count)
        {
            var moveForward = count > 0;
            var movementNumber = Mathf.Abs(count);
            while (movementNumber > 1)
            {
                Move(moveForward);
                movementNumber--;
            }

            Move(moveForward, ShowPlayerCurrentPiece);
        }

        private void StartMoveByConsequence(int count)
        {
            var moveForward = count > 0;
            var movementNumber = Mathf.Abs(count);
            while (movementNumber > 1)
            {
                Move(moveForward);
                movementNumber--;
            }

            Move(moveForward, GoToNextPlayer);
            Debug.Log(currentPlayer.Piece);
        }
        
        private void Move(bool moveForward, Action callback = null)
        {
            Transform piece = null;
            currentPlayer.Piece += moveForward ? 1 : -1;
            EventManager.Trigger<int, Action<Transform>>(BoardEvents.GetBoardPiece, currentPlayer.Piece,
                (pieceTransform) => piece = pieceTransform);
            currentPlayer.Move(distanceToMove, piece, () => callback?.Invoke());
        }
        
        private void GoToNextPlayer()
        {
            var index = playerList.IndexOf(currentPlayer);
            SetCurrentPlayer(playerList[(index + 1) % playerList.Count]);
            EventManager.Trigger(CameraEvents.SetReferenceTransform, currentPlayer.transform);
            EventManager.Trigger(DiceEvents.SetRollDiceInputAction);
        }

        private void PositionPlayers()
        {
            var playersTransform = playerList.Select(player => player.transform).ToList();
            EventManager.Trigger(BoardEvents.PositionPlayers, playersTransform);
        }

        private void SetCurrentPlayer(PlayerController player)
        {
            currentPlayer = player;
            EventManager.Trigger(CameraEvents.SetReferenceTransform, player.transform);
        }

        private void ShowPlayerCurrentPiece()
        {
            EventManager.Trigger<int, Action<BoardPieceData>>(BoardEvents.GetBoardPieceData, currentPlayer.Piece,
                (data) => currentPiece = data);
            EventManager.Trigger(MainBoardPieceEvents.Setup, currentPiece.Texture, currentPiece.Description);
            EventManager.Trigger(MainBoardPieceEvents.Show);
            EventManager.Trigger<string, Action>(TurnEvents.SetupInputAction,"Toque para continuar",  OnFinishMove);
        }

        private void OnFinishMove()
        {
            EventManager.Trigger(MainBoardPieceEvents.Hide);
            if (currentPiece.SpacesToMove > 0)
                StartMoveByConsequence(currentPiece.SpacesToMove);
            else
                GoToNextPlayer();
        }
    }
}