using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid {

	private static Grid Instance {
		get {
			if (instance == null) {
				throw new System.InvalidOperationException ("A script is trying to acces the Grid before innitialisation");
			}
			return instance;
		}
	}
	private static Grid instance;

	public Dictionary<Vector2, GridVector> Cells {
		get {
			return cells;
		}
	}
	private Dictionary<Vector2, GridVector> cells;
	private LevelLoader levelLoader;


	public static Grid Initialize (LevelLoader loader) {
		instance = new Grid ();
		instance.levelLoader = loader;
		return instance;
	}

	public static bool Flush () {
		foreach (KeyValuePair<Vector2,GridVector> entry in Instance.Cells) {
			Instance.DisableObject (entry.Key);
		}
		instance = null;
		return true;
	}

	public static GridVector GetCell (Vector2 position) {
		if (Instance.cells.ContainsKey (position)) {
			return Instance.Cells [position];
		}
		return null;
	}

	public static Directions ProvideAllowedDirections (Vector2 currentPos){
		if (!Instance.cells.ContainsKey (currentPos)) {
			throw new KeyNotFoundException ();
		}
		Directions allowedDirections = new Directions();
		allowedDirections.left = Instance.cells [currentPos + Vector2.left].walkable;
		allowedDirections.up = Instance.cells [currentPos + Vector2.up].walkable;
		allowedDirections.right = Instance.cells [currentPos + Vector2.right].walkable;
		allowedDirections.down = Instance.cells [currentPos + Vector2.down].walkable;
		return allowedDirections;
	}

	public static void UpdateGrid (int xPos, int yPos, int range) {
		for (int x = -range; x <= range; x++) {
			int yRange = instance.CalcY (x, range);
			if (yRange != 0) {
				for (int y = -yRange; y <= yRange; y++) {
					Vector2 position = new Vector2 (x + xPos, y + yPos);
					instance.EnableObject (position);
				}
			}
		}
		instance.DisableOuterCircle (xPos, yPos, range);
	}

	public static Vector2 ProvideRandomPosition () {
		List<Vector2> positions = new List<Vector2> (Instance.Cells.Keys);
		return positions [Random.Range (0, positions.Count - 1)];
	}

	public GridVector AssignCell (Vector2 position, List<OccupantType> occupants){
		cells.Add (position, new GridVector (occupants));
		return cells[position];
	}

	private void DisableOuterCircle (int xPos, int yPos, int range) {
		for (int x = -range; x <= range; x++) {

			int yRange = CalcY (x, range);

			if (yRange != 0) {
				if (CalcY (x - 1, range) == 0) {
					for (int y = -yRange + 1; y < yRange; y++) {
						DisableObject (new Vector2(x + xPos, y + yPos));
					}
				} 
				if (CalcY (x + 1, range) == 0) {
					for (int y = -yRange + 1; y < yRange; y++) {
						DisableObject (new Vector2(x + xPos, y + yPos));
					}
				} 
				DisableObject (new Vector2(x + xPos, yRange + yPos));
				DisableObject (new Vector2(x + xPos, -yRange + yPos));
			}
		}
	}

	private void EnableObject (Vector2 position) {
		if (Instance.Cells.ContainsKey (position)) {
			if (!Instance.Cells[position].activated) {
				foreach (OccupantType occupant in Instance.Cells[position].occupants) {
					GameObject obj;
					if (occupant == OccupantType.pelletBlue || occupant == OccupantType.pelletRed || occupant == OccupantType.pelletYellow){
						obj = ObjectPool.GetObject (OccupantType.pelletBase);
						PelletType pType;
						switch (occupant) {
						case OccupantType.pelletBlue:
							pType = PelletType.blue;
							break;
						case OccupantType.pelletRed:
							pType = PelletType.red;
							break;
						case OccupantType.pelletYellow:
							pType = PelletType.yellow;
							break;
						default:
							pType = PelletType.yellow;
							break;
						}
						obj.GetComponent <Pellet>().SetPelletType (pType);
					} else if (occupant == OccupantType.weaponBlue || occupant == OccupantType.weaponRed || occupant == OccupantType.weaponYellow) {
						switch (occupant) {
						case OccupantType.weaponYellow:
							obj = WeaponFactory.ProvideWeapon (WeaponFactory.WeaponTypes.Blaster, levelLoader).gameObject;
							break;
						case OccupantType.weaponRed:
							obj = WeaponFactory.ProvideWeapon (WeaponFactory.WeaponTypes.Piercer, levelLoader).gameObject;
							break;
						case OccupantType.weaponBlue:
							obj = WeaponFactory.ProvideWeapon (WeaponFactory.WeaponTypes.Shocker, levelLoader).gameObject;
							break;
						default:
							obj = WeaponFactory.ProvideWeapon (WeaponFactory.WeaponTypes.Blaster, levelLoader).gameObject;
							break;
						}
					} else {
						obj = ObjectPool.GetObject (occupant);
					}
					obj.SetActive (true);
					obj.transform.position = position;
					Instance.Cells[position].objectRef.Add (obj);
				}
				Instance.Cells[position].activated = true;
			}
		}
	}

	private void DisableObject (Vector2 position) {
		if (Instance.Cells.ContainsKey (position)) {
			if (instance.Cells [position].activated) {
				while (instance.Cells[position].objectRef.Count > 0) {
					instance.Cells [position].objectRef [0].SetActive (false);
					instance.Cells [position].objectRef.RemoveAt(0);
				}
				Instance.Cells[position].activated = false;
			}
		}
	}

	private Grid (){
		cells = new Dictionary<Vector2, GridVector>();
	}

	private int CalcY (int x, int range) {
		return Mathf.RoundToInt(
			Mathf.Sqrt (
				- Mathf.Pow ((x), 2) + 
				Mathf.Pow (range, 2))
		);
	}

}
