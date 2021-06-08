using General.View;
using UnityEngine;
using UnityEngine.UI;

namespace TurnController.Scripts
{
    public class TurnView : View
    {
        [SerializeField] private Text text;

        public void SetText(string txt)
        {
            text.text = txt;
        }
    }
}
