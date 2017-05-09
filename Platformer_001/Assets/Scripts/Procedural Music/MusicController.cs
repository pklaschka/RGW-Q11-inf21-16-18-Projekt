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

		if (currentBar != lastBar) {
			lastBar = currentBar;

			print("Bar: " + currentBar);
			playChord (currentBar);
		}
	}

	void playChord(int number) {
		while (number > 7) {
			number -= 7;
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
}
