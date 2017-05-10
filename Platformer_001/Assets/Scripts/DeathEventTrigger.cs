using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathEventTrigger : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D c)
	{
		if (c.CompareTag ("Player")) 
		{
			var Hp = c.gameObject.GetComponent<SpielerHP>();
			Hp.Umbringen ();
		}
	}
}
