using System;
using System.Collections;
using UnityEngine;

public static class Helfer {
    public static IEnumerator AusfuehrenNach(float sekunden, Action f) {
        yield return new WaitForSeconds(sekunden);
        f();
    }

    public static Vector2 MittelpunktFinden(params Vector2[] punkte) {
        if (punkte.Length <= 0) return Vector2.zero;

        float tx = 0, ty = 0;

        foreach (var p in punkte) {
            tx += p.x;
            ty += p.y;
        }

        return new Vector2(
            tx / punkte.Length,
            ty / punkte.Length
        );
    }
}