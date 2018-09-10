using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlugEmUp
{
    public class PulsingUI : MonoBehaviour {

        public float speed = 2.5f; // The speed of this animation
        public float maxSize = 1.5f; // The maximum size of the object
        public float minSize = 0.5f; // The minimum size of the object

        private bool growing = true; // Stage of the animation

        public bool active = true; // Whether or not the animation should play

        private SpriteRenderer sprite; // The physical sprite

        private void Start()
        {
            sprite = GetComponent<SpriteRenderer>();
        }

        // Update is called once per frame
        void Update() {
            if (!active)
                return;

            if (growing)
            {
                transform.localScale = new Vector3(transform.localScale.x + (speed * Time.deltaTime), transform.localScale.y + (speed * Time.deltaTime), transform.localScale.z);

                if (transform.localScale.x >= maxSize)
                    growing = false;
            }
            else
            {
                transform.localScale = new Vector3(transform.localScale.x - (speed * Time.deltaTime), transform.localScale.y - (speed * Time.deltaTime), transform.localScale.z);

                if (transform.localScale.x <= minSize)
                    growing = true;
            }
        }

        /// <summary>
        /// Activates or deactivates the sprite/animation
        /// </summary>
        /// <param name="isActive">True to activate, false to deactivate.</param>
        public void setActive(bool isActive = true)
        {
            active = isActive;

            sprite.enabled = isActive;
        }
    }
}