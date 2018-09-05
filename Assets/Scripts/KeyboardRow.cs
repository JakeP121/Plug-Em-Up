using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlugEmUp
{
    public class KeyboardRow
    {
        public KeyboardKey[] keys; // Individual keys in this row

        /// <summary>
        /// Creates a KeyboardRow
        /// </summary>
        /// <param name="keyCount">The amount of keys in this row.</param>
        public KeyboardRow(int keyCount)
        {
            this.keys = new KeyboardKey[keyCount];
        }

        /// <summary>
        /// Creates a KeyboardRow
        /// </summary>
        /// <param name="keys">KeyCodes for all keys in this row.</param>
        public KeyboardRow(params KeyCode[] keys)
        {
            this.keys = new KeyboardKey[keys.Length];

            for (int i = 0; i < keys.Length; i++)
                this.keys[i] = new KeyboardKey(keys[i]);
        }

        /// <summary>
        /// Sets the size and positions of all keys in this row
        /// </summary>
        /// <param name="rowSize">Width and height of this row.</param>
        /// <param name="keySize">The width and height of all keys in this row.</param>
        /// <param name="yPos">The ycoordinate of all keys.</param>
        public void setPositions(Vector2 rowSize, Vector2 keySize, float yPos)
        {
            float whitespace = rowSize.x - (keySize.x * keys.Length);
            whitespace /= keys.Length + 1;

            for (int i = 0; i < keys.Length; i++)
            {
                keys[i].size = new Vector3(keySize.x, keySize.y, 1.2f);

                float xPos = -(rowSize.x / 2) + (keySize.x / 2) + (keySize.x * i);

                if (whitespace > 0)
                    xPos += whitespace * i;

                keys[i].position = new Vector3(xPos, yPos, -1.0f);
            }
        }
    }
}