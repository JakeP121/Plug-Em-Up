using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlugEmUp.Utilities
{
    public class SceneManager : MonoBehaviour
    {
        /// <summary>
        /// Sets the current scene 
        /// </summary>
        /// <param name="scene"></param>
        public void setScene(int scene)
        {

        }

        /// <summary>
        /// Quits either the Unity Editor or Application
        /// </summary>
        public void quit()
        {
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                    Application.Quit();
            #endif
        }
    }
}