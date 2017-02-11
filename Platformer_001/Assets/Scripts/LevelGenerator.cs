using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {
    [Serializable]
    public struct ColourToSprite {
        public Color32 colour;
        public Sprite sprite;
        public GameObject customPrefab;
    }

    [Serializable]
    public struct TileDef {
        public Sprite sprite;
        public GameObject customPrefab;
    };

    public Texture2D levelMap;
    public ColourToSprite[] colourSpriteMap;
    public GameObject defaultTilePrefab;
    public int tileSize;

    private readonly Dictionary<int, TileDef> colourSpriteDict = new Dictionary<int, TileDef>();

    private static int HashColour24(Color32 c) {
        return c.r | (c.g << 8) | (c.b << 16);
    }

	void Start() {
        foreach (var cts in colourSpriteMap) {
            var def = new TileDef();
            def.sprite = cts.sprite;
            def.customPrefab = cts.customPrefab;
            colourSpriteDict[HashColour24(cts.colour)] = def;
        }

        colourSpriteMap = null;

		LoadMap();
	}

	void EmptyMap() {
        while (transform.childCount > 0) {
            var c = transform.GetChild(0);
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
				SpawnTileAt(allPixels[x + y * width], x * tileSize, y * tileSize);
			}
		}
	}

	void SpawnTileAt(Color32 c, int x, int y) {
		if (c.a < 255) {
			return;
		}

        int rgba = HashColour24(c);

        var tileDef = colourSpriteDict[rgba];
        var tilePrefab = tileDef.customPrefab != null ? tileDef.customPrefab : defaultTilePrefab;
		var tileObject = Instantiate(tilePrefab, new Vector2(x, y), Quaternion.identity, transform);

        var sr = tileObject.GetComponent<SpriteRenderer>();

        if (sr != null) {
            sr.sprite = tileDef.sprite;
        }

		return;
	}
}
