using CodeBase.Infrastructure;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace CodeBase.UI
{
    public abstract class WindowBase : MonoBehaviour
    {
        public Button ContinueButton;
        public Button NewGameButton;
        public Button ExitButton;

        public GameRunner GameRunner;

        private void Awake()
        {
            OnAwake();
        }

        protected virtual void OnAwake()
        { 
            ContinueButton.onClick.AddListener(Continue);
            NewGameButton.onClick.AddListener(NewGame);
            ExitButton.onClick.AddListener(Exit);
        }

        private void Exit()
        {
            Application.Quit();
        }

        private void NewGame()
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
            Continue();
        }

        private void Continue()
        {
            var gameRunner = FindObjectOfType<GameRunner>();

            if (gameRunner != null) return;

            Instantiate(GameRunner);
        }
        
    }
}