﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBoard : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<UnityEngine.UI.Text>().text = PlayerPrefs.GetInt("Score").ToString();
	}
}
