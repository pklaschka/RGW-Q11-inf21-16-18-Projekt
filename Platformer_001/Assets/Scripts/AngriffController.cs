using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngriffController : MonoBehaviour {
    public GameObject fireball;

    public void SchwertAngriff() {
        // TODO: Muss noch implementiert werden.
    }

    private GameObject ProjektilFeuern(GameObject prefab, float abstand = 2.5f) {
        var ply = FindObjectOfType<PlayerMovementController>();

        var pos = new Vector3(
            ply.transform.position.x + (float)ply.richtung * abstand,
            ply.transform.position.y,
            ply.transform.position.z
        );

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
        var feuerball = proj.GetComponent<Feuerball>();
        if (feuerball != null) feuerball.bewegungsrichtung = ply.richtung;
    }
}
