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
        public Tool currentTool { get; private set; }

        // Use this for initialization
        void Start()
        {
            switchTool(Tool.PLANK);
        }

        /// <summary>
        /// Switches to a specified tool
        /// </summary>
        /// <param name="tool">The tool to switch to</param>
        public void switchTool(Tool tool)
        {
            switch (tool)
            {
                case (Tool.HAMMER):
                    currentTool = Tool.HAMMER;
                    Cursor.SetCursor(hammer, new Vector2(51.5f, 51.5f), CursorMode.ForceSoftware);
                    break;
                case (Tool.PLANK):
                    currentTool = Tool.PLANK;
                    Cursor.SetCursor(plank, new Vector2(102.5f, 51.5f), CursorMode.ForceSoftware);
                    break;
            }

        }
    }
}