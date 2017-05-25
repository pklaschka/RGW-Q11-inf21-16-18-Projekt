using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @author Pablo Klaschka
*/
public class LevelAuswahl : MonoBehaviour {
	[Header("Button Prefab")]
	public GameObject btnPrefab;

	[Header("Scene Loader Prefabs")]
	public GameObject slider;
	public GameObject background;

	[Header("Leveleintellungen")]
	public int anzahlLevel;
	public int freigeschaltetBis = 0;

	// Use this for initialization
	void Start () {
		freigeschaltetBis = PlayerPrefs.GetInt ("unlocked_to_level_index", 1);

		for (int i = 0; i < anzahlLevel; i++) {
			GameObject btn = Instantiate<GameObject> (btnPrefab);
			btn.transform.SetParent(transform);
			btn.transform.localScale = new Vector3 (1, 1, 1);
			btn.transform.GetChild (0).GetComponent <UnityEngine.UI.Text>().text = "" + (i + 1);
			btn.GetComponent<KampagneSceneLoader> ().sceneIndex = i;
			btn.GetComponent<UnityEngine.UI.Button> ().interactable = (i <= freigeschaltetBis);
			btn.GetComponent<KampagneSceneLoader> ().ProgressBar = slider.GetComponent<UnityEngine.UI.Slider>();
			btn.GetComponent<KampagneSceneLoader> ().BackgroundImage = background.GetComponent<UnityEngine.UI.Image>();

			btn.GetComponent<UnityEngine.UI.Button> ().onClick.AddListener(btn.GetComponent<KampagneSceneLoader> ().LoadScene);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
