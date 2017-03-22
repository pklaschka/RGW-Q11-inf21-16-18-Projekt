using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundFollower : MonoBehaviour {

	public GameObject Player;
	float pos;
	// Use this for initialization
	void Start () {
		pos = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if(Player.transform.position.x >= pos + 15f){
			this.gameObject.transform.position += new Vector3(0,15);
			pos += 15;
		}
	}
}
