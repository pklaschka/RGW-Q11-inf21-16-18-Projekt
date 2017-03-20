using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkerTypeKI : MonoBehaviour {
	public GameObject walkerObject;
	public float speed;
	private Rigidbody2D walker;
	private BoxCollider2D walkerCollider;
	int d = -1;

	// Use this for initialization
	void Start () {
		walker = GetComponent<Rigidbody2D>();
		print("got rb2D");
		walkerCollider = GetComponent<BoxCollider2D>();
		print ("got Collider2D");
	}

	// Update is called once per frame
	void Update () {
		var links = Physics2D.Raycast (gameObject.transform.position, Vector2.left, 8.0f);
		var rechts = Physics2D.Raycast (gameObject.transform.position, Vector2.right, 8.0f);
		var linksUnten = Physics2D.Raycast (transform.position, new Vector2(-1.0f, -1.0f), 8.0f);
		var rechtsUnten = Physics2D.Raycast (transform.position, new Vector2(1.0f, -1.0f), 8.0f);
		Debug.DrawRay (gameObject.transform.position, Vector2.left * 8.0f, Color.red, 0.1f, false);
		Debug.DrawRay (gameObject.transform.position, Vector2.right * 8.0f, Color.green, 0.1f, false);

		print (links.distance);

		if ((links.collider != null && links.distance < 2.2) || (rechts.collider != null && rechts.distance < 2.2) || 
			(linksUnten.collider == null) || (rechtsUnten.collider == null)) {
			print (links.distance + " " + rechts.distance);
			d = d * -1;
			print ("direction Changed");
		}

		Walk(d);
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

