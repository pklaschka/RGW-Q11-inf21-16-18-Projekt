using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour {
    public GameObject anfangsItem;
    private GameObject itemInHand;

    void Start() {
        if (anfangsItem != null) {
            Tragen(anfangsItem);
        }
    }

    private Transform ItemStelleFinden(GameObject prefab) {
        string punktName = string.Format("{0}-Punkt", prefab.name);

        foreach (var t in gameObject.GetComponentsInChildren<Transform>()) {
            if (t.gameObject.name == punktName) {
                return t;
            }
        }

        return null;
    }

	public bool Tragen(GameObject prefab) {
        var itemTransform = ItemStelleFinden(prefab);

        if (itemTransform == null) {
            print(string.Format("Item '{0}' kann nicht getragen werden; Es hat keine Anfügungsstelle im Spieler.", prefab.name));
            return false;
        }

        if (itemInHand != null) {
            // Es wird schon ein Item getragen. Dieses muss zerstört werden, bevor wir ein neues tragen können.
            Destroy(itemInHand);
        }

        itemInHand = Instantiate(prefab, itemTransform, false);
        return true;
    }
}
