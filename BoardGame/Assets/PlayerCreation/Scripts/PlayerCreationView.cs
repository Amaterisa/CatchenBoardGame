using System;
using General.View;
using UnityEngine;
using Button = UnityEngine.UI.Button;

namespace PlayerCreation.Scripts
{
    public class PlayerCreationView : View
    {
        [SerializeField] private Button addPlayerButton;

        public void SetClickListener(Action action)
        {
            addPlayerButton.onClick.RemoveAllListeners();
            addPlayerButton.onClick.AddListener(action.Invoke);
        }

        public void PushButton(float distance)
        {
            transform.position = transform.right * distance;
        }
    }
}
