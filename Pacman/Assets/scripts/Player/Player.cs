using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public Vector2 direction;
	private InputManager inputManager;
	private GridVector currentVector;
	private GridVector targetVector;
	private float speed = 7f;

	// Use this for initialization
	void Start () {
		currentVector = LevelLoader.instance.levelGrid.GetCell (1, 1);
		targetVector = currentVector;
		Vector3 goTo = currentVector.objectRef.transform.position;
		transform.position = new Vector3 (goTo.x, goTo.y, transform.position.z);
		inputManager = new InputManagerPC();
	}
	
	// Update is called once per frame
	void Update () {
		inputManager.Update ();
		Move ();
	}

	private void Move () {
		Vector2 desiredDirection = inputManager.Direction ();
		if (currentVector == targetVector) {
			if (desiredDirection != Vector2.zero) {
				Directions allowedDirections = LevelLoader.instance.ProvideAllowedDirections (currentVector);
				if (allowedDirections == desiredDirection) {
					targetVector = LevelLoader.GetCel ((int)(currentVector.x + desiredDirection.x), (int)(currentVector.y + desiredDirection.y));
					direction = desiredDirection;
				}
			}
		}

		if (Vector3.Distance (transform.position, targetVector.objectRef.transform.position) < (Time.deltaTime * speed)) {
			transform.position = targetVector.objectRef.transform.position;
			currentVector = targetVector;
		} else {
			transform.position += (Vector3)direction * Time.deltaTime * speed;
		}
	}
}
