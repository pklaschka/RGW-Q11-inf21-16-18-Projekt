using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngriffController : MonoBehaviour {
    public GameObject fireball;

    public void SchwertAngriff() {
		var playerObj = GameObject.FindGameObjectWithTag("Player");
		var bc = playerObj.GetComponent<BoxCollider2D>();
		var pmc = playerObj.GetComponent<PlayerMovementController>();

		var hits = new RaycastHit2D[8];
		int c = bc.Raycast(pmc.richtung == Richtung.RECHTS ? Vector2.right : Vector2.left, hits);

		for (int i = 0; i < c; ++i) {
			var hit = hits [i];

			if (hit.distance > 4.0f) {
				// Keinen Schaden hinzufügen, wenn der Gegner ausser Reichweite ist.
				continue;
			}

			if (hit.collider && hit.collider.gameObject) {
				var gobj = hit.collider.gameObject;

				if (SchadenAdapter.IstGegner(gobj)) {
					SchadenAdapter.SchadenZufuegen(gobj, 10);
				}
			}
		}
    }

    private GameObject ProjektilFeuern(GameObject prefab, float abstand = 2.5f) {
        var ply = FindObjectOfType<PlayerMovementController>();

        // Wir finden mit Hinsicht auf die Spielerrichtung eine Position,
        // wo das Projektil erstellt werden kann.
        var pos = new Vector3(
            ply.transform.position.x + (float)ply.richtung * abstand,
            ply.transform.position.y,
            ply.transform.position.z
        );

        // Das Projektil wird erstellt.
        var proj = Instantiate(prefab, pos, Quaternion.identity);
        proj.transform.localScale = new Vector3(
            (float)ply.richtung * prefab.transform.localScale.x,
            prefab.transform.localScale.y,
            prefab.transform.localScale.z
        );

        return proj;
    }

    public void WandAngriff() {
        var proj = ProjektilFeuern(fireball);
        var ply = FindObjectOfType<PlayerMovementController>();

        // Die Feuerball-Komponente muss wissen, in welche Richtung der Feuerball
        // sich bewegen soll, sonst würde er sich nur nach rechts bewegen,
        // egal wohin der Spieler zeigt.
        var feuerball = proj.GetComponent<Feuerball>();
        if (feuerball != null) feuerball.bewegungsrichtung = ply.richtung;
    }
}
