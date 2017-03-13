using System;
using System.Collections;
using UnityEngine;

public static class Helfer {
    public static IEnumerator AusfuehrenNach(float sekunden, Action f) {
        yield return new WaitForSeconds(sekunden);
        f();
    }

    public static Vector2 MittelpunktFinden(params Vector2[] punkte) {
        // Wenn wir keine Punkte haben, macht es keinen Sinn weiter zu machen.
        if (punkte.Length <= 0) return Vector2.zero;

        // Um den Mittelpunkt mehrerer Punkte zu berechnen, addieren
        // wir erst die Koordinaten aller Punkte zusammen.
        float tx = 0, ty = 0;

        foreach (var p in punkte) {
            tx += p.x;
            ty += p.y;
        }

        // Nun teilen wir die Koordinatensumme durch die Gesamtanzahl
        // der Punkte, wie bei der Durchschnittsberechnung.
        return new Vector2(
            tx / punkte.Length,
            ty / punkte.Length
        );
    }
}