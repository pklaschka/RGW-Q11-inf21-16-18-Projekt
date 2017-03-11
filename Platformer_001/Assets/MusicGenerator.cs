using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class MusicGenerator : MonoBehaviour {

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad (transform.gameObject);
		r = new System.Random();
	}

	public double frequency = 440;
	[Range(0.01,5)]
	public double gain = 1;

	private double increment;
	private double phase;
	private double sampling_frequency = 48000;

	public double ticksPerBar = 100;
	public double ticks;
	private bool noteChanged = true;

	private double[] notes = {
		261.63,
		293.66,
		329.63,
		349.23,
		392,
		440,
		493.88
	};

	private System.Random r;

	void FixedUpdate() {
		ticks += Time.deltaTime;
		while (ticks > ticksPerBar) {
			ticks -= ticksPerBar;
			noteChanged = false;
		}
		if (ticks < 5 && !noteChanged) {
			int nextIndex = r.Next (0, 6);
			print (notes[nextIndex]);
			frequency = notes [nextIndex];
			noteChanged = !noteChanged;
		}
	}

	void OnAudioFilterRead(float[] data, int channels)
	{
		// update increment in case frequency has changed
		increment = frequency * 2 * Math.PI / sampling_frequency;
		for (var i = 0; i < data.Length; i += channels)
		{
			phase += increment;
			// this is where we copy audio data to make them “available” to Unity
			data[i] = (float)(gain*Math.Sin(phase));
			// if we have stereo, we copy the mono data to each channel
			if (channels == 2) data[i + 1] = data[i];
			if (phase > 2 * Math.PI) phase = 0;
		}
	}
}
