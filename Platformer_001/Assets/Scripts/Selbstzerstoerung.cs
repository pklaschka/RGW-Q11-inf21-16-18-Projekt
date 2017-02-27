using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selbstzerstoerung : MonoBehaviour {
    public float verzoegerung = 0.0f;
    private float vergangeneZeit = 0.0f;
	
	void Update() {
        vergangeneZeit += Time.deltaTime;

        if (vergangeneZeit >= verzoegerung) {
            Destroy(gameObject);
        }
	}
}
