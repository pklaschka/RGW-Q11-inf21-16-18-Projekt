﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkerTypeKI : MonoBehaviour {
	public GameObject walkerObject;
	public float speed;

	// Debug:
	public float rayLength = 3f;

	private Rigidbody2D walker;
	private BoxCollider2D walkerCollider;

	// Use this for initialization
	void Start () {
		walker = GetComponent<Rigidbody2D>();
		walkerCollider = GetComponent<BoxCollider2D>();
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 origin = new Vector2(walkerObject.transform.position.x, walkerObject.transform.position.y);
		bool wallCheckRight = Physics2D.Raycast(origin, Vector2.right, rayLength);
		bool wallCheckLeft = Physics2D.Raycast(origin, Vector2.left, rayLength);

        if (!wallCheckLeft) {
            WalkLeft();
        } else if (!wallCheckRight) {
            WalkRight();
        }
	}

	public void WalkLeft(){
	    walker.velocity = new Vector2 (-speed, walker.velocity.y);
	}

	public void WalkRight(){
	    walker.velocity = new Vector2 (speed, walker.velocity.y);
	}
}

