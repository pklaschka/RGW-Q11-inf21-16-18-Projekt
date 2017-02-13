using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDHPAnzeige : MonoBehaviour {
	[Range(0, 100)]
    public int angezeigteHP = 100;

    public Text hpText;
    public Image herzInnen;
	
    public void HPSetzen(int hp) {
        angezeigteHP = hp;
        hpText.text = angezeigteHP + "%";
        herzInnen.color = new Color(Math.Min(100.0f, angezeigteHP / 100.0f), 0.0f, 0.0f);
    }
}
