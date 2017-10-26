using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour {

	public enum SpawnableObjects {
		wall, floor, player, ghost, pellet, weapon
	}

	public Texture2D wallsnpelletMap;
	public Texture2D weaponMap;
	public Texture2D enemyMap;
	public GameObject wall;
	public GameObject playerPrefab;
	public GameObject ghostPrefab;
	public GameObject pelletPrefab;
	public GameObject weaponPrefab;
	public GameObject bulletPrefab;
	public Vector2 spawnPoint;

	private Dictionary<Color, OccupantType> pelletColorCodes;
	private Dictionary<Color, OccupantType> weaponColorCodes;
	private Dictionary<Color, EnemyFactory.EnemyTypes> enemyColorCodes;


	void Awake () {

		pelletColorCodes = new Dictionary<Color, OccupantType> {
			{new Color(1,1,0), OccupantType.pelletYellow},
			{new Color(0,0,1), OccupantType.pelletBlue},
			{new Color(1,0,0), OccupantType.pelletRed}
		};

		weaponColorCodes = new Dictionary<Color, OccupantType> {
			{new Color(1,1,0), OccupantType.weaponYellow},
			{new Color(0,0,1), OccupantType.weaponBlue},
			{new Color(1,0,0), OccupantType.weaponRed}
		};

		enemyColorCodes = new Dictionary<Color, EnemyFactory.EnemyTypes> {
			{new Color(1,0,0), EnemyFactory.EnemyTypes.Stalker},
			{new Color(1,1,0), EnemyFactory.EnemyTypes.Herd},
			{new Color(0,0,1), EnemyFactory.EnemyTypes.Loner}
		};

		if (!ObjectPool.ContainsKey (OccupantType.wall)) {
			ObjectPool.AddPool (OccupantType.wall, wall, 20, true);
		}
		if (!ObjectPool.ContainsKey (OccupantType.pelletBase)) {
			ObjectPool.AddPool (OccupantType.pelletBase, pelletPrefab, 20, true);
		}
		if (!ObjectPool.ContainsKey (OccupantType.bullet)) {
			ObjectPool.AddPool (OccupantType.bullet, bulletPrefab, 20, true);
		}

		GameManager.ResetScore ();
		GenerateLevel(Grid.Initialize (this));
	}

	private void GenerateLevel (Grid levelGrid){
		for (int x = 0; x < wallsnpelletMap.width; x++) {
			for (int y = 0; y < wallsnpelletMap.height; y++) {
				GenerateTile (levelGrid, x, y);
			}
		}
	}

	private void GenerateTile (Grid levelGrid, int x, int y){
		Color pixelColor = wallsnpelletMap.GetPixel (x, y);
		Color weaponColor = weaponMap.GetPixel (x, y);
		Color enemyColor = enemyMap.GetPixel (x, y);

		List<OccupantType> occupants = new List<OccupantType> ();

		if (pixelColor == Color.black) {
			occupants.Add (OccupantType.wall);
		} else {
			if (pelletColorCodes.ContainsKey (pixelColor)) {
				occupants.Add (pelletColorCodes [pixelColor]);
			}
			if (weaponColorCodes.ContainsKey (weaponColor)) {
				occupants.Add (weaponColorCodes [weaponColor]);
			}
		}

		levelGrid.AssignCell (new Vector2 (x, y), occupants);

		if (pixelColor == new Color(0,1,0)) { // player
			spawnPoint = new Vector2 (x, y);
			Instantiate (playerPrefab, new Vector3 (x, y, -1), Quaternion.identity);
		}
		if (enemyColorCodes.ContainsKey (enemyColor) && enemyColor != new Color(1,1,0)) {
			EnemyFactory.ProvideEnemy (enemyColorCodes[enemyColor], this).Setup (new Vector2 (x, y));
		}
	}
}
