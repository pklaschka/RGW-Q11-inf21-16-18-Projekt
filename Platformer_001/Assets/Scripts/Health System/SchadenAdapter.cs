using System;
using UnityEngine;

public class SchadenAdapter {
	public static bool IstGegner(GameObject obj) {
		if (obj == null) return false;
		return obj.CompareTag("Enemy");
	}
	
	public static void SchadenZufuegen(GameObject obj, int schaden, Action onSchaden) {
		if (obj == null) return;

		var hp = obj.GetComponent<HPController>();
		if (hp != null) {
			hp.SchadenZufuegen(schaden);
			onSchaden();
		}
	}

	public static void SchadenZufuegen(GameObject obj, int schaden) {
		SchadenZufuegen(obj, schaden, () => {});
	}
}