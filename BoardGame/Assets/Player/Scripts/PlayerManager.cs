using System;
using System.Collections.Generic;
using System.Linq;
using Events;
using General.EventManager;
using UnityEngine;

namespace Player.Scripts
{
    public class PlayerManager: MonoBehaviour
    {
        private readonly List<PlayerController> playerList = new List<PlayerController>();
        private PlayerController currentPlayer;

        private void Awake()
        {
            EventManager.Register<PlayerController>(PlayerManagerEvents.AddPlayer, AddPlayer);
            EventManager.Register<PlayerController>(PlayerManagerEvents.RemovePlayer, RemovePlayer);
            EventManager.Register<float, Vector3, Action>(PlayerManagerEvents.Move, Move);
            EventManager.Register(PlayerManagerEvents.GoToNextPlayer, GoToNextPlayer);
            EventManager.Register(PlayerManagerEvents.PositionPlayers, PositionPlayers);
        }

        private void OnDestroy()
        {
            EventManager.Unregister<PlayerController>(PlayerManagerEvents.AddPlayer, AddPlayer);
            EventManager.Unregister<PlayerController>(PlayerManagerEvents.RemovePlayer, RemovePlayer);
            EventManager.Unregister<float, Vector3, Action>(PlayerManagerEvents.Move, Move);
            EventManager.Unregister(PlayerManagerEvents.GoToNextPlayer, GoToNextPlayer);
            EventManager.Unregister(PlayerManagerEvents.PositionPlayers, PositionPlayers);
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

        private void Move(float distance, Vector3 forward, Action callback)
        {
            currentPlayer.Move(distance, forward, callback);
        }
        
        private void GoToNextPlayer()
        {
            var index = playerList.IndexOf(currentPlayer);
            SetCurrentPlayer(playerList[(index + 1) % playerList.Count]);
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
    }
}