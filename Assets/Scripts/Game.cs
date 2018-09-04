using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {

    private KeyboardGrid grid;
    private Waves waves;

    private float breakFrequency = 2.0f;
    private float currentCounter = 0.0f;

    public float pluggedFillSpeed = 0.5f;
    public float leakingFillSpeed = 1.0f;
    private float fillCounter = 0.0f;

    public Finger[] fingers = new Finger[5];

    // Use this for initialization
    void Start () {
        grid = GetComponent<KeyboardGrid>();
        waves = FindObjectOfType<Waves>();

        GameObject hand = GameObject.Find("Hand");

        for (int i = 0; i < 5; i++)
            fingers[i] = hand.transform.GetChild(i).GetComponent<Finger>();
	}
	
	// Update is called once per frame
	void Update () {
        currentCounter += Time.deltaTime;
        fillCounter += Time.deltaTime;

        if (currentCounter >= breakFrequency)
        {
            currentCounter = 0.0f;
            breakRandom();
        }

        if (fillCounter >= 0.5f)
        {
            updateWater();
            fillCounter = 0.0f;
        }
	}

    private void updateWater()
    {
        for (int i = 0; i < 3; i++)
        {
            foreach (KeyboardKey box in grid.rows[i].boxes)
            {
                if (box.currentState == KeyboardKey.State.PLUGGED)
                    waves.increaseWater(pluggedFillSpeed);
                else if (box.currentState == KeyboardKey.State.LEAKING)
                    waves.increaseWater(leakingFillSpeed);
                    
            }
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
            KeyboardKey[] boxes = grid.rows[Random.Range(0, 3)].boxes;
            KeyboardKey box = boxes[Random.Range(0, boxes.Length)];

            if (box.currentState == KeyboardKey.State.HEALTHY)
            {
                found = true;

                GameObject leak = Instantiate(Resources.Load("Leak")) as GameObject;
                leak.GetComponent<Leak>().init(box);

                box.breakWood(leak);
            }
            else
                i++;

            if (i > 10)
                return;
        }
    }

    public Finger getFreeFinger()
    {
        foreach (Finger f in fingers)
        {
            if (!f.inUse)
                return f;
        }

        return null;
    }

    private void endGame()
    {

    }
}
