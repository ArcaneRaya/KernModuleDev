using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPiercer : WeaponBase {
	public override PelletType WeaponPelletType {
		get {
			return pelletType;
		}
	}

	public override void OnPickup () {
		base.OnPickup ();
		timeBetweenBullets = 0.2f;
		requiredPelletsPerShot = 5;
	}

	private PelletType pelletType = PelletType.red;
}
