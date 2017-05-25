using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverErgebnis : MonoBehaviour {
	public string aktuellesErgebnisPrefix = "Ihr Ergebnis:";
	public string rekordErgebnisPrefix = "Ihr Rekord:";

	private UnityEngine.UI.Text text;

	void Start () {
		text = GetComponent<UnityEngine.UI.Text> ();

		string aktuell = PlayerPrefs.GetInt ("Endlosweite", 0) + "";
		string rekord  = PlayerPrefs.GetInt ("maxEndlosweite", 0) + "";

		text.text = aktuellesErgebnisPrefix + " " + aktuell
		+ "\n\r"
		+ rekordErgebnisPrefix + " " + rekord;
	}
}
