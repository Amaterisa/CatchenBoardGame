using System;
using General.View;
using UnityEngine;
using UnityEngine.UI;

namespace Menu.Scripts
{
    public class MenuView : View
    {
        [SerializeField] private Button startButton;

        public void SetStartButtonClickListener(Action action)
        {
            startButton.onClick.AddListener(action.Invoke);
        }
    }
}
