using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardKey {

    public KeyCode key;

    public Vector3 position;
    public Vector3 size;

    public enum State { HEALTHY, PLUGGED, LEAKING };
    public State currentState;

    private Game game;
    private GameObject leakObject;
    private GameObject plank;
    private Finger finger;

    public KeyboardKey(KeyCode key, Game game)
    {
        this.key = key;
        currentState = State.HEALTHY;

        this.game = game;
    }

    public void breakWood(GameObject leakObject)
    {
        if (currentState != State.HEALTHY)
            return;

        currentState = State.LEAKING;
        this.leakObject = leakObject;

        leakObject.transform.position = position;

        if (plank != null)
        {
            plank.GetComponent<Plank>().fall();
            plank = null;
        }
    }

    public bool plug()
    {
        if (currentState != State.LEAKING)
            return false;

        finger = game.getFreeFinger();
        if (finger != null)
        {
            finger.plugHole(leakObject);
            currentState = State.PLUGGED;
            return true;
        }
        else
            return false;
    }

    public void unplug()
    {
        if (currentState != State.PLUGGED)
            return;

        if (finger != null)
            finger.resetFinger();

        currentState = State.LEAKING;
    }

    public void fix()
    {
        if (currentState != State.HEALTHY)
            currentState = State.HEALTHY;
    }
}
