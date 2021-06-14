using General.View;
using UnityEngine;
using UnityEngine.UI;

namespace TurnController.Scripts
{
    public class TurnView : View
    {
        [SerializeField] private Text text;
        [SerializeField] private Text playerName;

        public void SetText(string txt)
        {
            text.text = txt;
        }
        
        public void SetPlayerName(string txt)
        {
            playerName.text = txt;
        }
    }
}
