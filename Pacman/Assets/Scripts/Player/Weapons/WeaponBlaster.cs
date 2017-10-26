using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBlaster : WeaponBase {
	public override PelletType WeaponPelletType {
		get {
			return pelletType;
		}
	}

	public override void OnPickup () {
		base.OnPickup ();
		timeBetweenBullets = 0.6f;
		requiredPelletsPerShot = 20;
	}

	private PelletType pelletType = PelletType.yellow;
	
}
