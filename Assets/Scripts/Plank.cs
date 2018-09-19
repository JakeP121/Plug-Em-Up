using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlugEmUp
{
    public class Plank : MonoBehaviour
    {
        public float fallingSpeed = 6.0f; // How fast the plank falls if it falls off a hole
        public float destroyWhenPlankReaches = -15.0f; // The distance the plank will fall to before being destroyed

        public float timeToBuild = 5.0f; // The maximum time the plank has to be fully built before falling off
        private float buildTimeCounter = 0.0f; // Counter to keep track of current build time

        private int clicksToBuild; // How many clicks it takes to fully build the plank
        private int clickCounter = 0; // Counter to keep track of how many clicks have been performed

        public enum State { BUILDING, BUILT, FALLING };
        public State currentState;

        private KeyboardKey key; // Keyboard location of this plank

        private PulsingUI ring; // The pulsing UI ring

        private Mouse mouse; // The mouse used to build this plank
        private Pause.Menu pauseMenu; 

        public AudioClip[] hammerSounds;
        public AudioClip[] placementSound;
        private AudioSource audioSource;

        /// <summary>
        /// Initialises the plank
        /// </summary>
        /// <param name="parent">Transform of hole to place this plank on top of.</param>
        public void init(Transform parent)
        {
            currentState = State.BUILDING;

            mouse = FindObjectOfType<Mouse>();
            mouse.switchTool(Mouse.Tool.HAMMER);

            pauseMenu = FindObjectOfType<Pause.Menu>();

            transform.parent = parent;
            transform.localPosition = new Vector3(0.0f, 0.0f, -21.0f);
            key = transform.parent.GetComponent<Leak>().key;

            ring = transform.Find("Ring").gameObject.GetComponent<PulsingUI>();

            clicksToBuild = Random.Range(1, 8);
            audioSource = GetComponent<AudioSource>();
            playSoundFrom(placementSound);
        }

        public void Update()
        {
            if (pauseMenu.isPaused)
                return;

            switch (currentState)
            {
                case (State.BUILT): // Skip if built
                    if (key.hasALeak)
                        startFalling();
                    break;


                case (State.BUILDING): // Check if plank should start falling
                    buildTimeCounter += Time.deltaTime;

                    if (buildTimeCounter >= timeToBuild)
                    {
                        startFalling();
                        mouse.switchTool(Mouse.Tool.PLANK);
                        transform.parent.GetComponent<Leak>().currentState = Leak.State.LEAKING;
                    }
                    break;


                case (State.FALLING): // Fall until out of range
                    transform.position = new Vector3(transform.position.x, transform.position.y - (fallingSpeed * Time.deltaTime), transform.position.z);

                    if (transform.position.y <= destroyWhenPlankReaches)
                        Destroy(gameObject);
                    break;
            }

        }

        /// <summary>
        /// Mouse clicked on this plank, adds one to clickCounter
        /// </summary>
        private void OnMouseDown()
        {
            if (currentState != State.BUILDING || pauseMenu.isPaused)
                return;

            clickCounter++;
            playSoundFrom(hammerSounds);

            if (clickCounter >= clicksToBuild) // Click counter satisfied, fully built
            {
                currentState = State.BUILT;
                ring.setActive(false);

                mouse.switchTool(Mouse.Tool.PLANK);

                Leak leak = transform.parent.GetComponent<Leak>();
                transform.parent = null;
                leak.fix();
            }
        }

        /// <summary>
        /// Pops the plank off a hole and starts falling.
        /// </summary>
        public void startFalling()
        {
            currentState = State.FALLING;

            if (transform.parent != null)
                transform.parent.GetComponent<Leak>().plank = null;
        }

        /// <summary>
        /// Picks a random sound to play from an array.
        /// </summary>
        /// <param name="sounds">An array of sounds</param>
        private void playSoundFrom(AudioClip[] sounds)
        {
            AudioClip sound = sounds[Random.Range(0, sounds.Length)];
            audioSource.clip = sound;
            audioSource.Play();
        }
    }
}