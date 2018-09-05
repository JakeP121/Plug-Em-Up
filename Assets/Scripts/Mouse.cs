using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour {

    public Texture2D hammer;
    public Texture2D board;

    public enum State { BOARDING, HAMMERING };
    public State currentState;

	// Use this for initialization
	void Start () {
        currentState = State.BOARDING;

        Cursor.SetCursor(board, Vector3.zero, CursorMode.ForceSoftware);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void switchState()
    {
        switch (currentState)
        {
            case (State.BOARDING):
                currentState = State.HAMMERING;
                Cursor.SetCursor(hammer, Vector3.zero, CursorMode.ForceSoftware);
                break;
            case (State.HAMMERING):
                currentState = State.BOARDING;
                Cursor.SetCursor(board, Vector3.zero, CursorMode.ForceSoftware);
                break;
        }
    }
}
