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
	void Update () {
		if (parent.transform.position.y > 9) {
			Vector3 targetVector = new Vector3 (0, parent.transform.position.y, 0);
			print ("Target: " + targetVector);
			transform.position =  Vector3.Slerp(transform.position, targetVector + offset, Time.deltaTime * multiplier);
		} else {
			transform.position =  Vector3.Slerp(transform.position, new Vector3 (0, 0, 0), Time.deltaTime * multiplier);
		}
		transform.position = new Vector3 (parent.transform.position.x, transform.position.y, -10f) + offset;
	}
}
