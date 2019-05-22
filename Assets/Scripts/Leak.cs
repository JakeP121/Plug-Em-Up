using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlugEmUp
{
    public class Leak : MonoBehaviour
    {
        public KeyboardKey key; // The key used to plug this hole

        public enum State { PLUGGED, LEAKING, BOARDING };
        public State currentState;

        private GameObject hole; // Hole texture
        private GameObject keyIcon; // Key texture

        private Finger finger; // Finger in use to plug (null if unplugged)

        public GameObject plank; // Plank in use to fix (null if not being repaired)

        private new ParticleSystem particleSystem;

        private Game game; // Game script, keeps track of fingers
        private Waves waves; // Water to increase over time

        private float waterFillSpeed = 5.0f; // How many units should the water increase by every second
        private float waterFillCounter = 0.0f; // Counter to keep track of how long since water was increased by this leak

        private Pause.PauseMenu pauseMenu;

        /// <summary>
        /// Initialises the leak
        /// </summary>
        /// <param name="key">The key used to plug this hole</param>
        public void init(KeyboardKey key)
        {
            this.key = key;

            currentState = State.LEAKING;

            transform.position = key.position;

            game = FindObjectOfType<Game>();
            pauseMenu = FindObjectOfType<Pause.PauseMenu>();

            Score.newLeak();
            waves = FindObjectOfType<Waves>();

            hole = transform.Find("Hole").gameObject;
            hole.transform.rotation = Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)); // Random orientation

            keyIcon = transform.Find("Key").gameObject;
            keyIcon.transform.Find("Text").GetComponent<TextMesh>().text = key.keyCode.ToString();

            particleSystem = GetComponentInChildren<ParticleSystem>();
        }

        // Update is called once per frame
        void Update()
        {
            if (pauseMenu.isPaused)
            {
                particleSystem.Stop();
                return;
            }
            else if (currentState == State.LEAKING && !particleSystem.isPlaying)
                particleSystem.Play();

            handleInput();

            updateWater();
        }

        /// <summary>
        /// Checks if this hole should be plugged/unplugged
        /// </summary>
        private void handleInput()
        {
            if (currentState == State.PLUGGED && !Input.GetKey(key.keyCode))
                unplug();
            else if (currentState == State.LEAKING)
            {
                if (Input.GetKey(key.keyCode)) // Plug button pressed
                {
                    if (finger == null)
                        moveToPlug();
                }
            }
        }

        /// <summary>
        /// Mouse clicked on this leak, start repairing
        /// </summary>
        private void OnMouseDown()
        {
            Mouse mouse = FindObjectOfType<Mouse>();

            if (mouse.currentTool == Mouse.Tool.PLANK && plank == null)
                startRepairing();
        }

        /// <summary>
        /// Increases the water level every second when leaking
        /// </summary>
        private void updateWater()
        {
            if (currentState != State.LEAKING)
                return;

            waterFillCounter += Time.deltaTime;

            if (waterFillCounter >= 1.0f)
            {
                if (currentState == State.LEAKING)
                    waves.increaseWater(waterFillSpeed);

                waterFillCounter = 0.0f;
            }
        }



        //
        // Gameplay events
        //

        /// <summary>
        /// Attempts to find an available finger and signals it to plug this hole
        /// </summary>
        private void moveToPlug()
        {
            finger = game.getFreeFinger();

            if (finger != null)
            {
                finger.moveToPlugHole(this.gameObject);
                showKeyIcon(false);
            }
        }

        /// <summary>
        /// Plugs the hole (stops increasing water level and hides particle effect)
        /// </summary>
        public void plug()
        {
            currentState = State.PLUGGED;

            waterFillCounter = 0.0f;

            particleSystem.Stop();
        }

        /// <summary>
        /// Unplugs the hole (starts increasing water level and shows particle effect)
        /// </summary>
        public void unplug()
        {
            currentState = State.LEAKING;

            if (finger != null)
                finger.retract();
            finger = null;

            particleSystem.Play();

            showKeyIcon(true);
        }

        /// <summary>
        /// Starts repair process
        /// </summary>
        private void startRepairing()
        {
            if (currentState == State.PLUGGED)
                unplug();

            Mouse mouse = FindObjectOfType<Mouse>();

            currentState = State.BOARDING;

            particleSystem.Stop();
            showKeyIcon(false);

            plank = Instantiate(Resources.Load("Plank")) as GameObject;
            plank.GetComponent<Plank>().init(transform);

            mouse.switchTool(Mouse.Tool.HAMMER);
        }

        /// <summary>
        /// Ends repair process successfully (hole fixed)
        /// </summary>
        public void fix()
        {
            key.hasALeak = false;
            Score.repairedLeak();

            plank.transform.SetParent(transform.parent);

            Destroy(this.gameObject);
        }


        //
        // Visuals
        //

        /// <summary>
        /// Shows or hides the key prompt icon
        /// </summary>
        /// <param name="show">True to show, false to hide.</param>
        private void showKeyIcon(bool show)
        {
            keyIcon.GetComponent<SpriteRenderer>().enabled = show;

            if (show)
                keyIcon.transform.Find("Text").GetComponent<TextMesh>().text = key.keyCode.ToString();
            else
                keyIcon.transform.Find("Text").GetComponent<TextMesh>().text = "";
        }
    }
}