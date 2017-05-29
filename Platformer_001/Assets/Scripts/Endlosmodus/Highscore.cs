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
			
			var confirmationString = "confirming" + length + "-1-75_13+" + (length * length) % (length * 3 / 2);
			PlayerPrefs.SetString("maxEndlosweiteConfirmation", confirmationString);
		}
		PlayerPrefs.SetInt ("Endlosweite", length);
		PlayerPrefs.Save();
	}

	private string getHashed(int length) {
		// TODO: Echten Hash erzeugen
		return "confirming" + length;
	}
}
