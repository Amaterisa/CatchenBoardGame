using General.View;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

namespace TextFieldPopup.Scripts
{
    public class TextFieldPopupView : View
    {
        private TextField textField;
        [SerializeField] private Button confirmButton;
        [SerializeField] private Button cancelButton;

        public void SetConfirmClickListener(UnityAction action)
        {
            confirmButton.onClick.RemoveAllListeners();
            confirmButton.onClick.AddListener(action);
        }
        
        public void SetCancelClickListener(UnityAction action)
        {
            cancelButton.onClick.RemoveAllListeners();
            cancelButton.onClick.AddListener(action);
        }

        public string GetText()
        {
            return textField.value;
        }
    }
}
