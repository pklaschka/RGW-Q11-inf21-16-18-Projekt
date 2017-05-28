using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class KampagneSceneLoader : SceneLoader {
	public int sceneIndex = 0;

	public void LoadScene() {
		PlayerPrefs.SetInt ("level_current", sceneIndex);
		PlayerPrefs.Save ();
		base.LoadScene ();
	}
}
