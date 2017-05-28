using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverErgebnis : MonoBehaviour {
	public string aktuellesErgebnisPrefix = "Ihr Ergebnis:";
	public string rekordErgebnisPrefix = "Ihr Rekord:";

	private UnityEngine.UI.Text text;

	public void Start () {
		text = GetComponent<UnityEngine.UI.Text> ();

		var aktuell = PlayerPrefs.GetInt ("Endlosweite", 0) + "";
		var rekord  = PlayerPrefs.GetInt ("maxEndlosweite", 0) + "";

		text.text = aktuellesErgebnisPrefix + " " + aktuell
		+ "\n\r"
		+ rekordErgebnisPrefix + " " + rekord;
	}
}
