using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Highscore : MonoBehaviour {
	public Text liveAnzeige;

	public void saveLength(int length) {
		if (liveAnzeige != null) {
			liveAnzeige.text = "" + length;
		}

		if (length > PlayerPrefs.GetInt("maxEndlosweite", 0)) {
			PlayerPrefs.SetInt ("maxEndlosweite", length);
			PlayerPrefs.SetString("maxEndlosweiteConfirmation", getHashed(length));
		}
		PlayerPrefs.SetInt ("Endlosweite", length);
		print ("Score:" + PlayerPrefs.GetInt ("Endlosweite", 0));
		print ("Highscore:" + PlayerPrefs.GetInt ("maxEndlosweite", 0));
	}

	private string getHashed(int length) {
		// TODO: Echten Hash erzeugen
		return "confirming" + length;
	}
}
