using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlugEmUp
{
    public class ScoreBoard : MonoBehaviour {

        // Use this for initialization
        void Start() {
            transform.Find("Score Value").GetComponent<UnityEngine.UI.Text>().text = Score.getCurrentScore().ToString();
            transform.Find("High Score Value").GetComponent<UnityEngine.UI.Text>().text = PlayerPrefs.GetInt("High score").ToString();
        }
    }
}