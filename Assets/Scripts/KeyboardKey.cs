using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlugEmUp
{
    public class KeyboardKey
    {
        public KeyCode keyCode; // KeyCode associated with this location transposed onto a keyboard

        public Vector3 position; // Realworld location of this key
        public Vector3 size; // Size of collider

        public bool hasALeak = false; // Allows the game to see which keys it can spawn a leak on.

        /// <summary>
        /// Creates a KeyboardKey
        /// </summary>
        /// <param name="keyCode">The letter associated with this position on a keyboard.</param>
        public KeyboardKey(KeyCode keyCode)
        {
            this.keyCode = keyCode;
        }
    }
}