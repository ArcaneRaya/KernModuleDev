using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class WeaponBase : MonoBehaviour, IPickupable {
	protected PelletType pellettype;

	public virtual void OnPickup () {
		throw new NotImplementedException ();
	}
	protected virtual void OnShoot () {
		throw new NotImplementedException ();
	}
}
