using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlugEmUp.Pause
{
    public class NoButton : MonoBehaviour
    {

        private Menu pauseMenu;

        private void Start()
        {
            pauseMenu = GetComponentInParent<Menu>();
        }

        private void OnMouseDown()
        {
            pauseMenu.flipPause();
        }
    }
}