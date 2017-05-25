using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highscore : MonoBehaviour {

	// Use this for initialization
	void Start (){
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void saveLength(int length) {
		if (length > PlayerPrefs.GetInt("maxEndlosweite")) {
			PlayerPrefs.SetInt ("maxEndlosweite", length);
		}
		PlayerPrefs.SetInt ("Endlosweite", length);
		print ("Score:" + PlayerPrefs.GetInt ("Endlosweite"));
		print ("Highscore:" + PlayerPrefs.GetInt ("maxEndlosweite"));
	}
}
