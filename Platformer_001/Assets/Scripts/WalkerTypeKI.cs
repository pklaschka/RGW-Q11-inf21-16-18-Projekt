using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkerTypeKI : MonoBehaviour {
	public GameObject walkerObject;
	public float speed;

	// Debug:
	public float rayLength = 3f;

	private Rigidbody2D walker;
	private BoxCollider2D walkerCollider;
	int d = -1;

	// Use this for initialization
	void Start () {
		walker = GetComponent<Rigidbody2D>();
		walkerCollider = GetComponent<BoxCollider2D>();
	}
	
	// Update is called once per frame
	void Update () {
		//Vector2 origin = new Vector2(walkerObject.transform.position.x, walkerObject.transform.position.y);
		//bool wallCheckRight = Physics2D.Raycast(origin, Vector2.right, rayLength);
		//bool wallCheckLeft = Physics2D.Raycast(origin, Vector2.left, rayLength);
		print(walker.velocity);
		if (walker.velocity.magnitude < 0.1) {
			d = d * -1;
		}
		Walk (d);
			/*if (wallCheckLeft) {
				WalkRight ();
			} else if (wallCheckRight) {
				WalkLeft ();
			}*/
	}

	public void Walk(int direction){
		walker.velocity = new Vector2 ((direction * speed), walker.velocity.y);
	}

	public void WalkLeft(){
	    walker.velocity = new Vector2 (-speed, walker.velocity.y);
	}

	public void WalkRight(){
	    walker.velocity = new Vector2 (speed, walker.velocity.y);
	}
}

