using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aufhebbar : MonoBehaviour {
    public GameObject itemPrefab;

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            var nachricht = FindObjectOfType<UINachricht>();
            if (nachricht != null) {
                nachricht.Anzeigen(
                    NachrichtenArt.DRUECKEN_ZUM_AUFHEBEN, "'Q' drücken um aufzuheben", 2.0f
                );
            }
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        var nachricht = FindObjectOfType<UINachricht>();
        if (nachricht != null && nachricht.ArtGeben() == NachrichtenArt.DRUECKEN_ZUM_AUFHEBEN) {
            nachricht.SofortAusblenden();
        }
    }

    void OnTriggerStay2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            if (Input.GetButtonUp("Aufheben")) {
                var itemController = other.gameObject.GetComponent<ItemController>();
                if (itemController == null) return;

                var nachricht = FindObjectOfType<UINachricht>();

                if (nachricht != null && nachricht.ArtGeben() == NachrichtenArt.DRUECKEN_ZUM_AUFHEBEN) {
                    nachricht.SofortAusblenden();
                }

                var momentanesItem = itemController.ItemInHandGeben();

                if (momentanesItem != null && momentanesItem.name == itemPrefab.name) {
                    if (nachricht != null) {
                        nachricht.Anzeigen(NachrichtenArt.FEHLER, "Du hast dieses Item schon!");
                    }
                } else {
                    itemController.Tragen(itemPrefab);
                    Destroy(gameObject);
                }
            }
        }
    }
}
