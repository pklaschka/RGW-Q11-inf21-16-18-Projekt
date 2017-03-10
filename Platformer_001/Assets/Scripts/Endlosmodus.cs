﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Endlosmodus : MonoBehaviour {

	public GameObject grass;
	public GameObject Player;

	int minPlatSize=3;
	int maxPlatSize=7;
	int maxHazardSize=4;
	int maxUp=4;
	int maxDown=-6;
	[Range(0,1)]
	float hazardChance=.3f;
	int lastX;
	List<GameObject> spawnedTiles;
	bool isHazard;

	void Start () {
		spawnedTiles = new List<GameObject>();
		for (int i = 1; i <= 20; i = i + 2) {
			lastX = i - 10;
			GameObject tile = Instantiate (grass, new Vector3 (i - 10, 0, 0), new Quaternion ());
			spawnedTiles.Add (tile);
		}
		for (int a = 0; a <= 20; a++) {
			Spawn ();
		}
	}

	void Update () {
		if (lastX - Player.transform.position.x <= 30) {
			for (int i = 0; i <= 7; i++)
			Spawn ();
			Delete ();
			hazardChance += .01f;
			print (hazardChance);
		}
	}

	void Spawn() {
		if (hazardChance > Random.value && isHazard == false) {
			int size = Random.Range(2, maxHazardSize);
			for (int i = 1; i <= size; i++) {
				lastX += 2;
				isHazard = true;
			}
		} else {
			int size = Random.Range (minPlatSize, maxPlatSize);
			int height = Random.Range (maxUp, maxDown);
			isHazard = false;
			for (int i = 2; i <= size * 2; i = i + 2) {
				GameObject tile = Instantiate (grass, new Vector3 (lastX + 2, height, 0), new Quaternion ());
				lastX += 2;
				spawnedTiles.Add (tile);
			}
		}
	}

	void Delete()	{
		int f = Mathf.RoundToInt(Player.transform.position.x - spawnedTiles [0].transform.position.x - 100f);
		for (int i = 1; i <= f; i++) {
			Destroy (spawnedTiles [0]);
			spawnedTiles.RemoveAt (0);
		}
	}
}
