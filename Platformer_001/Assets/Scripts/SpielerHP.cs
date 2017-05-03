using UnityEngine;

public class SpielerHP : HPController {
	public override void OnSterben() {
		base.OnSterben();

		var leben = gameObject.GetComponent<LebenController> ();
		if (leben != null) {
			leben.Sterben();
		}

		var levelGen = FindObjectOfType<LevelGenerator>();
		if (levelGen != null) {
			levelGen.NeuGenerieren();
			Vector3 newPos = new Vector3 (levelGen.spawnPoint.x, levelGen.spawnPoint.y, 0.0f);
			transform.position = newPos;
			hp = 100;
		}
	}
}
