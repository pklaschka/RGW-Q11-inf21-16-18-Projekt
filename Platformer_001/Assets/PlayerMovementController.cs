using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour {
	public float speed;

	private Rigidbody2D rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.D)) {
			rb.velocity = new Vector2 (speed, rb.velocity.y);
		}
		if (Input.GetKey(KeyCode.A)) {
			rb.velocity = new Vector2 (-speed, rb.velocity.y);
		}
		if (Input.GetKeyDown(KeyCode.W)) {
			rb.AddForce(new Vector2(0,5f), ForceMode2D.Impulse);
		}
	}
}
