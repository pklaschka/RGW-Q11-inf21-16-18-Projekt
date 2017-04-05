using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionFollower : MonoBehaviour {
	public GameObject parent;

	public Vector3 offset;
	public float multiplier;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		var a = new Vector3 (parent.transform.position.x, transform.position.y, -10f);
		var b = new Vector3 (parent.transform.position.x, parent.transform.position.y + 2, -10f);
		transform.position = Vector3.Slerp(a, b, Time.deltaTime*2);
		transform.position = new Vector3 (
			(float)Math.Round (transform.position.x, 3),
			(float)Math.Round (transform.position.y, 3),
			(float)Math.Round (transform.position.z, 3)
		);
	}
}
