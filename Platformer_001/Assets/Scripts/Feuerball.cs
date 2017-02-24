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
        // Wenn wir ein statisches Objekt (unbewegbare Objekte wie Tiles) treffen, erstellen
        // wir für gute Ästhetik ein Partikelsystem.
        if (wandpartikel != null && collision.contacts.Length > 0 && collision.gameObject.isStatic) {
            // Wir wollen das Partikelsystem in der Mitte der Kontaktpunkte erstellen.
            // Zuerst müssen wir aus allen ContactPoint2Ds das "point" Attribut extrahieren.
            // Das ganze speichern wir in vs.
            Vector2[] vs = new Vector2[collision.contacts.Length];

            for (int i = 0; i < vs.Length; ++i) {
                vs[i] = collision.contacts[i].point;
            }

            var m = Helfer.MittelpunktFinden(vs);

            // Wir rotieren das Partikelsystem um 90° bzw. -90°,
            // damit es entgegen der Wand zeigt und es somit scheint,
            // als würde es aus der Wand herauskommen.
            var rot = Quaternion.AngleAxis((float)bewegungsrichtung * -90.0f, Vector3.forward);

            // Wir erstellen das Partikelsystem mit der gesammelten Information.
            Instantiate(wandpartikel, new Vector3(m.x, m.y, 0.0f), rot);
        }

        // TODO: Betroffenem GameObject Schaden zufügen, wenn möglich

        Destroy(gameObject);
    }
}
