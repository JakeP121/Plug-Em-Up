using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScreen : MonoBehaviour {

    public float riseSpeed = 5.0f; // How fast the screen rises
    private bool rising = false; // If the screen is currently rising

	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();

        if (rising)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + (riseSpeed * Time.deltaTime), transform.position.z);

            if (transform.position.y >= 0.0f)
            {
                rising = false;
                UnityEngine.SceneManagement.SceneManager.LoadScene(2);
            }

            return;
        }
	}

    /// <summary>
    /// Start rising over game screen
    /// </summary>
    public void rise()
    {
        rising = true;
    }

    /// <summary>
    /// Go back to main menu 
    /// </summary>
    private void OnMouseDown()
    {
        if (!rising)
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
