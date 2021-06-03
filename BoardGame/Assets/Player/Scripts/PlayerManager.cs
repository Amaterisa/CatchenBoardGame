using System;
using System.Collections.Generic;
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
            EventManager.Register<float, Vector3>(PlayerManagerEvents.Move, Move);
            EventManager.Register<PlayerController>(PlayerManagerEvents.SetCurrentPlayer, SetCurrentPlayer);
        }

        private void OnDestroy()
        {
            EventManager.Unregister<PlayerController>(PlayerManagerEvents.AddPlayer, AddPlayer);
            EventManager.Unregister<PlayerController>(PlayerManagerEvents.RemovePlayer, RemovePlayer);
            EventManager.Unregister<float, Vector3>(PlayerManagerEvents.Move, Move);
            EventManager.Unregister<PlayerController>(PlayerManagerEvents.SetCurrentPlayer, SetCurrentPlayer);
        }

        private void AddPlayer(PlayerController player)
        {
            if (!playerList.Contains(player))
                playerList.Add(player);
        }
        
        private void RemovePlayer(PlayerController player)
        {
            if (playerList.Contains(player))
                playerList.Add(player);
        }

        private void Move(float distance, Vector3 forward)
        {
            currentPlayer.Move(distance, forward);
        }
        
        private void SetCurrentPlayer(PlayerController player)
        {
            currentPlayer = player;
        }
    }
}