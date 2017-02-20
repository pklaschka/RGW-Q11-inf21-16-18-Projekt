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
		WalkRight ();
		bool wallCheckRight = Physics2D.Raycast (new Vector2 (walkerObject.transform.position.x, walkerObject.transform.position.y), new Vector2 ((walkerObject.transform.position.x + 1), walkerObject.transform.position.y), rayLength);
		bool wallCheckLeft = Physics2D.Raycast (new Vector2 (walkerObject.transform.position.x, walkerObject.transform.position.y), new Vector2 ((walkerObject.transform.position.x - 1), walkerObject.transform.position.y), rayLength);
		if (wallCheckLeft) {
			WalkRight ();
			print("NachRechts");
		}
		if (wallCheckRight) {
			WalkLeft ();
			print ("NachLinks");
		}
	}

	public void WalkLeft(){
	walker.velocity = new Vector2 (-speed, walker.velocity.y);
	}

	public void WalkRight(){
	walker.velocity = new Vector2 (speed, walker.velocity.y);
	}
}

