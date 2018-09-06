using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlugEmUp
{
    public static class Score
    {
        private static float timeSurvived = 0;
        private static int leaksStarted = 0;
        private static int leaksRepaired = 0;

        /// <summary>
        /// Sets the time that the player has survived this game
        /// </summary>
        /// <param name="time">Time in seconds.</param>
        public static void setTimeSurvived(float time)
        {
            timeSurvived = time;
        }

        /// <summary>
        /// Call when a new leak has spawned.
        /// </summary>
        public static void newLeak()
        {
            leaksStarted++;
        }

        /// <summary>
        /// Call when a leak has been repaired
        /// </summary>
        public static void repairedLeak()
        {
            leaksRepaired++;
        }

        /// <summary>
        /// Produces and returns a score
        /// </summary>
        /// <returns>Score value</returns>
        public static int getCurrentScore()
        {
            int score = ((int)timeSurvived * 100) + (leaksStarted * 5) + (leaksRepaired * 10);
            return score;
        }

        /// <summary>
        /// Saves the score if it is a highscore
        /// </summary>
        public static void saveIfHighscore()
        {
            int currentScore = getCurrentScore();

            if (currentScore >= PlayerPrefs.GetInt("Highscore", 0))
                PlayerPrefs.SetInt("Highscore", currentScore);
        }

        /// <summary>
        /// Resets the current score
        /// </summary>
        public static void reset()
        {
            timeSurvived = 0.0f;
            leaksStarted = 0;
            leaksRepaired = 0;
        }
    }
}