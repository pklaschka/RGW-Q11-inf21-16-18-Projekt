using System;
using UnityEngine;

public class Tone : MonoBehaviour {
	public double frequency = 440;

	public enum audioMethod{
		Sinus,
		Sqrt
	}

	[Range(0.01f,5f)]
	public double gain = 0.05f;

	private double increment;
	private double phase;
	private double sampling_frequency = 48000;

	// Use this for initialization
	void Start () {
		
	}

	void OnAudioFilterRead(float[] data, int channels)
	{
		increment = frequency * 2 * Mathf.PI / sampling_frequency;
		for (var i = 0; i < data.Length; i += channels) {
			phase += increment;
			// this is where we copy audio data to make them “available” to Unity
			data [i] = (float)(gain * Math.Sin (phase));

			// if we have stereo, we copy the mono data to each channel
			if (channels == 2)
				data [i + 1] = data [i];
			
			if (phase > 2 * Math.PI)
				phase = 0;
		
		}
	}
}
	
