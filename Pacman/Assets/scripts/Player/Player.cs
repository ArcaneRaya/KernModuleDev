using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	
	public static PlayerAction Shoot;
	public static Player instance;


	public Vector2 CurrentVector {
		get {
			return currentVector;
		}
	}

	public delegate void PlayerAction ();
	public Vector2 direction;
	private Vector2 currentVector;
	private Vector2 targetVector;
	private float speed = 6f;

	// Use this for initialization
	void Start () {
		if (instance != null) {
			Destroy (instance.gameObject);
		}
		InputManager.Player1.Reset ();
		instance = this;
		currentVector = (Vector2)transform.position;
		targetVector = currentVector;
		Grid.GetCell (targetVector).temporaryOccupants.Add (this);
		Grid.UpdateGrid ((int)currentVector.x, (int)currentVector.y, 8);
	}
	
	// Update is called once per frame
	void Update () {
		InputManager.Player1.Update ();
		Move ();
		if (InputManager.Player1.Shooting){
			if (Shoot != null){
				Shoot ();
			}
		}
	}

	private void Move () {
		Vector2 desiredDirection = InputManager.Player1.Direction ();
		if (currentVector != targetVector && Vector2.Distance ((Vector2)transform.position, targetVector) > (Time.deltaTime * speed)) {
			transform.position += (Vector3)direction * Time.deltaTime * speed;
		}


//		currentVector = targetVector;
//		PickupObjects ();


		else {
			// calculate next position based on last movement amount
			// calculate amount of movement necesary to complete target trajectory
			float distanceTravelled = Vector2.Distance ((Vector2)transform.position, targetVector);
			if (currentVector != targetVector) {
				transform.position = (Vector3)targetVector + new Vector3 (0, 0, -1);
				Grid.GetCell (currentVector).temporaryOccupants.Remove (this);
				currentVector = targetVector;
				Grid.GetCell (currentVector).temporaryOccupants.Add (this);
				Grid.UpdateGrid ((int)currentVector.x, (int)currentVector.y, 8);
				PickupObjects ();
			}

			// substract this amount from the movement to our new target
			if (desiredDirection != Vector2.zero) {
				Directions allowedDirections = Grid.ProvideAllowedDirections (currentVector);
				if (allowedDirections == desiredDirection) {
					targetVector = currentVector + desiredDirection;
					direction = desiredDirection;
					transform.position += (Vector3)direction * Time.deltaTime * (speed-distanceTravelled);
				}
			}
		}

		bool shouldDie = false;
		foreach (MonoBehaviour mb in Grid.GetCell (currentVector).temporaryOccupants) {
			if (mb is EnemyBase) {
				shouldDie = true;
				break;
			}
		}
		if (shouldDie){
			DestroyPlayer ();
		}
	}

	private void PickupObjects () {
		if (Grid.GetCell (currentVector).objectRef != null) {
			foreach (GameObject obj in Grid.GetCell (currentVector).objectRef) {
				if (obj != null) {
					if (obj.GetComponent<IPickupable> () != null) {
						obj.GetComponent<IPickupable> ().OnPickup ();
					}
				}
			}
			Grid.GetCell (currentVector).objectRef = new List<GameObject> ();
			Grid.GetCell (currentVector).occupants = new List<OccupantType> ();
		}
		if (ObjectPool.IsEntirelyInactive (OccupantType.pelletBase)) {
			DestroyPlayer ();
		}
	}

	private void DestroyPlayer () {
		EnemyManager.DisableEnemies ();
		Inventory.DropWeapons ();
		Grid.Flush ();
		InputManager.Player1.Reset ();
		GameManager.SetScene (2);
	}
}
