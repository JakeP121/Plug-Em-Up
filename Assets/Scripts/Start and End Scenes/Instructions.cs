using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlugEmUp
{
    public class Instructions : MonoBehaviour
    {

        public List<Sprite> instructionImages = new List<Sprite>();

        private int currentInstruction = -1;
        private SpriteRenderer spriteRenderer;
        private Pause.Menu pauseMenu;

        private void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            pauseMenu = FindObjectOfType<Pause.Menu>();
        }

        private void FixedUpdate()
        {
            if (pauseMenu.isPaused)
                return;

            if (Input.GetMouseButtonDown(0))
            {
                currentInstruction++;

                if (instructionImages.Count > currentInstruction)
                    spriteRenderer.sprite = instructionImages[currentInstruction];
                else
                    UnityEngine.SceneManagement.SceneManager.LoadScene(1);
            }
        }

    }
}