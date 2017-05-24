using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumperTypeKI : MonoBehaviour {
	public float speed;
	private Rigidbody2D garg;
	private BoxCollider2D gargCollider;
	private int d = -1;
	private bool rotating = false;
	// Use this for initialization
	void Start () {
		garg = GetComponent<Rigidbody2D>();
		gargCollider = GetComponent<BoxCollider2D>();
	}

	// Update is called once per frame
	void Update () {
		var runter = Physics2D.Raycast (gameObject.transform.position, new Vector2 (0f, -1f), 6.0f);
		var linksRunter = Physics2D.Raycast (gameObject.transform.position, new Vector2 (-1f, -1f), 8.0f);
		var rechtsRunter = Physics2D.Raycast (gameObject.transform.position, new Vector2 (1f, -1f), 8.0f);
		if(runter.collider == null){
			if (linksRunter.collider == null) {
				FlyR ();
			} else {
				FlyL
			}
		}
	}

	void FlyR()
	{
		garg.velocity = new Vector2 ((speed), 0f);
	}

	void FlyL()
	{
		garg.velocity = new Vector2 ((-speed), 0f);
	}
}