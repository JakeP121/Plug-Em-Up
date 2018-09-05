using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour {

    private float independentLifespan = 10.0f;
    private float independentLifespanCounter = 0.0f;

    private float timeToBuild = 5.0f;
    private float buildTimeCounter = 0.0f;

    private int clicksToBuild = 5;
    private int clickCounter = 0;

    private enum State { BUILDING, BUILT, FALLING };
    private State currentState;

    public void init(Transform parent)
    {
        currentState = State.BUILDING;

        transform.parent = parent;
        transform.localPosition = new Vector3(0.0f, 0.0f, -21.0f);
    }

    public void Update()
    {
        if (currentState == State.BUILDING)
            return;
        else if (currentState == State.BUILDING)
        {
            buildTimeCounter += Time.deltaTime;

            if (buildTimeCounter >= timeToBuild)
                fall();
        }
        else if (currentState == State.FALLING)
        {
            independentLifespanCounter += Time.deltaTime;

            if (independentLifespanCounter >= independentLifespan)
                Destroy(gameObject);
        }
    }

    public void fall()
    {
        currentState = State.FALLING;
    }
}
