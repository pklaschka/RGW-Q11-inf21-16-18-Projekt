using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionFollower : MonoBehaviour {
	public GameObject parent;

	public float offset;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		Vector3 a = new Vector3 (parent.transform.position.x, transform.position.y, -10f);
		Vector3 b = new Vector3 (parent.transform.position.x, parent.transform.position.y + offset, -10f);
		transform.position = Vector3.Slerp (a, b, Time.deltaTime*2);

	}
}
