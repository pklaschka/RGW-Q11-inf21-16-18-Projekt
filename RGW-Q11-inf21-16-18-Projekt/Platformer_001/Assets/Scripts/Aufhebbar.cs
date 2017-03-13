using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aufhebbar : MonoBehaviour {
    public GameObject itemPrefab;

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            var itemController = other.gameObject.GetComponent<ItemController>();
            if (itemController == null) return;

            itemController.Tragen(itemPrefab);
            Destroy(gameObject);
        }
    }
}
