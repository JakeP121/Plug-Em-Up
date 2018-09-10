using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instructions : MonoBehaviour {

    public List<Sprite> instructionImages = new List<Sprite>();

    private int currentInstruction = -1;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            currentInstruction++;

            if (instructionImages.Count > currentInstruction)
                spriteRenderer.sprite = instructionImages[currentInstruction];
            else
                UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        }
    }

}
