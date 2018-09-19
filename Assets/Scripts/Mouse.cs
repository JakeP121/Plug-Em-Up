using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlugEmUp
{
    public class Mouse : MonoBehaviour
    {
        // Cursor textures
        public Texture2D hammer;
        public Texture2D plank; 

        public enum Tool { PLANK, HAMMER };
        public Tool ? currentTool { get; private set; }

        private Pause.Menu pauseMenu;
        private Tool ? pausedOnTool = null; // The tool that was equipped before the game was paused

        // Use this for initialization
        void Start()
        {
            pauseMenu = FindObjectOfType<Pause.Menu>();

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
                    Cursor.SetCursor(hammer, new Vector2(51.5f, 51.5f), CursorMode.ForceSoftware);
                    break;
                case (Tool.PLANK):
                    Cursor.SetCursor(plank, new Vector2(102.5f, 51.5f), CursorMode.ForceSoftware);
                    break;
                case (null):
                    Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
                    break;

            }
        }
    }
}