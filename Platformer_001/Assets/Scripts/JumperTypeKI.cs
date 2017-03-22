using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumperTypeKI : MonoBehaviour {
	public float speed;
	public float jumpForce;
	public float highJumpForce;
	private Rigidbody2D slime;
	private BoxCollider2D slimeCollider;
	// Use this for initialization
	void Start () {
		slime = GetComponent<Rigidbody2D>();
		slimeCollider = GetComponent<BoxCollider2D>();
	}

	// Update is called once per frame
	void Update () {
		bool JumpReady = slime.velocity.y < 0.001f && slime.velocity.y > -0.001f && slime.velocity.x < 0.001f && slime.velocity.x > -0.001f;
		if (JumpReady) {
			float jumpOrder = Random.Range(0,4);
			if(jumpOrder < 1f){
				SpringenVorwaerts();
			}
			else{
				if (jumpOrder < 2f) {
					SpringenRueckwaerts ();
				} 
				else {
					if (jumpOrder < 3f) {
						SpringenHochRueckwaerts ();
					} 
					else {
						SpringenHochVorwaerts();
						}
					}
				}
		} 
	}

	public void SpringenVorwaerts() {
		slime.AddForce(new Vector2(speed, jumpForce), ForceMode2D.Impulse);
	}

	public void SpringenRueckwaerts(){
		slime.AddForce(new Vector2(-speed, jumpForce), ForceMode2D.Impulse);
	}

	public void SpringenHochVorwaerts(){
		slime.AddForce(new Vector2((speed/2), highJumpForce), ForceMode2D.Impulse);
	}

	public void SpringenHochRueckwaerts(){
		slime.AddForce(new Vector2((-(speed/2)), highJumpForce), ForceMode2D.Impulse);
	}
}