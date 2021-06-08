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
        [SerializeField] private Transform storage;
        [SerializeField] private float distance = 4f;
        private int maxPlayers = 6;
        private int playersCount;
        private readonly List<Color> colors = new List<Color>{Color.red, Color.blue, Color.green, Color.yellow, Color.magenta, Color.cyan};

        private void Awake()
        {
            EventManager.Register<string>(PlayerCreationEvents.ConfirmPlayerCreation, ConfirmPlayerCreation);
            EventManager.Register(PlayerCreationEvents.Hide, Hide);
        }

        private void OnDestroy()
        {
            EventManager.Unregister<string>(PlayerCreationEvents.ConfirmPlayerCreation, ConfirmPlayerCreation);
            EventManager.Unregister(PlayerCreationEvents.Hide, Hide);
        }

        private void Start()
        {
            view.SetClickListener(AddButtonClicked);
            view.PushButton(distance * (- maxPlayers / 2f + 0.5f));
        }

        private void AddButtonClicked()
        {
            EventManager.Trigger(TextFieldPopupEvents.Show);
        }

        private void ConfirmPlayerCreation(string text)
        {
            var position = storage.position + transform.right * distance * (playersCount - maxPlayers / 2 + 0.5f);
            var player = Instantiate(playerPrefab, position, Quaternion.identity).GetComponent<PlayerController>();
            SetupPlayer(player, text);
            EventManager.Trigger(PlayerManagerEvents.AddPlayer, player);
            playersCount++;
            HandlePlayerCreation();
            EventManager.Trigger(GameConfigurationMenuEvents.SetStartButtonInteractable, true);
        }

        private void HandlePlayerCreation()
        {
            if (playersCount < maxPlayers)
            {
                view.PushButton(distance * (playersCount - maxPlayers / 2 + 0.5f));
            }
            else
            {
                view.HideInstantly();
            }
        }

        private void SetupPlayer(PlayerController player, string text)
        {
            player.SetName(String.IsNullOrEmpty(text) ? "Jogador " + playersCount : text);
            player.SetColor(colors[playersCount]);
        }

        private void Hide()
        {
            view.Hide();
        }
    }
}
