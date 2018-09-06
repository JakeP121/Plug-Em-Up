
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlugEmUp
{
    public class Waves : MonoBehaviour
    {
        private GameObject[] waves; // Individual wave objects

        public void Start()
        {
            waves = new GameObject[transform.childCount];

            for (int i = 0; i < waves.Length; i++)
                waves[i] = transform.GetChild(i).gameObject;
        }

        private void Update()
        {
            checkIfGameOver();
        }

        /// <summary>
        /// Check if the water level has covered the screen and game is over
        /// </summary>
        private void checkIfGameOver()
        {
            if (transform.position.y >= 0)
                GameObject.Find("Game").GetComponent<Game>().endGame();
        }

        /// <summary>
        /// Increases the water by a value
        /// </summary>
        /// <param name="increase">The value to increase by.</param>
        public void increaseWater(float increase)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + (increase / 100), transform.position.z);
        }
    }
}