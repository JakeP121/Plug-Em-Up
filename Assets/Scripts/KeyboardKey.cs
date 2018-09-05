using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardKey {

    public KeyCode keyCode;

    public Vector3 position;
    public Vector3 size;

    public bool hasALeak = false;

    public KeyboardKey(KeyCode keyCode)
    {
        this.keyCode = keyCode;
    }
}