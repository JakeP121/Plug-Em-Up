using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardRow {

    public KeyboardKey[] keys;

    public KeyboardRow(int keyCount)
    {
        this.keys = new KeyboardKey[keyCount];
    }

    public KeyboardRow(Game game, params KeyCode[] keys)
    {
        this.keys = new KeyboardKey[keys.Length];

        for (int i = 0; i < keys.Length; i++)
            this.keys[i] = new KeyboardKey(keys[i]);
    }


    public void setPositions(Vector2 rowSize, Vector2 boxSize, float yPos)
    {
        float whitespace = rowSize.x - (boxSize.x * keys.Length );
        whitespace /= keys.Length + 1;

        for (int i = 0; i < keys.Length; i++)
        {
            keys[i].size = new Vector3(boxSize.x, boxSize.y, 1.2f);

            float xPos = -(rowSize.x / 2) + (boxSize.x / 2) + (boxSize.x * i);

            if (whitespace > 0)
                xPos += whitespace * i;

            keys[i].position = new Vector3(xPos, yPos, -1.0f);
        }
    }
}
