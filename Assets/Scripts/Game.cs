using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {

    private KeyboardGrid grid;
    private Waves waves;

    private float breakFrequency = 5.0f;
    private float counter = 0.0f;

    public Finger[] fingers = new Finger[5];

    // Use this for initialization
    void Start () {
        grid = GetComponent<KeyboardGrid>();

        GameObject hand = GameObject.Find("Hand");

        for (int i = 0; i < 5; i++)
            fingers[i] = hand.transform.GetChild(i).GetComponent<Finger>();

        breakRandom();
	}
	
	// Update is called once per frame
	void Update () {
        counter += Time.deltaTime;

        if (counter >= breakFrequency)
        {
            counter = 0.0f;
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
                //leak.transform.parent = this.transform;
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
    private void endGame()
    {
        throw new System.NotImplementedException();
    }
}
