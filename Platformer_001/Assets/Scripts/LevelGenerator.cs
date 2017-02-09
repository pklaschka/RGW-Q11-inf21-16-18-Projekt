using System;
using UnityEngine;

[Serializable]
public class ColourToSprite {
    public Color32 colour;
    public Sprite sprite;
}

public class LevelGenerator : MonoBehaviour {
	public Texture2D levelMap;
    public ColourToSprite[] colourSpriteMap;
    public GameObject tilePrefab;
    public int size;

	void Start() {
		LoadMap();
	}

	void EmptyMap() {
		while (transform.childCount > 0) {
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
				SpawnTileAt(allPixels[x + y * width], x * size, y * size);
			}
		}
	}

	void SpawnTileAt (Color32 c, int x, int y) {
		if (c.a < 255) {
			return;
		}

		foreach (ColourToSprite ctp in colourSpriteMap) {
			if (c.r == ctp.colour.r && c.g == ctp.colour.g && c.b == ctp.colour.b) {
				var tileObject = Instantiate(tilePrefab, new Vector2(x, y), Quaternion.identity, transform);
                var sr = tileObject.GetComponent<SpriteRenderer>();

                if (sr != null) {
                    sr.sprite = ctp.sprite;
                }

				return;
			}
		}
	}
}
