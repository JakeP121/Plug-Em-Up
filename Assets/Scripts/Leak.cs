using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leak : MonoBehaviour {

    public KeyboardKey key;

    public enum State { HEALTHY, PLUGGED, LEAKING, BOARDING };
    public State currentState;

    private GameObject hole;
    private GameObject keyIcon;

    private Finger finger;

    private GameObject board;

    private Game game;
    private Waves waves;

    private float waterFillSpeed = 1.0f;
    private float waterFillCounter = 0.0f;

    private float boardingTime = 5.0f;
    private float boardingCounter = 0.0f;

    public void init(KeyboardKey key)
    {
        this.key = key;

        currentState = State.LEAKING;

        transform.position = key.position;

        game = FindObjectOfType<Game>();
        waves = FindObjectOfType<Waves>();

        hole = transform.Find("Hole").gameObject;
        hole.transform.rotation = Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f));

        keyIcon = transform.Find("Key").gameObject;
        keyIcon.transform.Find("Text").GetComponent<TextMesh>().text = key.keyCode.ToString();
    }

    // Update is called once per frame
    void Update () {
        if (currentState == State.HEALTHY)
            Destroy(this.gameObject);

        if (currentState == State.BOARDING)
        {
            boardingCounter += Time.deltaTime;

            if (boardingCounter >= boardingTime)
                stopBoarding();
        }

        handleInput();

        updateWater();
	}

    /// <summary>
    /// Checks if this hole should be plugged/unplugged
    /// </summary>
    private void handleInput()
    {
        if (currentState == State.PLUGGED && !Input.GetKey(key.keyCode))
            unplug();
        else if (currentState == State.LEAKING)
        {
            if (Input.GetKey(key.keyCode))
            {
                if (finger == null)
                    moveToPlug();
            }
            else
                unplug();
        }
    }

    private void OnMouseDown()
    {
        Mouse mouse = FindObjectOfType<Mouse>();

        if (mouse.currentState == Mouse.State.BOARDING && board == null)
            startBoarding();
    }

    /// <summary>
    /// Increases the water level every second when leaking
    /// </summary>
    private void updateWater()
    {
        if (currentState != State.LEAKING)
            return;

        waterFillCounter += Time.deltaTime;

        if (waterFillCounter >= 1.0f)
        {
            if (currentState == State.LEAKING)
                waves.increaseWater(waterFillSpeed);

            waterFillCounter = 0.0f;
        }
    }




    /// <summary>
    /// Attempts to find an available finger and signals it to plug this hole
    /// </summary>
    private void moveToPlug()
    {
        finger = game.getFreeFinger();

        if (finger != null)
        {
            finger.moveToPlugHole(this.gameObject);
            showKeyIcon(false);
        }
    }

    /// <summary>
    /// Plugs the hole (stops increasing water level and hides particle effect)
    /// </summary>
    public void plug()
    {
        currentState = State.PLUGGED;

        waterFillCounter = 0.0f;

        showParticles(false);
    }

    /// <summary>
    /// Unplugs the hole (starts increasing water level and shows particle effect)
    /// </summary>
    private void unplug()
    {
        currentState = State.LEAKING;

        if (finger != null)
            finger.retract();
        finger = null;

        showParticles(true);

        showKeyIcon(true);
    }

    private void startBoarding()
    {
        Mouse mouse = FindObjectOfType<Mouse>();

        currentState = State.BOARDING;

        showParticles(false);
        showKeyIcon(false);

        board = Instantiate(Resources.Load("Board")) as GameObject;
        board.GetComponent<Board>().init(this.transform);

        mouse.switchState();
    }

    private void stopBoarding()
    {
        Mouse mouse = FindObjectOfType<Mouse>();

        currentState = State.LEAKING;

        showParticles(true);
        showKeyIcon(true);

        board.GetComponent<Board>().fall();

        mouse.switchState();
    }

    /// <summary>
    /// Shows or hides the key prompt icon
    /// </summary>
    /// <param name="show">True to show, false to hide.</param>
    private void showKeyIcon(bool show)
    {
        keyIcon.GetComponent<SpriteRenderer>().enabled = show;

        if (show)
            keyIcon.transform.Find("Text").GetComponent<TextMesh>().text = key.keyCode.ToString();
        else
            keyIcon.transform.Find("Text").GetComponent<TextMesh>().text = "";
    }

    private void showParticles(bool show)
    {
        ParticleSystem.EmissionModule psEmission = GetComponentInChildren<ParticleSystem>().emission;
        psEmission.enabled = show;
    }
}
