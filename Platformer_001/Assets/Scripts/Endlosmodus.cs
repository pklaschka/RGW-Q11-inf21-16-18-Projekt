using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Endlosmodus : MonoBehaviour {

	public GameObject grass;
	public GameObject ground;
	public GameObject Player;	
	public GameObject redBrick;
	public GameObject brick;
	public GameObject crocodile;

	int minPlatSize=2;
	int maxPlatSize=8;
	int maxHazardSize=5;
	int maxUp=6;
	int maxDown=-4;
	[Range(0,1)]
	float hazardChance=.5f;
	int lastX;
	List<GameObject> spawnedTiles;
	List<GameObject> spawnedTilesGround;
	bool isHazard;
	int spawnedBlocks=0;
	int heightAlt;
	int height;
	int blockArt = 1;
	int counter = 25;

	void Start () {
		spawnedTiles = new List<GameObject>();
		spawnedTilesGround = new List<GameObject> ();
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
			blockArt = Mathf.RoundToInt(Random.Range(-0.5f,2.5f));
					counter = 25;
			}
	}

	void Spawn() {
		counter--;
		if (hazardChance > Random.value && !isHazard) {
			int size = Random.Range(2, maxHazardSize);
			for (int i = 1; i <= size; i++) {
				lastX += 2;
				isHazard = true;
			}
		} else {
			int size = Random.Range (minPlatSize, maxPlatSize);
			height = Mathf.RoundToInt(Random.Range (maxUp, maxDown)/2)*2;
			if (Mathf.Abs (heightAlt - height) >= 6) {
				Spawn ();
			} else {
				heightAlt = height;
				isHazard = false;
				for (int i = 2; i <= size * 2; i = i + 2) {
					switch(blockArt)
					{
					case 0:
						SpawnLine (redBrick, false, redBrick);
						break;
					case 1:
						SpawnLine (grass, true, ground);
						break;
					case 2:
						SpawnLine (brick, false, brick);
						break;
					default:
						print ("Fehler");
						break;
					}
				}
			}
			if (size >= 7 && Random.value > .5f) {
					Instantiate (crocodile, new Vector3 (lastX - 3, height + 3, 0), new Quaternion ());
				}
		} 
	}

	void Delete()	{
		int f = spawnedBlocks;
		for (int i = 1; i <= f; i++) {
			Destroy (spawnedTiles [0]);
			spawnedTiles.RemoveAt (0);
			if (spawnedTiles != null && spawnedTilesGround != null && spawnedTiles [0].transform.position.x == spawnedTilesGround [0].transform.position.x) {
				for(int z=1; z <7;z++){
				Destroy (spawnedTilesGround [0]);
				spawnedTilesGround.RemoveAt (0);
				}
			}
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
				spawnedTilesGround.Add (uTile);
				}}
			lastX += 2;
		}
	}
