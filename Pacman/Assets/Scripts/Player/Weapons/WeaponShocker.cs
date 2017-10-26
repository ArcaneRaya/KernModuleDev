using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShocker : WeaponBase {
	public override PelletType WeaponPelletType {
		get {
			return pelletType;
		}
	}

	public override void OnPickup () {
		base.OnPickup ();
		timeBetweenBullets = 0.1f;
		requiredPelletsPerShot = 2;
	}

	private PelletType pelletType = PelletType.blue;
}
