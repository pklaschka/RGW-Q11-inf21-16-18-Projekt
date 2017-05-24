using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour {
	private GameObject[] tones;

	public int beatsPerMinute = 120;
	public int quartersPerBar = 4;
	private float ticks = 0f;

	// Computational Attributes
	private int lastBar = 0;
	private int lastEight = 0;

	void Awake() {
		DontDestroyOnLoad(this);

		if (FindObjectsOfType(GetType()).Length > 1)
		{
			Destroy(gameObject);
		}
	}

	// Use this for initialization
	void Start () {
		tones = new GameObject[] {
			transform.Find("l1").gameObject,
			transform.Find("l2").gameObject,
			transform.Find("l3").gameObject,
			transform.Find("l4").gameObject,
			transform.Find("l5").gameObject,
			transform.Find("l6").gameObject,
			transform.Find("l7").gameObject,
			transform.Find("h1").gameObject,
			transform.Find("h2").gameObject,
			transform.Find("h3").gameObject,
			transform.Find("h4").gameObject,
			transform.Find("h5").gameObject,
			transform.Find("h6").gameObject,
			transform.Find("h7").gameObject
		};
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		ticks += Time.deltaTime;

		var currentBar = Mathf.FloorToInt(ticks * beatsPerMinute / (quartersPerBar * 60f));
		var currentEight = Mathf.FloorToInt (/*2 **/ ticks * beatsPerMinute / 60f);
		while (currentEight > quartersPerBar) {
			currentEight -= quartersPerBar;
		}

		print (lastEight);
		if (currentEight != this.lastEight) {
			deactivateTones ();
			this.lastEight = currentEight;

			playTone (Random.Range(1, 11));
			//print("Bar: " + currentBar + "/" + currentEight);
		}
		print (lastEight);

		// Change Chord after Bar:
		if (currentBar != lastBar) {
			lastBar = currentBar;

			playChord (currentBar);
		}
	}

	void playTone(int number) {
		if (7 + number < tones.Length) {
			tones [7 + number].SetActive (true);
		}
	}

	void playChord(int number) {
		while (number > 4) {
			number -= 4;
		}

		switch (number) {
		default:
			number = 1;
			break;
		case 1:
			number = 1;
			break;
		case 2:
			number = 5;
			break;
		case 3:
			number = 6;
			break;
		case 4:
			number = 4;
			break;
		}

		deactivateAll();

		tones [number - 1].SetActive (true);
		tones [number - 1 + 2].SetActive (true);
		tones [number - 1 + 4].SetActive (true);
	}

	void deactivateAll() {
		foreach (var tone in tones) {
			tone.SetActive (false);
		}
	}

	void deactivateTones() {
		for (int i = 6; i < tones.Length; i++) {
			tones [i].SetActive (false);
		}
	}
}
