using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {
	public string NameSceneToLoad;

	public Image BackgroundImage;
	public Slider ProgressBar;

	private bool sceneLoading;
	AsyncOperation async;

	public void Start() {
		sceneLoading = false;
	}

	public void LoadScene() {
		print ("Loading scene...");
		if (NameSceneToLoad != null && BackgroundImage != null && ProgressBar != null) {
			BackgroundImage.GetComponent<Image>().enabled = true;

			sceneLoading = true;
			StartCoroutine(StartSceneLoad());
		} else {
			Debug.LogError("Szene konnte nicht geladen werden, da ein nötiger Paramter nicht festgelegt wurde.");
		}
	}

	public void LoadLastLevel() {
		SceneManager.LoadScene("Kampagne");
	}

	private IEnumerator StartSceneLoad() {
		async = SceneManager.LoadSceneAsync(NameSceneToLoad);
		if( null != async )
			async.allowSceneActivation = false;
		yield return async;
	}

	public void Update() {
		if (!sceneLoading) return;
		ProgressBar.value = async.progress/0.9f;
		if (async.progress >= 0.9f)
			async.allowSceneActivation = true;
	}

	public void CloseGame()
	{
		Application.Quit();
	}
}
