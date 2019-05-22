using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlugEmUp
{
    public class Game : MonoBehaviour
    {
        private KeyboardGrid grid; // Game screen split into keyboard-based grid
        private Waves waves; // Overall water level

        private float gameTimer = 0.0f; // How long the game has been running

        private float breakCounter = 0.0f; // How long since damage was last inflicted
        private List<Tuple<float, float, float>> breakFrequency = new List<Tuple<float, float, float>>(); // The length of each stage (seconds), the frequency of leaks (seconds) and probability of a double event (0-1)
        private int stage = 0; // What stage of breakFrequency the game is currently in
        private float stageTimer = 0.0f; // How long the current stage has been active

        public Finger[] fingers; // Array of all fingers

        public AudioClip explosionSound; // A sound to signal a new leak
        private AudioSource audioSource; // AudioSource to play explosion sound

        private bool running = false; // Is the game running
        private Pause.PauseMenu pauseMenu;
        
        // Use this for initialization
        void Start()
        {
            grid = GetComponent<KeyboardGrid>();
            pauseMenu = FindObjectOfType<Pause.PauseMenu>();

            audioSource = GetComponent<AudioSource>();
            audioSource.clip = explosionSound;

            GameObject hand = GameObject.Find("Hand");

            fingers = new Finger[hand.transform.childCount];
            for (int i = 0; i < hand.transform.childCount; i++)
                fingers[i] = hand.transform.GetChild(i).GetComponent<Finger>();

            initBreakFrequencies();

            running = true;
        }

        /// <summary>
        /// Initialises the stages
        /// </summary>
        private void initBreakFrequencies()
        {
            breakFrequency.Add(new Tuple<float, float, float>(15.0f, 4.0f, 0.0f));
            breakFrequency.Add(new Tuple<float, float, float>(16.0f, 4.0f, 0.3f));
            breakFrequency.Add(new Tuple<float, float, float>(15.0f, 3.0f, 0.3f));
            breakFrequency.Add(new Tuple<float, float, float>(15.0f, 3.0f, 0.4f));
            breakFrequency.Add(new Tuple<float, float, float>(12.0f, 3.0f, 0.5f));
            breakFrequency.Add(new Tuple<float, float, float>(12.0f, 2.5f, 0.5f));
            breakFrequency.Add(new Tuple<float, float, float>(12.0f, 2.0f, 0.5f));
            breakFrequency.Add(new Tuple<float, float, float>(12.0f, 2.0f, 0.7f));
            breakFrequency.Add(new Tuple<float, float, float>(Mathf.Infinity, 1.5f, 0.9f));

            stage = 0;
        }

        // Update is called once per frame
        void Update()
        {
            if (!running || pauseMenu.isPaused)
                return;

            gameTimer += Time.deltaTime;
            stageTimer += Time.deltaTime;

            if (stageTimer >= breakFrequency[stage + 1].key1 && stage < breakFrequency.Count -2) // Advance stage if time elapsed
            {
                stageTimer = 0.0f;
                stage++;
            }

            breakCounter += Time.deltaTime;

            if (breakCounter >= breakFrequency[stage].key2) // Make a leak
            {
                breakCounter = 0.0f;

                breakRandom();

                // Test if there should be a double event?
                float doubleEvent = Random.Range(0.0f, 1.0f);

                if (doubleEvent <= breakFrequency[stage].key3)
                    breakRandom();
            }
        }

        /// <summary>
        /// Breaks a random part of the hull
        /// </summary>
        private void breakRandom()
        {
            bool found = false;
            int i = 0;

            while (!found)
            {
                KeyboardKey[] row = grid.rows[Random.Range(0, 3)].keys;
                KeyboardKey key = row[Random.Range(0, row.Length)];

                if (!key.hasALeak)
                {
                    found = true;

                    GameObject leak = Instantiate(Resources.Load("Leak")) as GameObject;
                    leak.transform.SetParent(this.transform);
                    leak.GetComponent<Leak>().init(key);
                    audioSource.Play();

                    key.hasALeak = true;
                }
                else
                    i++;

                if (i > 10)
                    return;
            }
        }


        /// <summary>
        /// Finds and returns a free finger.
        /// </summary>
        /// <returns>A finger currently plugging no holes.</returns>
        public Finger getFreeFinger()
        {
            foreach (Finger f in fingers)
            {
                if (!f.inUse)
                    return f;
            }

            return null;
        }

        /// <summary>
        /// Ends the game and displays the score
        /// </summary>
        public void endGame()
        {
            if (!running)
                return;

            running = false;

            Score.setTimeSurvived(gameTimer);

            closeAllLeaks();
            GetComponent<Mouse>().switchTool(null);

            GameObject.Find("End Screen").GetComponent<EndScreen>().rise();
        }

        /// <summary>
        /// Destroys all leaks
        /// </summary>
        private void closeAllLeaks()
        {
            GameObject[] leaks = GameObject.FindGameObjectsWithTag("Leak");

            foreach (GameObject leak in leaks)
            {
                for (int i = 0; i < leak.transform.childCount; i++)
                    Destroy(leak.transform.GetChild(i).gameObject);
                Destroy(leak);
            }
        }
    }
}