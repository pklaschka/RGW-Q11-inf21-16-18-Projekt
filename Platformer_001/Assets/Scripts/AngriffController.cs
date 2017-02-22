using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngriffController : MonoBehaviour {
    public GameObject fireball;

    public void SchwertAngriff() {
        // TODO: Muss noch implementiert werden.
    }

    private void ProjektilFeuern(GameObject prefab) {
        var ply = FindObjectOfType<PlayerMovementController>();
        bool rechts = ply.richtung == PlayerMovementController.Richtung.RECHTS;
        float ad = 2.0f;
        float d = rechts ? -ad : ad;

        var pos = new Vector3(
            ply.transform.position.x + ad,
            ply.transform.position.y,
            ply.transform.position.z
        );

        var proj = Instantiate(prefab, pos, Quaternion.identity);
        proj.transform.localScale = new Vector3(
            (rechts ? 1.0f : -1.0f) * prefab.transform.localScale.x,
            prefab.transform.localScale.y,
            prefab.transform.localScale.z
        );
    }

    public void WandAngriff() {
        ProjektilFeuern(fireball);
    }
}
