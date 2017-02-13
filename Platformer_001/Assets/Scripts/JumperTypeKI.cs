using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumperTypeKI : MonoBehaviour {
	public float speed;
	public float jumpForce;
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
			if(jumpOrder < 2f){
				SpringenVorwaerts();
			}
			else{
				SpringenRueckwaerts();
			}
		} 
	}

	public void SpringenVorwaerts() {
		slime.AddForce(new Vector2(speed, jumpForce), ForceMode2D.Impulse);
	}

	public void SpringenRueckwaerts(){
		slime.AddForce(new Vector2(-speed, jumpForce), ForceMode2D.Impulse);
	}
}