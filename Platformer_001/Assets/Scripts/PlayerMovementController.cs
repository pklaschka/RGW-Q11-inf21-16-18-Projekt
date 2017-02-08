using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour {
	public float speed;
	public float jumpForce;

    private bool doublejump;
	private Rigidbody2D rb;
	private Animator anim;
	private BoxCollider2D col;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
		col = GetComponent<BoxCollider2D> ();
        doublejump = false;
    }
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.D)) {
			rb.velocity = new Vector2 (speed, rb.velocity.y);
			transform.eulerAngles = new Vector3 (0, 0, 0);
		}
		if (Input.GetKey(KeyCode.A)) {
			rb.velocity = new Vector2 (-speed, rb.velocity.y);
			transform.eulerAngles = new Vector3 (0, 180f, 0);
		}
		
		if (col.IsTouchingLayers ()) {
			anim.SetBool ("jump", false);
            if (Input.GetKeyDown(KeyCode.W))
            {
				rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                doublejump = true;
            }
        } else {
			anim.SetBool ("jump", true);
            if(doublejump)
            {
                if (Input.GetKeyDown(KeyCode.W))
                {
					rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                    doublejump = false;
                }
            }
		}

		// spped parameter setzen
		anim.SetFloat("speed", rb.velocity.magnitude);
	}
}
