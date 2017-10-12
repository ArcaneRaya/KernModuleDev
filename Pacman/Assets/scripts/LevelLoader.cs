using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour {

	public static LevelLoader instance;

	public Texture2D map;
	public GameObject wall;
	public GameObject floor;
	public Grid levelGrid;

	// Use this for initialization
	void Awake () {
		instance = this;
		GenerateLevel ();
	}

	public static GridVector GetCel (int x, int y) {
		return instance.levelGrid.GetCell (x, y);
	}

	public Directions ProvideAllowedDirections (GridVector currentPos){
		Directions allowedDirections = new Directions();
		int x = currentPos.x;
		int y = currentPos.y;
		GridVector cell;
		if (-1 < x && x < levelGrid.Cells.Count && -1 < y && y < levelGrid.Cells.Count) {
			if ((cell = levelGrid.GetCell (x - 1, y)) != null) {
				if (cell.occupant == OccupantType.empty) {
					allowedDirections.left = true;
				}
			}
			if ((cell = levelGrid.GetCell (x, y + 1)) != null) {
				if (cell.occupant == OccupantType.empty) {
					allowedDirections.up = true;
				}
			}
			if ((cell = levelGrid.GetCell (x + 1, y)) != null) {
				if (cell.occupant == OccupantType.empty) {
					allowedDirections.right = true;
				}
			}
			if ((cell = levelGrid.GetCell (x, y - 1)) != null) {
				if (cell.occupant == OccupantType.empty) {
					allowedDirections.down = true;
				}
			}
			return allowedDirections;
		}
		throw new KeyNotFoundException ();
	}

	void GenerateLevel (){
		levelGrid = new Grid(map.width);
		int offset = map.width / 2;
		for (int x = 0; x < map.width; x++) {
			for (int y = 0; y < map.height; y++) {
				GenerateTile (x, y, offset);
			}
		}
	}

	void GenerateTile (int x, int y, int offset){
		Color pixelColor = map.GetPixel(x, y);
		if (pixelColor == Color.black) {
			GameObject objectRef = Instantiate (wall, new Vector3 (x - offset, y - offset, 0), Quaternion.identity, gameObject.transform);
			levelGrid.AssignCell (x, y, OccupantType.wall, objectRef);
		} else {
			GameObject objectRef = Instantiate (floor, new Vector3 (x - offset, y - offset, 0), Quaternion.identity, gameObject.transform);
			levelGrid.AssignCell (x, y, OccupantType.empty, objectRef);
		}
	}
}
