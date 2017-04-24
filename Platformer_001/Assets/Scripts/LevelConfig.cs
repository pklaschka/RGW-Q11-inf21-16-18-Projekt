using System;

using UnityEngine;

[Serializable]
public struct LevelLighting {
	public bool sonnenlicht;
	public Vector3 sonnenlichtRichtung;
}

[Serializable]
public struct LevelConfig {
	public string name;
	public LevelLighting beleuchtung;
	public string hintergrund;
}
