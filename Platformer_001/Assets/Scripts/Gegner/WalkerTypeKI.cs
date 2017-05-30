using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkerTypeKI : MonoBehaviour {
	public GameObject walkerObject;
	public float speed;
	public float radiusCollider;
	private Rigidbody2D walker;
	private BoxCollider2D walkerCollider;
	public bool walking = true;
	int d = -1;
	bool rotating;


	// Use this for initialization
	void Start () {
		walker = GetComponent<Rigidbody2D>();
		//print("got rb2D");
		walkerCollider = GetComponent<BoxCollider2D>();
		//print ("got Collider2D");
	}

	// Update is called once per frame
	void Update () {
		if (d == 1) {
			var rechts = Physics2D.Raycast (gameObject.transform.position, new Vector2(1.0f, -0.1f), 4.0f);
			var rechtsUnten = Physics2D.Raycast (transform.position, new Vector2(1.0f, -0.75f), 4.0f);
			if ((rechts.collider != null && rechts.distance < radiusCollider) || (rechtsUnten.collider == null)) {
				d = d * -1;
				Walk (d);
				rotating = false;
			} else {
				Walk (d);
			}
		} else {
			var links = Physics2D.Raycast (gameObject.transform.position, new Vector2 (-1.0f, -0.1f), 4.0f);
			var linksUnten = Physics2D.Raycast (transform.position, new Vector2 (-1.0f, -0.75f), 4.0f);
			if ((links.collider != null && links.distance < radiusCollider) || (linksUnten.collider == null)) { 
				d = d * -1;
				Walk (d);
				rotating = false;
			} else {
				Walk (d);
			}
		}
	}

	public void Walk(int direction){
		walker.velocity = new Vector2 ((direction * speed), walker.velocity.y);
		//rotating = false;
		float yRotation = (direction == 1f) ? 0 : 180f;
		transform.eulerAngles = new Vector3 (0, yRotation);
		rotating = true;
	}

	public void WalkLeft(){
	    walker.velocity = new Vector2 (-speed, walker.velocity.y);
	}

	public void WalkRight(){
	    walker.velocity = new Vector2 (speed, walker.velocity.y);
	}
}

