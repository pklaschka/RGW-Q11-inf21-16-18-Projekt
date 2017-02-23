using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feuerball : MonoBehaviour {
    public float geschwindigkeit = 50.0f;
    public float lebensdauer = 2.0f;
    public Richtung bewegungsrichtung = Richtung.RECHTS;

    private float lebenszeit = 0.0f;
    
	void Update() {
        lebenszeit += Time.deltaTime;

        if (lebenszeit >= lebensdauer) {
            Destroy(gameObject);
            return;
        }

        var rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2((float)bewegungsrichtung * geschwindigkeit, 0.0f);
    }

    void OnCollisionEnter2D(Collision2D collision) {
        // TODO: Betroffenem GameObject Schaden zufügen, wenn möglich
        Destroy(gameObject);
    }
}
