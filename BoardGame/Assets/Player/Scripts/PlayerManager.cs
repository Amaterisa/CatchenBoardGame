using System;
using System.Collections.Generic;
using System.Linq;
using Board.Scripts;
using Events;
using General.Consts;
using General.EventManager;
using UnityEngine;

namespace Player.Scripts
{
    public class PlayerManager: MonoBehaviour
    {
        private readonly List<PlayerController> playerList = new List<PlayerController>();
        private PlayerController currentPlayer;
        private BoardPieceData currentPiece;

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
            EventManager.Trigger<int, Action<BoardPieceController>>(BoardEvents.GetBoardPiece, currentPlayer.Piece,
                (pieceController) =>
                {
                    pieceController.RemovePlayer(currentPlayer.transform);
                    currentPlayer.GoToPosition(pieceController.transform);
                });
            Move(count, ShowPlayerCurrentPiece);
        }
        
        private void StartMoveByConsequence(int count)
        {
            Move(count, GoToNextPlayer);
        }

        private void Move(int count, Action callback)
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
            currentPlayer.Piece += moveForward ? 1 : -1;
            EventManager.Trigger<int, Action<BoardPieceController>>(BoardEvents.GetBoardPiece, currentPlayer.Piece,
                (pieceController) => piece = pieceController.transform);
            currentPlayer.Move(piece, () => callback?.Invoke());
        }
        
        private void GoToNextPlayer()
        {
            PositionPlayer(currentPlayer, currentPlayer.Piece);
            var index = playerList.IndexOf(currentPlayer);
            SetCurrentPlayer(playerList[(index + 1) % playerList.Count]);
            EventManager.Trigger(CameraEvents.SetReferenceTransform, currentPlayer.transform);
            EventManager.Trigger(DiceEvents.SetRollDiceInputAction);
        }

        private void PositionPlayers()
        {
            var playersTransform = playerList.Select(player => player.transform).ToList();
            EventManager.Trigger(BoardEvents.PositionPlayers, playersTransform);
            EventManager.Trigger<int, Action<BoardPieceController>>(BoardEvents.GetBoardPiece, 0,
                (pieceController) =>
                {
                    foreach (var player in playerList)
                    {
                        PositionPlayer(player, 0);
                    }
                });
        }

        private void PositionPlayer(PlayerController player, int piece)
        {
            EventManager.Trigger<int, Action<BoardPieceController>>(BoardEvents.GetBoardPiece, piece,
                (pieceController) =>
                {
                    pieceController.AddPlayer(player.transform);
                    var point = pieceController.GetPoint(player.transform);
                    player.GoToPosition(point);
                });
        }

        private void SetCurrentPlayer(PlayerController player)
        {
            currentPlayer = player;
            EventManager.Trigger(CameraEvents.SetReferenceTransform, player.transform);
            EventManager.Trigger(TurnEvents.SetCurrentPlayer, currentPlayer.GetName());
        }

        private void ShowPlayerCurrentPiece()
        {
            EventManager.Trigger<int, Action<BoardPieceData>>(BoardEvents.GetBoardPieceData, currentPlayer.Piece,
                (data) => currentPiece = data);
            EventManager.Trigger(MainBoardPieceEvents.Setup, currentPiece.Texture, currentPiece.Description, currentPlayer.Piece.ToString());
            EventManager.Trigger(MainBoardPieceEvents.Show);
            EventManager.Trigger<string, Action>(TurnEvents.SetupInputAction, Consts.TouchToContinue,  OnFinishMove);
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