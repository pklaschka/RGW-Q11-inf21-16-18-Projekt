using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartikelSpawner : MonoBehaviour {
	public GameObject systemPrefab;

	public void Spawnen(Transform ort) {
		Instantiate(systemPrefab, new Vector3(ort.position.x, ort.position.y, ort.position.z), Quaternion.identity);
	}
}
