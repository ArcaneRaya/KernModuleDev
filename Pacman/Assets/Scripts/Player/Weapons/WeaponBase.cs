using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class WeaponBase : MonoBehaviour, IPickupable {
	public virtual PelletType WeaponPelletType {
		get {
			throw new NotImplementedException ();
		}
	}

	protected float timeBetweenBullets = 1f;
	protected float lastShotTime;
	protected int requiredPelletsPerShot;

	protected void Awake () {
		DontDestroyOnLoad (gameObject);
	}

	public virtual void OnPickup () {
		gameObject.GetComponent <SpriteRenderer> ().enabled = false;
		gameObject.transform.SetParent (Player.instance.gameObject.transform);
		Inventory.AddWeapon (this);
	}

	protected virtual void OnShoot () {
		if (Time.time - lastShotTime > timeBetweenBullets && Inventory.RemovePellets (WeaponPelletType, requiredPelletsPerShot)) {
			lastShotTime = Time.time;
			Vector2 shootDirection = InputManager.Player1.ShootDirection ();
			if (Grid.GetCell (Player.instance.CurrentVector + shootDirection).occupants.Contains (OccupantType.wall)) {
				return;
			}
			ObjectPool.GetObject (OccupantType.bullet).GetComponent<Bullet> ()
				.Setup (WeaponPelletType, Player.instance.CurrentVector + shootDirection, shootDirection);
		}
	}

	public void Dequip (Inventory inventory) {
		inventory.Dequip -= Dequip;
		Player.Shoot -= OnShoot;
		gameObject.GetComponent <SpriteRenderer> ().enabled = false;
	}

	public void Equip (Inventory inventory) {
		inventory.Dequip += Dequip;
		Player.Shoot += OnShoot;
		gameObject.GetComponent <SpriteRenderer> ().enabled = true;
	}
}
