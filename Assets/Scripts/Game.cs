using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlugEmUp
{
    public class Game : MonoBehaviour
    {
        private KeyboardGrid grid; // Game screen split into keyboard-based grid
        private Waves waves; // Overall water level

        public float breakFrequency = 1.0f; // How frequently damage is inflicted
        private float breakCounter = 0.0f; // How long since damage was last inflicted

        public Finger[] fingers; // Array of all fingers

        private bool running = false; // Is the game running

        private float gameTimer = 0.0f;

        // Use this for initialization
        void Start()
        {
            grid = GetComponent<KeyboardGrid>();

            GameObject hand = GameObject.Find("Hand");

            fingers = new Finger[hand.transform.childCount];
            for (int i = 0; i < hand.transform.childCount; i++)
                fingers[i] = hand.transform.GetChild(i).GetComponent<Finger>();

            running = true;


            breakRandom();
        }

        // Update is called once per frame
        void Update()
        {
            if (!running)
                return;

            gameTimer += Time.deltaTime;

            breakCounter += Time.deltaTime;

            if (breakCounter >= breakFrequency)
            {
                breakCounter = 0.0f;
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
                    leak.GetComponent<Leak>().init(key);

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
            GetComponent<Mouse>().reset();

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