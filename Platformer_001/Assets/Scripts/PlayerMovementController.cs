using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour {
	public float speed;
	public float jumpForce = 5.0f;

    private bool doubleJump;
	private Rigidbody2D rb;
	private Animator anim;
	private BoxCollider2D col;

    public enum Richtung {
        LINKS = -1, RECHTS = 1
    };

	void Start() {
		rb = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
		col = GetComponent<BoxCollider2D>();
        doubleJump = false;
    }

    public void Springen() {
        rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
    }

    public void Bewegen(Richtung richtung) {
        rb.velocity = new Vector2((int)richtung * speed, rb.velocity.y);
        transform.eulerAngles = new Vector3(0, richtung == Richtung.LINKS ? 180.0f : 0.0f, 0);
    }
	
	void Update() {
		if (Input.GetKey(KeyCode.D)) Bewegen(Richtung.RECHTS);
        if (Input.GetKey(KeyCode.A)) Bewegen(Richtung.LINKS);

        bool amBoden = col.IsTouchingLayers();
        anim.SetBool("jump", !amBoden);

        if (Input.GetKeyDown(KeyCode.W) && (amBoden || doubleJump)) {
            Springen();
            doubleJump = amBoden;
        }
        
		anim.SetFloat("speed", rb.velocity.magnitude);
	}
}
