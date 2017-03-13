using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class SpielerHP : MonoBehaviour {
	[Header("Werte")]
	[Range(0, 100)]
	public int hp = 100;
	public int maxHP = 100;

	[Header("Events")]
    public EventTrigger.TriggerEvent schadenEvent;
    public EventTrigger.TriggerEvent todEvent;

    public HUDHPAnzeige hpAnzeige;

    void Start() {
        if (hpAnzeige != null) hpAnzeige.HPSetzen(hp);
    }

	private void OnSterben()
	{
		todEvent.Invoke(null);

		var leben = gameObject.GetComponent<LebenController> ();
		if (leben != null) 
		{
			leben.Sterben ();
		}

		var levelGen = FindObjectOfType<LevelGenerator> ();
		levelGen.NeuGenerieren ();
		Vector3 newPos = new Vector3 (levelGen.spawnPoint.x, levelGen.spawnPoint.y, 0.0f);
		transform.position = newPos;
        hp = 100;
	}

	public void SchadenZufuegen(int schaden) {
        hp = Math.Max(hp - schaden, 0);

        if (hp <= 0) {
			OnSterben ();
        } else {
            schadenEvent.Invoke(null);
        }

        if (hpAnzeige != null) hpAnzeige.HPSetzen(hp);
    }

    public void Heilen(int hp) {                        
        this.hp = hp + Math.Min(maxHP, hp);
    }

	public void Umbringen()
	{
		SchadenZufuegen (hp);
	}
}
