using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leak : MonoBehaviour {

    public KeyCode key;
    public KeyboardKey box;

    private GameObject hole;
    private GameObject keyIcon;

    public void init(KeyboardKey box)
    {
        this.box = box;
        key = box.key;

        hole = transform.Find("Hole").gameObject;
        hole.transform.rotation = Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f));


        keyIcon = transform.Find("Key").gameObject;
        keyIcon.transform.Find("Text").GetComponent<TextMesh>().text = key.ToString();
    }

    // Update is called once per frame
    void Update () {
        if (box.currentState == KeyboardKey.State.HEALTHY)
            return;

        if (box.currentState == KeyboardKey.State.PLUGGED && !Input.GetKey(key))
            box.unplug();
        else if (box.currentState == KeyboardKey.State.LEAKING)
        {
            if (Input.GetKey(key))
            {
                if (box.plug())
                    showKeyIcon(false);
            }
            else
                showKeyIcon(true);
        }
	}

    /// <summary>
    /// Shows or hides the key prompt icon
    /// </summary>
    /// <param name="show">True to show, false to hide.</param>
    private void showKeyIcon(bool show)
    {
        keyIcon.GetComponent<SpriteRenderer>().enabled = show;

        if (show)
            keyIcon.transform.Find("Text").transform.localPosition = new Vector3(0.0f, 0.0f, -0.1f);
        else
            keyIcon.transform.Find("Text").transform.localPosition = new Vector3(0.0f, 0.0f, 1.0f);
    }
}
