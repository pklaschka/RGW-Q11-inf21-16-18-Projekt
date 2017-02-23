using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feuerball : MonoBehaviour {
    public float geschwindigkeit = 50.0f;
    public float lebensdauer = 2.0f;
    public Richtung bewegungsrichtung = Richtung.RECHTS;

    public GameObject wandpartikel;

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
        if (wandpartikel != null && collision.contacts.Length > 0 && collision.gameObject.isStatic) {
            Vector2[] vs = new Vector2[collision.contacts.Length];

            for (int i = 0; i < vs.Length; ++i) {
                vs[i] = collision.contacts[i].point;
            }

            var m = Helfer.MittelpunktFinden(vs);
            var rot = Quaternion.AngleAxis((float)bewegungsrichtung * -90.0f, Vector3.forward);
            var wp = Instantiate(wandpartikel, new Vector3(m.x, m.y, 0.0f), rot);
        }
        // TODO: Betroffenem GameObject Schaden zufügen, wenn möglich
        Destroy(gameObject);
    }
}
