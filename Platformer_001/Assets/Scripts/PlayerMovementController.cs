using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour {
	public float speed;
	public float jumpForce = 5.0f;

    private bool doubleJump;
	private Rigidbody2D rb;
	private Animator anim;
	//private BoxCollider2D col;

    public enum Richtung {
        LINKS = -1, RECHTS = 1
    };

	void Start() {
		rb = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
		//col = GetComponent<BoxCollider2D>();
        doubleJump = false;
    }

    public void Springen() {
        rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
    }

    public void Bewegen(Richtung richtung) {
        rb.velocity = new Vector2((int)richtung * speed, rb.velocity.y);

		/*
		 * Für's erste lösen wir den Part über die Skalierung, da ich mich mit 'Shader-Coding' nicht genug auskenne - Pablo
		 * 
        // Bewegen wir uns nach links, dann wird der Spieler um 180° rotiert, wenn nicht, dann nicht.
        // Dadurch zeigt der Spieler nach links, falls es nötig ist.
        // x ? a : b ist if als Ausdruck, auf Deutsch heißt das: bedingung ? wert wenn true : wert wenn false.
        float yDrehung = richtung == Richtung.LINKS ? 180.0f : 0.0f;

        transform.eulerAngles = new Vector3(0, yDrehung, 0);
        */


		// x ? a : b ist if als Ausdruck, auf Deutsch heißt das: bedingung ? wert wenn true : wert wenn false.
		var xScale = richtung == Richtung.LINKS ? -1f : 1f;
		transform.localScale = new Vector3(xScale, 1, 1);
    }
	
	void Update() {
		if (Input.GetKey(KeyCode.D)) Bewegen(Richtung.RECHTS);
        if (Input.GetKey(KeyCode.A)) Bewegen(Richtung.LINKS);

        bool amBoden = rb.velocity.y < 0.001f && rb.velocity.y > -0.001f;
        anim.SetBool("jump", !amBoden);

        if (Input.GetKeyDown(KeyCode.W) && (amBoden || doubleJump)) {
            Springen();
            doubleJump = amBoden;
        }
        
		anim.SetFloat("speed", rb.velocity.magnitude);
	}
}
