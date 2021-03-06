﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlugEmUp
{
    public class KeyboardGrid : MonoBehaviour
    {
        public bool drawBoxes = false; // Draw boxes to the editor?

        public KeyboardRow[] rows = new KeyboardRow[3]; // Rows of all possible holes

        private bool initialised = false; // Stops the grid from getting drawn before it is initialised

        private float cameraAspect = 0.0f;

        // Use this for initialization
        void Start()
        {
            setupGrid();

            cameraAspect = Camera.main.aspect;

            initialised = true;
        }

        /// <summary>
        /// Sets up the grid of all holes
        /// </summary>
        private void setupGrid()
        {
            rows[0] = new KeyboardRow(KeyCode.Q, KeyCode.W, KeyCode.E, KeyCode.R, KeyCode.T, KeyCode.Y, KeyCode.U, KeyCode.I, KeyCode.O, KeyCode.P);
            rows[1] = new KeyboardRow(KeyCode.A, KeyCode.S, KeyCode.D, KeyCode.F, KeyCode.G, KeyCode.H, KeyCode.J, KeyCode.K, KeyCode.L);
            rows[2] = new KeyboardRow(KeyCode.Z, KeyCode.X, KeyCode.C, KeyCode.V, KeyCode.B, KeyCode.N, KeyCode.M);

            alignGrid();
        }

        /// <summary>
        /// Aligns the keyboard grid with the camera
        /// </summary>
        private void alignGrid()
        {
            Vector2 playSize = getPlaySize();

            transform.localScale = new Vector3(playSize.x, playSize.y, 1.0f);

            Vector2 rowSize = new Vector2(playSize.x, playSize.y / 3);
            Vector2 boxSize = new Vector2(playSize.x / rows[0].keys.Length, playSize.y / 3);

            for (int i = 0; i < rows.Length; i++)
            {
                float yPos = (1 - i) * (playSize.y / 3);
                rows[i].setPositions(rowSize, boxSize, yPos);
            }
        }

        /// <summary>
        /// Gets the play size of keyboard grid from the camera's size
        /// </summary>
        /// <returns>Keyboard grid playsize</returns>
        private Vector2 getPlaySize()
        {
            // https://answers.unity.com/questions/230190/how-to-get-the-width-and-height-of-a-orthographic.html
            float camHeight = Camera.main.orthographicSize * 2.0f;
            float camWidth = camHeight * Camera.main.aspect;

            return new Vector2(camWidth, camHeight);
        }

        /// <summary>
        /// Draws the grid to the screen using gizmos
        /// </summary>
        private void OnDrawGizmos()
        {
            if (!initialised || !drawBoxes)
                return;

            foreach (KeyboardRow row in rows)
            {
                foreach (KeyboardKey box in row.keys)
                    Gizmos.DrawCube(box.position, box.size);
            }
        }

        private void Update()
        {
            // Realine grid if camera aspect ratio has changed
            if (Camera.main.aspect != cameraAspect)
            {
                cameraAspect = Camera.main.aspect;
                alignGrid();
            }
        }
    }
}