
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
