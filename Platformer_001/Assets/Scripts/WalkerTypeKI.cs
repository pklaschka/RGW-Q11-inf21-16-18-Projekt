using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkerTypeKI : MonoBehaviour {
	public GameObject walkerObject;
	public float speed;

	// Debug:
	public float rayLength = 2f;

	private Rigidbody2D walker;
	private BoxCollider2D walkerCollider;
	// Use this for initialization
	void Start () {
		walker = GetComponent<Rigidbody2D>();
		walkerCollider = GetComponent<BoxCollider2D>();
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 origin = new Vector2 (walkerObject.transform.position.x, walkerObject.transform.position.y);
		Vector2 directionR = new Vector2 (1,-0);
		Vector2 directionL = new Vector2 (-1,-0);
		bool wallCheckRight = Physics2D.Raycast (origin, directionR, rayLength);
		bool wallCheckLeft = Physics2D.Raycast (origin, directionL, rayLength);
		if (!wallCheckLeft) {
			print("NachRechts");
			WalkLeft ();
		}
		if (!wallCheckRight) {
			print ("NachLinks");
			WalkRight ();
		} else {
			WalkLeft ();
		}
	}

	public void WalkLeft(){
	walker.velocity = new Vector2 (-speed, walker.velocity.y);
	}

	public void WalkRight(){
	walker.velocity = new Vector2 (speed, walker.velocity.y);
	}
}

