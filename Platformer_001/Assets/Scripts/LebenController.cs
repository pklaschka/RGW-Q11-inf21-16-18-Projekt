using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LebenController : MonoBehaviour {
	public int leben;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{

	}
	public void GameOver()
	{
		print ("Du bist Tod!");
		SceneManager.LoadScene("Game_Over_Screen");
	}

	public void Sterben()
	{
		leben--;
		if (leben <= 0) 
		{
			GameOver ();
		}
	}
}
