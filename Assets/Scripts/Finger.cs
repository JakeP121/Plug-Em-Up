using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlugEmUp
{
    public class Finger : MonoBehaviour
    {
        public float speed = 5.0f; // The speed this finger moves to plug/unplug holes.

        public bool inUse = false; // Whether this finger is currently plugging or moving to plug a hole.

        private GameObject target; // Target plugged or going to be plugged (null if not inUse)

        private Pause.Menu pauseMenu;

        private void Start()
        {
            pauseMenu = FindObjectOfType<Pause.Menu>();
        }

        // Update is called once per frame
        void Update()
        {
            if (pauseMenu.isPaused)
                return;

            if (target == null) // No target, move back to starting position
            {
                if (transform.localPosition.y >= 0)
                    transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y - (Time.deltaTime * speed), transform.localPosition.z);
                else
                    inUse = false;
            }
            else
            {
                // 6.3 - distance from tip of finger to origin
                float distance = target.transform.position.y - (transform.position.y + 6.3f);

                if (distance >= 0.5f)
                    transform.localPosition = new Vector3(target.transform.position.x, transform.localPosition.y + (Time.deltaTime * speed), transform.localPosition.z);
                else if (target.GetComponent<Leak>().currentState != Leak.State.PLUGGED)
                    target.GetComponent<Leak>().plug();
            }
        }


        /// <summary>
        /// Starts moving this finger to a target hole
        /// </summary>
        /// <param name="hole">The target to move to.</param>
        public void moveToPlugHole(GameObject hole)
        {
            target = hole;
            inUse = true;
        }

        /// <summary>
        /// Starts to retract this finger back below the screen
        /// </summary>
        public void retract()
        {
            target = null;
        }
    }
}