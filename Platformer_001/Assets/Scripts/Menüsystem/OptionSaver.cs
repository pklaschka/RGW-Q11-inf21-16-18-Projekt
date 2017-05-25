using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionSaver : MonoBehaviour {
	private MusicController music;
	public Slider musikLautstaerke;

	// Use this for initialization
	void Start () {
		musikLautstaerke.value = PlayerPrefs.GetFloat ("options_volume_music", 0.1f);
		//music = GameObject.Find ("MusicController").GetComponent<MusicController>();
	}

	public void VolumeUpdate() {
		PlayerPrefs.SetFloat ("options_volume_music", musikLautstaerke.value);
		//music.setGain (musikLautstaerke.value);
		print ("Volume Updated");
	}
}
