using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour {
    public GameObject anfangsItem;
    public GameObject itemInHand;

    private Animator anim;

    // Anzahl an Sekunden nach einem Angriff, in denen kein neuer Angriff
    // ausgeführt werden darf.
    // Dies ist dazu da, dass man nicht einfach den Angriff-Knopf wie verrückt
    // drücken kann.
    public float angriffCooldown = 0.8f;

    private float angriffCooldownTimer = 0;
    private bool darfAngreifen = true;

    void Start() {
        anim = GetComponent<Animator>();

        if (anfangsItem != null) {
            Tragen(anfangsItem);
        }
    }

    void Update() {
        if (!darfAngreifen) {
            angriffCooldownTimer += Time.deltaTime;

            if (angriffCooldownTimer >= angriffCooldown) {
                darfAngreifen = true;
                angriffCooldownTimer = 0.0f;
            }
        }

        if (darfAngreifen && Input.GetButtonDown("Attack")) {
            Angreifen();
        }
    }

    public GameObject ItemInHandGeben() {
        return itemInHand;
    }

    public bool KannGeradeAngreifen() {
        return darfAngreifen;
    }

    /** Findet heraus, wo das Item gespawnt werden muss, damit es in der Hand erscheint. */
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
        itemInHand.name = prefab.name;
        return true;
    }

    public void Angreifen() {
		var pmc = GetComponent<PlayerMovementController>();
		if (pmc != null && !pmc.IstAmBoden()) return;
        if (itemInHand == null) return;

        var waffe = itemInHand.GetComponent<Waffe>();
        if (waffe == null) return;

        darfAngreifen = false;
        angriffCooldownTimer = 0.0f;
        anim.SetTrigger(string.Format("attack-{0}", itemInHand.name));

        StartCoroutine(Helfer.AusfuehrenNach(waffe.angriffszeitverschiebung, () => {
            if (waffe.angriffEvent != null) waffe.angriffEvent.Invoke(null);
        }));
    }
}
