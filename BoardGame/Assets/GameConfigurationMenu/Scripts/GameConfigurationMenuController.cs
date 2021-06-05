using System;
using Events;
using General.EventManager;
using UnityEngine;

namespace GameConfigurationMenu.Scripts
{
    public class GameConfigurationMenuController : MonoBehaviour
    {
        [SerializeField] private GameConfigurationMenuView view;

        private void Start()
        {
            view.SetStartGameClickListener(StartGame);
            view.SetBackClickListener(Back);
        }

        private void StartGame()
        {
            view.Hide();
            EventManager.Trigger(PlayerCreationEvents.Hide);
            EventManager.Trigger(BoardEvents.Show);
            EventManager.Trigger(PlayerManagerEvents.PositionPlayers);
        }

        private void Back()
        {
            //TODO: load menu
        }
    }
}
