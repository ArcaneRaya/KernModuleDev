using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory {

	public delegate void InventoryMessage (Inventory inventory);
	public InventoryMessage Dequip;

	private static Inventory Instance {
		get {
			if (instance == null) {
				instance = new Inventory ();
				InputManager.Player1.OnChangeWeaponPicked += instance.ChangeWeapon;
			}
			return instance;
		}
	}
	private static Inventory instance;

	private Dictionary<PelletType, int> pellets = new Dictionary<PelletType, int> {
		{PelletType.blue, 0},
		{PelletType.red, 0},
		{PelletType.yellow, 0}
	};

	private Dictionary <PelletType, WeaponBase> weaponsEquiped = new Dictionary<PelletType, WeaponBase> {
		{PelletType.blue, null},
		{PelletType.red, null},
		{PelletType.yellow, null}
	};

	public static void AddPellet (PelletType pelletType) {
		HUDControl.UpdatePelletDisplay (pelletType, Instance.pellets [pelletType] += 1);
	}

	public static bool RemovePellets (PelletType pelletType, int amount) {
		if (Instance.pellets[pelletType] >= amount) {
			HUDControl.UpdatePelletDisplay (pelletType, Instance.pellets [pelletType] -= amount);
			return true;
		} else {
			return false;
		}
	}

	public static void AddWeapon (WeaponBase weapon) {
		Instance.weaponsEquiped [weapon.WeaponPelletType] = weapon;
		HUDControl.UpdateWeaponDisplay (weapon.WeaponPelletType);
		if (Instance.Dequip == null){
			Instance.weaponsEquiped [weapon.WeaponPelletType].Equip (Instance);
		}
	}

	public static void DropWeapons () {
		if (Instance.weaponsEquiped[PelletType.yellow] != null) {
			Instance.weaponsEquiped [PelletType.yellow].gameObject.transform.SetParent (null);
			Grid.GetCell (Player.instance.CurrentVector).objectRef.Add (Instance.weaponsEquiped [PelletType.yellow].gameObject);
			MonoBehaviour.DontDestroyOnLoad (Instance.weaponsEquiped [PelletType.yellow].gameObject);
		}
		if (Instance.weaponsEquiped[PelletType.blue] != null) {
			Instance.weaponsEquiped [PelletType.blue].gameObject.transform.SetParent (null);
			Grid.GetCell (Player.instance.CurrentVector).objectRef.Add (Instance.weaponsEquiped [PelletType.blue].gameObject);
			MonoBehaviour.DontDestroyOnLoad (Instance.weaponsEquiped [PelletType.blue].gameObject);
		}
		if (Instance.weaponsEquiped[PelletType.red] != null) {
			Instance.weaponsEquiped [PelletType.red].gameObject.transform.SetParent (null);
			Grid.GetCell (Player.instance.CurrentVector).objectRef.Add (Instance.weaponsEquiped [PelletType.red].gameObject);
			MonoBehaviour.DontDestroyOnLoad (Instance.weaponsEquiped [PelletType.red].gameObject);
		}
	}

	private void ChangeWeapon () {
		if (Dequip != null) {
			Dequip (this);
		}
		switch (InputManager.Player1.WeaponPicked ()) {
		case 0:
			if (weaponsEquiped[PelletType.yellow] != null) {
				weaponsEquiped [PelletType.yellow].Equip (this);
			}
			break;
		case 1:
			if (weaponsEquiped[PelletType.red] != null) {
				weaponsEquiped [PelletType.red].Equip (this);
			}
			break;
		case 2:
			if (weaponsEquiped[PelletType.blue] != null) {
				weaponsEquiped [PelletType.blue].Equip (this);
			}
			break;
		default:
			break;
		}
	}

	~Inventory () {
		InputManager.Player1.OnChangeWeaponPicked -= instance.ChangeWeapon;
	}
}
