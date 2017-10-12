using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public Vector2 direction;
	private InputManager inputManager;

	// Use this for initialization
	void Start () {
		Vector3 goTo = LevelLoader.instance.levelGrid.GetCell (1, 1).transform.position;
		transform.position = new Vector3 (goTo.x, goTo.y, transform.position.z);
		inputManager = new InputManagerPC();
	}
	
	// Update is called once per frame
	void Update () {
		inputManager.Update ();
		Move ();
	}

	private void Move () {
		direction = inputManager.Direction ();
	}
}
