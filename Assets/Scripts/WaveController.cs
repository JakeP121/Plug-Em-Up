using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour {

    public bool clockwise = true;

    private float counter = 0.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        counter += Time.deltaTime;

        if (clockwise)
            transform.localPosition = new Vector3(Mathf.Sin(counter), Mathf.Cos(counter), transform.localPosition.z);
        else
            transform.localPosition = new Vector3(Mathf.Sin(-counter), Mathf.Cos(-counter), transform.localPosition.z);
	}
}
