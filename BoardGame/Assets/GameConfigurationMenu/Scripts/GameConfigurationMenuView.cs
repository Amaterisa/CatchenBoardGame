using General.View;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace GameConfigurationMenu.Scripts
{
    public class GameConfigurationMenuView : View
    {
        [SerializeField] private Button startGameButton;
        [SerializeField] private Button backButton;

        public void SetStartGameClickListener(UnityAction action)
        {
            startGameButton.onClick.RemoveAllListeners();
            startGameButton.onClick.AddListener(action);
        }
        
        public void SetBackClickListener(UnityAction action)
        {
            backButton.onClick.RemoveAllListeners();
            backButton.onClick.AddListener(action);
        }

        public void SetStartButtonInteractable(bool interactable)
        {
            startGameButton.interactable = interactable;
        }
        
        public void SetBackButtonInteractable(bool interactable)
        {
            startGameButton.interactable = interactable;
        }
    }
}
