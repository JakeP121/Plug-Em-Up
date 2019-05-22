using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlugEmUp
{
    public class Mouse : MonoBehaviour
    {
        // Cursor textures
        public List<Texture2D> hammer;
        public List<Texture2D> plank;

        public enum Tool { PLANK, HAMMER };
        public Tool ? currentTool { get; private set; }

        private Pause.PauseMenu pauseMenu;
        private Tool ? pausedOnTool = null; // The tool that was equipped before the game was paused

        private Resolution res;

        // Use this for initialization
        void Start()
        {
            pauseMenu = FindObjectOfType<Pause.PauseMenu>();

            res = Screen.currentResolution;

            switchTool(Tool.PLANK);
        }

        private void Update()
        {
            if (pauseMenu.isPaused)
            {
                if (currentTool == null)
                    return;
                else
                {
                    pausedOnTool = currentTool;
                    switchTool(null);
                }
            }
            else if (currentTool == null)
                switchTool(pausedOnTool);

            if (res.width != Screen.currentResolution.width || res.height != Screen.currentResolution.height)
                switchTool(currentTool);
        }

        /// <summary>
        /// Switches to a specified tool
        /// </summary>
        /// <param name="tool">The tool to switch to or null to reset.</param>
        public void switchTool(Tool ? tool)
        {
            currentTool = tool;

            switch (tool)
            {
                case (Tool.HAMMER):
                    setCursor(hammer);
                    break;
                case (Tool.PLANK):
                    setCursor(plank);
                    break;
                case (null):
                    Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
                    break;

            }
        }

        /// <summary>
        /// Sets the cursor to a texture
        /// </summary>
        /// <param name="textures">List of scaled textures</param>
        private void setCursor(List<Texture2D> textures)
        {
            int screenPixels = Screen.width * Screen.height;

            int regularPixels = 2073600; // 1080p

            Texture2D texture;

            // Determine texture size
            if (screenPixels <= regularPixels * 0.625f)
                texture = textures[0];
            else if (screenPixels <= regularPixels * 0.875f)
                texture = textures[1];
            else if (screenPixels <= regularPixels * 1.5f)
                texture = textures[2];
            else
                texture = textures[3];

            Cursor.SetCursor(texture, new Vector2(texture.width / 2, texture.height / 2), CursorMode.ForceSoftware);
        }
    }
}