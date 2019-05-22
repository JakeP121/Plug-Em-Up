using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlugEmUp
{
    public class WaveController : MonoBehaviour
    {
        public bool clockwise = true; // Which way to rotate

        private float counter = 0.0f; // Counter to keep track of time

        private Pause.PauseMenu pauseMenu;

        private void Start()
        {
            pauseMenu = FindObjectOfType<Pause.PauseMenu>();
        }

        // Update is called once per frame
        void Update()
        {
            if (pauseMenu.isPaused)
                return;

            counter += Time.deltaTime;

            if (clockwise)
                transform.localPosition = new Vector3(Mathf.Sin(counter), Mathf.Cos(counter), transform.localPosition.z);
            else
                transform.localPosition = new Vector3(Mathf.Sin(-counter), Mathf.Cos(-counter), transform.localPosition.z);
        }
    }
}