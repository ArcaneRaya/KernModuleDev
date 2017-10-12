using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public static Player instance;

	public Vector2 direction;
	private InputManager inputManager;
	private GridVector currentVector;
	private GridVector targetVector;
	private float speed = 7f;

	// Use this for initialization
	void Start () {
		if (instance != null) {
			Destroy (instance.gameObject);
		}
		instance = this;
		currentVector = LevelLoader.instance.spawnPoint;
		targetVector = currentVector;
		inputManager = new InputManagerPC();
	}
	
	// Update is called once per frame
	void Update () {
		inputManager.Update ();
		Move ();
	}

	private void Move () {
		Vector2 desiredDirection = inputManager.Direction ();

		if (Vector2.Distance ((Vector2)transform.position, (Vector2)targetVector.objectRef.transform.position) < (Time.deltaTime * speed)) {
			transform.position = targetVector.objectRef.transform.position + new Vector3(0,0,-1);
			currentVector = targetVector;
		} else {
			transform.position += (Vector3)direction * Time.deltaTime * speed;
		}

		if (currentVector == targetVector) {
			if (desiredDirection != Vector2.zero) {
				Directions allowedDirections = LevelLoader.instance.ProvideAllowedDirections (currentVector);
				if (allowedDirections == desiredDirection) {
					targetVector = LevelLoader.GetCel ((int)(currentVector.x + desiredDirection.x), (int)(currentVector.y + desiredDirection.y));
					direction = desiredDirection;
				}
			}
		}
	}
}
