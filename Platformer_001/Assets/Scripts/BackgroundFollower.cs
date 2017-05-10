using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundFollower : MonoBehaviour {

	public GameObject Player;
	public GameObject Background;
	public Vector3 offset;
	float pos = 0;
	GameObject bg1;
	GameObject bg2;
	GameObject bg3;
	 //Use this for initialization
	void Start () {
		bg1 = Instantiate(Background);
		bg2 = Instantiate (Background);
		bg3 = Instantiate (Background);
		bg1.transform.position = new Vector3 (0, 0, 0) + offset;
		bg2.transform.position = new Vector3 (0, 0, 0) + offset;
		bg3.transform.position = new Vector3 (60, 0, 0) + offset;
	}
	
	// Update is called once per frame
	void Update () {
		if(Player.transform.position.x >= pos + 60f){
			bg2.transform.position = new Vector3(pos + 60, 0) + offset;
			bg3.transform.position = new Vector3 (pos + 120, 0) + offset;
			var bg4 = bg1;
			bg1 = bg2;
			bg2 = bg4;
			pos += 60;
		}
	}
}
