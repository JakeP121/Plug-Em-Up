using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlugEmUp
{
    public class PlankUIRing : MonoBehaviour {

        public float speed = 2.5f; // The speed of this animation
        public float maxSize = 1.5f; // The maximum size of the ring 
        public float minSize = 0.5f; // The minimum size of the ring

        private bool growing = true; // Stage of the animation

        private Plank plank; // The plank this animation is attached to 

        // Use this for initialization
        void Start() {
            plank = transform.parent.GetComponent<Plank>();
        }

        // Update is called once per frame
        void Update() {
            checkIfValid();
            carryOutAnimation();
        }

        /// <summary>
        /// Check if plank is still being built
        /// </summary>
        private void checkIfValid()
        {
            if (plank.currentState != Plank.State.BUILDING)
                Destroy(this.gameObject);
        }

        /// <summary>
        /// Carrys out the scaling animation
        /// </summary>
        private void carryOutAnimation()
        {
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
    }
}