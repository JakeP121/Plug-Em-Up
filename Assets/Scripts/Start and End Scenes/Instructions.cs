using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PlugEmUp
{
    public class Instructions : MonoBehaviour
    {
        public List<Sprite> instructionImages = new List<Sprite>();

        private int currentInstruction = -1;
        private Image currentImage;
        private Pause.PauseMenu pauseMenu;

        private void Start()
        {
            currentImage = GetComponent<Image>();
            pauseMenu = FindObjectOfType<Pause.PauseMenu>();

            currentImage.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        }

        private void Update()
        {
            if (pauseMenu.isPaused)
                return;

            if (Input.GetMouseButtonDown(0))
            {
                currentImage.color = Color.white;
                currentInstruction++;

                if (instructionImages.Count > currentInstruction)
                    currentImage.sprite = instructionImages[currentInstruction];
                else
                    UnityEngine.SceneManagement.SceneManager.LoadScene(1);
            }
        }

    }
}