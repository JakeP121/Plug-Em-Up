using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardRow {

    public KeyboardKey[] boxes;

    public KeyboardRow(int boxes)
    {
        this.boxes = new KeyboardKey[boxes];
    }

    public KeyboardRow(Game game, params KeyCode[] boxes)
    {
        this.boxes = new KeyboardKey[boxes.Length];

        for (int i = 0; i < boxes.Length; i++)
            this.boxes[i] = new KeyboardKey(boxes[i], game);
    }


    public void setPositions(Vector2 rowSize, Vector2 boxSize, float yPos)
    {
        float whitespace = rowSize.x - (boxSize.x * boxes.Length );
        whitespace /= boxes.Length + 1;

        for (int i = 0; i < boxes.Length; i++)
        {
            boxes[i].size = new Vector3(boxSize.x, boxSize.y, 1.2f);

            float xPos = -(rowSize.x / 2) + (boxSize.x / 2) + (boxSize.x * i);

            if (whitespace > 0)
                xPos += whitespace * i;

            boxes[i].position = new Vector3(xPos, yPos, -1.0f);
        }
    }
}
