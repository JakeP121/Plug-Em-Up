using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlugEmUp.Pause
{
    public class Menu : MonoBehaviour
    {

        public bool isPaused = false; // Is the game currently paused.

        public KeyCode pauseButton = KeyCode.Escape; // The button the user presses to open/close the pause menu

        private void Start()
        {
            for (int i = 0; i < transform.childCount; i++)
                transform.GetChild(i).gameObject.SetActive(isPaused);
        }

        private void Update()
        {
            if (Input.GetKeyDown(pauseButton))
                flipPause();
        }

        /// <summary>
        /// Pauses the game if unpaused or unpauses if paused.
        /// </summary>
        public void flipPause()
        {
            isPaused = !isPaused;

            for (int i = 0; i < transform.childCount; i++)
                transform.GetChild(i).gameObject.SetActive(isPaused);
        }
    }
}