using UnityEngine;
using System.Collections.Generic;

public class WeaponFactory {
	public static WeaponBase ProvideWeapon (WeaponTypes weaponType, LevelLoader loader){
		foreach (WeaponBase weapon in weapons) {
			if (!weapon.gameObject.activeSelf){
				switch (weaponType){
				case WeaponTypes.Blaster:
					if (weapon is WeaponBlaster){
						return weapon;
					}
					break;
				case WeaponTypes.Piercer:
					if (weapon is WeaponPiercer){
						return weapon;
					}
					break;
				case WeaponTypes.Shocker:
					if (weapon is WeaponShocker){
						return weapon;
					}
					break;
				}
			}
		}

		WeaponBase newWeapon;
		GameObject newObj = GameObject.Instantiate (loader.ghostPrefab);

		switch (weaponType){
		case WeaponTypes.Blaster:
			newWeapon = newObj.AddComponent <WeaponBlaster> ();
			newWeapon.gameObject.GetComponent <SpriteRenderer>().color = new Color(1,1,0);
			break;
		case WeaponTypes.Piercer:
			newWeapon = newObj.AddComponent  <WeaponPiercer>();
			newWeapon.gameObject.GetComponent <SpriteRenderer>().color = new Color(1,0,0);
			break;
		case WeaponTypes.Shocker:
			newWeapon = newObj.AddComponent  <WeaponShocker>();
			newWeapon.gameObject.GetComponent <SpriteRenderer>().color = new Color(0,0,1);
			break;
		default:
			throw new System.ArgumentException ("This weapontype has not been defined");
		}
		weapons.Add (newWeapon);
		return newWeapon;
	}

	private static List<WeaponBase> weapons = new List<WeaponBase>();

	public enum WeaponTypes {
		Blaster,
		Piercer,
		Shocker
	}
}

