using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlugEmUp.Pause
{
    public class YesButton : MonoBehaviour
    {

        private void OnMouseDown()
        {
            Application.Quit();
        }
    }
}