using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Endlosmodus : MonoBehaviour {

	public GameObject grass;
	public GameObject ground;
	public GameObject Player;	
	public GameObject redBrick;
	public GameObject brick;

	int minPlatSize=2;
	int maxPlatSize=6;
	int maxHazardSize=5;
	int maxUp=6;
	int maxDown=-4;
	[Range(0,1)]
	float hazardChance=.5f;
	int lastX;
	List<GameObject> spawnedTiles;
	bool isHazard;
	int spawnedBlocks=0;
	int heightAlt;
	int height;
	int blockArt = 1;
	int counter = 25;

	void Start () {
		spawnedTiles = new List<GameObject>();
		for (int i = 1; i <= 20; i = i + 2) {
			lastX = i - 10;
			GameObject tile = Instantiate (grass, new Vector3 (i - 10, 0, 0), new Quaternion ());
			spawnedTiles.Add (tile);
			for (int x = 1; x < 7; x++) {
				GameObject uTile = Instantiate (ground, new Vector3 (i - 10,0 - 2 * x, 0), new Quaternion ());
				spawnedBlocks++;
				spawnedTiles.Add (uTile);
			}
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
		}
		spawnedBlocks = 0;
			if(counter <= 0)
			{
			blockArt = Mathf.RoundToInt(Random.Range(0,2));
					counter = 25;
			print (blockArt);
			}
	}

	void Spawn() {
		counter -= 1;
		if (hazardChance > Random.value && !isHazard) {
			int size = Random.Range(2, maxHazardSize);
			for (int i = 1; i <= size; i++) {
				lastX += 2;
				isHazard = true;
			}
		} else {
			int size = Random.Range (minPlatSize, maxPlatSize);
				 height = Random.Range (maxUp, maxDown);
			if (Mathf.Abs (heightAlt - height) >= 6) {
				Spawn ();
			} else {
				heightAlt = height;
				isHazard = false;
				for (int i = 2; i <= size * 2; i = i + 2) {
					switch(blockArt)
					{
					case 0:
						SpawnLine (redBrick, false, ground);
						break;
					case 1:
						SpawnLine (grass, true, ground);
						break;
					case 2:
						SpawnLine (brick, false, ground);
						break;
					default:
						print ("Fehler");
						break;
					}
				}
			}
		}
	}

	void Delete()	{
		int f = spawnedBlocks;
		print (f);
		for (int i = 1; i <= f; i++) {
			Destroy (spawnedTiles [0]);
			spawnedTiles.RemoveAt (0);
		}
	}

	void SpawnLine(GameObject block, bool ground, GameObject groundBlock)
	{
			GameObject tile = Instantiate (block, new Vector3 (lastX + 2, height, 0), new Quaternion ());
			spawnedBlocks++;
			spawnedTiles.Add (tile);
			if(ground){
			for (int x = 1; x < 7; x++) {
				GameObject uTile = Instantiate (groundBlock, new Vector3 (lastX + 2, height - 2 * x, 0), new Quaternion ());
				spawnedBlocks++;
				spawnedTiles.Add (uTile);
				}}
			lastX += 2;
		}
	}
