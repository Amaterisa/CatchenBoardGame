using System;
using System.Collections.Generic;
using Events;
using General.EventManager;
using Player.Scripts;
using UnityEngine;

namespace PlayerCreation.Scripts
{
    public class PlayerCreationController : MonoBehaviour
    {
        [SerializeField] private PlayerCreationView view;
        [SerializeField] private GameObject playerPrefab;
        [SerializeField] private float distance = 4f;
        private int maxPlayers = 6;
        private int playersCount;
        private readonly List<Color> colors = new List<Color>{Color.red, Color.blue, Color.green, Color.yellow, Color.magenta, Color.cyan};

        private void Start()
        {
            view.SetClickListener(AddButtonClicked);
        }

        private void AddButtonClicked()
        {
            //show popup with text field
        }

        private void ConfirmPlayerCreation(string text)
        {
            var position = transform.position + transform.right * distance * playersCount;
            var player = Instantiate(playerPrefab, position, Quaternion.identity, transform).GetComponent<PlayerController>();;
            SetupPlayer(player, text);
            EventManager.Trigger(PlayerManagerEvents.AddPlayer, player);
            playersCount++;
            HandlePlayerCreation();
        }

        private void HandlePlayerCreation()
        {
            if (playersCount < maxPlayers)
            {
                view.PushButton(distance * playersCount);
            }
            else
            {
                view.HideInstantly();
            }
        }

        private void SetupPlayer(PlayerController player, string text)
        {
            player.SetName(text);
            player.SetColor(colors[playersCount]);
        }
    }
}
