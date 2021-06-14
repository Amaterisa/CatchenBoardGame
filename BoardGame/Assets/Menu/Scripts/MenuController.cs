using Events;
using General.EventManager;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Menu.Scripts
{
    public class MenuController : MonoBehaviour
    {
        [SerializeField] private MenuView view;

        private void Start()
        {
            view.SetStartButtonClickListener(StartClick);
        }

        private void StartClick()
        {
            view.Hide();
            EventManager.Trigger(FadeScreenEvents.Show);
            SceneManager.LoadSceneAsync("MainScene");
        }
    }
}
