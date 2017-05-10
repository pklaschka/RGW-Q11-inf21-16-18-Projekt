using System;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
	using UnityEditor;
#endif

public class LevelGenerator : MonoBehaviour {
    [Serializable]
    public enum LevelObjectType {
        Tile,
        Item
    };

    [Serializable]
    public struct ColourToObject {
        public Color32 colour;
        public Sprite sprite;
        public GameObject prefab;
    };

    [Serializable]
    public struct ObjectDef {
        public Sprite sprite;
        public LevelObjectType type;
        public GameObject prefab;
    };

    [Serializable]
    public struct PrefabDefault {
        public LevelObjectType type;
        public GameObject prefab;
    };
    
    [Header("Essentielle Parameter")]
    public Texture2D levelMap;
	public TextAsset levelConfig;
	public SpriteRenderer backgroundLeft, backgroundRight;

    [Header("Zuordnungen")]
    public ColourToObject[] colourObjectMap;
    public PrefabDefault[] defaultPrefabs;

    [Header("Sonstige Parameter")]
    [Range(1, 20)]
    public int tileSize;

	public Vector2 spawnPoint;

	public static Texture2D letzterLevel;

    private readonly Dictionary<int, ObjectDef> idTileDict = new Dictionary<int, ObjectDef>();
    private readonly Dictionary<int, ObjectDef> idItemDict = new Dictionary<int, ObjectDef>();

    private readonly Dictionary<LevelObjectType, GameObject> prefabDefaultDict = new Dictionary<LevelObjectType, GameObject>();

    private Dictionary<int, LevelObjectType> idToType = new Dictionary<int, LevelObjectType>();

    public LevelGenerator() {
        idToType[0x00] = LevelObjectType.Tile;
        idToType[0xFF] = LevelObjectType.Item;
    }

	void Start() {
        foreach (ColourToObject cts in colourObjectMap) {
            var def = new ObjectDef();
            def.sprite = cts.sprite;
            def.type = idToType[cts.colour.r];
            def.prefab = cts.prefab;

            switch (def.type) {
                case LevelObjectType.Tile:
                    idTileDict[cts.colour.g] = def;
                    break;

                case LevelObjectType.Item:
                    idItemDict[cts.colour.g] = def;
                    break;

                default:
                    break;
            }
            
        }

        foreach (PrefabDefault d in defaultPrefabs) {
            prefabDefaultDict[d.type] = d.prefab;
        }

        colourObjectMap = null;
        defaultPrefabs = null;

		LoadMap();
	}

	void EmptyMap() {
        while (transform.childCount > 0) {
            var c = transform.GetChild(0);
            c.SetParent(null);
            Destroy(c.gameObject);
        }
	}

    private void CommonDecode(Dictionary<int, ObjectDef> dict, int id, Action<ObjectDef, int> f) {
        if (!dict.ContainsKey(id)) return;

        var objDef = dict[id];
        if (objDef.prefab == null) objDef.prefab = prefabDefaultDict[objDef.type];
        f(objDef, id);
    }

    private void DecodeMapInfo(Color32 pixel, int x, int y) {
        switch (pixel.r) {
        case 0x00:
            CommonDecode(idTileDict, pixel.g, (def, id) => SpawnObjectAt(def, id, x, y));
            break;

        case 0xFF:
            CommonDecode(idItemDict, pixel.g, (def, id) => SpawnItemAt(def, id, x, y));
            break;

		case 0x7F:
			spawnPoint.Set(x, y + 1.0f);
			break;
        }
    }

	private void LoadConfig() {
		if (levelConfig != null) {
			var config = JsonUtility.FromJson<LevelConfig> (levelConfig.text);
			print ("Levelname ist: " + config.name);

			if (config.beleuchtung.sonnenlicht) {
				var lightObject = new GameObject ("Sonnenlicht");
				var light = lightObject.AddComponent<Light> ();
				light.type = LightType.Directional;
				light.transform.rotation = Quaternion.LookRotation (config.beleuchtung.sonnenlichtRichtung);
			}

			#if UNITY_EDITOR
				if (config.hintergrund != null) {
					var backgroundTex = AssetDatabase.LoadAssetAtPath(config.hintergrund, typeof(Sprite));

					if (backgroundLeft != null && backgroundRight != null) {
						backgroundLeft.sprite = (Sprite) backgroundTex;
						backgroundRight.sprite = (Sprite) backgroundTex;
					}
				}
			#endif
		}
	}

	private void LoadMap(int levelIndex = 0) {
		if (letzterLevel != null) {
			levelMap = letzterLevel;
		} else {
			letzterLevel = levelMap;
		}

		EmptyMap();
		LoadConfig();

		Color32[] allPixels = levelMap.GetPixels32();

		int width = levelMap.width;
		int height = levelMap.height;

		for (int x = 0; x < width; x++) {
			for (int y = 0; y < height; y++) {
                var c = allPixels[y * width + x];
                if (c.a < 255) continue;
				DecodeMapInfo(c, x * tileSize, y * tileSize);
			}
		}

		var player = FindObjectOfType<PlayerMovementController>();
		if (player != null) {
			player.gameObject.transform.position = new Vector3(spawnPoint.x, spawnPoint.y, 0.0f);
		}
	}

	private GameObject SpawnObjectAt(ObjectDef objDef, int id, int x, int y) {
        if (objDef.prefab == null) return null;
		var obj = Instantiate(objDef.prefab, new Vector2(x, y), Quaternion.identity, transform);

        var sr = obj.GetComponentInChildren<SpriteRenderer>();
        if (sr != null) {
            sr.sprite = objDef.sprite;
        }

        return obj;
	}

    private GameObject SpawnItemAt(ObjectDef objDef, int id, int x, int y) {
        var itemPrefab = objDef.prefab;
        objDef.prefab = prefabDefaultDict[LevelObjectType.Item];

        var obj = SpawnObjectAt(objDef, id, x, y);
        if (obj == null) return null;

        var aufhebbar = obj.GetComponent<Aufhebbar>();
        if (aufhebbar == null) return null;
        aufhebbar.itemPrefab = itemPrefab;

        obj.transform.localScale.Set(15.0f, 15.0f, 0.0f);

        return obj;
    }

	public void NeuGenerieren() {
		EmptyMap();
		LoadMap();
	}
}
