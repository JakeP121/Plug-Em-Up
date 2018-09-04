using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waves : MonoBehaviour {

    private float waterLevel = 0.0f;

    private GameObject[] waves;

    public void Start()
    {
        waves = new GameObject[transform.childCount];

        for (int i = 0; i < waves.Length; i++)
            waves[i] = transform.GetChild(i).gameObject;
    }

    public void increaseWater(float increase)
    {
        waterLevel += increase;

        transform.position = new Vector3(transform.position.x, transform.position.y + (increase / 100), transform.position.z);
    }
}
