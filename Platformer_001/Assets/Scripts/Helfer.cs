using System;
using System.Collections;
using UnityEngine;

public static class Helfer {
    public static IEnumerator AusfuehrenNach(float sekunden, Action f) {
        yield return new WaitForSeconds(sekunden);
        f();
    }
}