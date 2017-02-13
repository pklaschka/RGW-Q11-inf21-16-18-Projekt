using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LebenController : MonoBehaviour {
	public int leben = 1;

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
