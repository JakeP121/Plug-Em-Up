using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finger : MonoBehaviour {

    public float speed = 5.0f;

    public bool inUse = false;

    private GameObject target;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (target == null) // No target, move back to starting position
        {
            if (transform.localPosition.y >= 0)
                transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y - (Time.deltaTime * speed), transform.localPosition.z);
            else
                inUse = false;
        }
        else
        {
            //6.3
            float distance = Mathf.Abs(target.transform.position.y - transform.localPosition.y);

            Debug.Log(distance);

            if (distance < 6.3)
            {
                transform.localPosition = new Vector3(target.transform.position.x, transform.localPosition.y + (Time.deltaTime * speed), transform.localPosition.z);
            }
        }
	}

    public void plugHole(GameObject hole)
    {
        target = hole;
        inUse = true;
    }

    public void resetFinger()
    {
        target = null;
    }
}
