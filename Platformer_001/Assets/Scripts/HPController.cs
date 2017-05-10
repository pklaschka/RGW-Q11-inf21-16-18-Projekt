using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class HPController : MonoBehaviour {
	[Header("Werte")]
	[Range(0, 100)]
	public int hp = 100;
	public int maxHP = 100;

	[Header("Events")]
    public EventTrigger.TriggerEvent schadenEvent;
    public EventTrigger.TriggerEvent todEvent;

    public HUDHPAnzeige hpAnzeige;

    void Start() {
		Anzeigen();
    }

	public virtual void OnSterben() {
		todEvent.Invoke(null);
	}
		
	private void Anzeigen() {
		hpAnzeige.HPSetzen(hp);
	}

	public void SchadenZufuegen(int schaden) {
        hp = Math.Max(hp - schaden, 0);

        if (hp <= 0) {
			OnSterben();
        } else {
            schadenEvent.Invoke(null);
        }

		Anzeigen();
    }

    public void Heilen(int hp) {                        
        this.hp = hp + Math.Min(maxHP, hp);
    }

	public void Umbringen() {
		SchadenZufuegen(hp);
	}
}
