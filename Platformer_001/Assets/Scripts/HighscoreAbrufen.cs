using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighscoreAbrufen : MonoBehaviour {

	int score;
	int highscore;
	Text Anzeige;

	// Use this for initialization
	void Start () {
		score = PlayerPrefs.GetInt ("Endlosweite");
		highscore = PlayerPrefs.GetInt("maxEndlosweite");
		Anzeige = GetComponent<Text>();
	}

	// Update is called once per frame
	void Update () {
		Anzeige.text = "" + PlayerPrefs.GetInt("maxEndlosweite");
	}
}
