using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Pellet : MonoBehaviour, IPickupable {
	private PelletType pelletType;

	public void SetPelletType (PelletType pelletType){
		this.pelletType = pelletType;
		SpriteRenderer rend = GetComponent<SpriteRenderer> ();
		switch (pelletType) {
		case PelletType.yellow:
			rend.color = Color.yellow;
			break;
		case PelletType.red:
			rend.color = Color.red;
			break;
		case PelletType.blue:
			rend.color = Color.blue;
			break;
		}
	}

	public void OnPickup (){
		Inventory.AddPellet (pelletType);
		AudioManager.PlayChomp ();
		GameManager.AddScore (1);
		gameObject.SetActive (false);
	}
}
