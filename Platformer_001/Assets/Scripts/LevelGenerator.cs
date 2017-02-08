using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ColorToPrefab {
	public Color32 color;
	public GameObject prefab;
}

public class LevelGenerator : MonoBehaviour {

	public Texture2D levelMap;

	public ColorToPrefab[] colorToPrefab;

	public int size;

	// Use this for initialization
	void Start () {
		LoadMap();
	}

	void EmptyMap() {
		while(transform.childCount > 0) {
			Transform c = transform.GetChild(0);
			c.SetParent(null);
			Destroy(c.gameObject);
		}
	}

	void LoadMap(int levelIndex = 0) {
		EmptyMap();

		Color32[] allPixels = levelMap.GetPixels32();

		int width = levelMap.width;
		int height = levelMap.height;

		for (int x = 0; x < width; x++) {
			for (int y = 0; y < height; y++) {
				print("Trying to spawn tile at: " + x + " | " + y);
				SpawnTileAt(allPixels[x + (y * width)], x*size, y*size);
			}
		}
	}

	void SpawnTileAt (Color32 c, int x, int y) {
		if (c.a < 255) {
			return;
		}

		foreach (ColorToPrefab ctp in colorToPrefab) {
			print("Found color: " + c);
			if (c.r == ctp.color.r && c.g == ctp.color.g && c.b == ctp.color.b) {
				print("Instantiating Object: " + ctp.prefab);
				GameObject go = (GameObject)Instantiate(ctp.prefab, new Vector2(x, y), Quaternion.identity);
				go.transform.SetParent(transform);

				return;
			}
		}
	}
}
