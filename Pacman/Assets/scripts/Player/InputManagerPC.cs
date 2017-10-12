using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManagerPC : InputManager {

	protected override void ProcessInput () {
		if (Input.GetKeyDown(KeyCode.A)) {
			direction = new Vector2 (-1, 0);
		}
		if (Input.GetKeyDown(KeyCode.W)) {
			direction = new Vector2 (0, 1);
		}
		if (Input.GetKeyDown(KeyCode.D)) {
			direction = new Vector2 (1, 0);
		}
		if (Input.GetKeyDown(KeyCode.S)) {
			direction = new Vector2 (0, -1);
		}
		if (Input.GetKeyUp (KeyCode.A) && direction == new Vector2 (-1, 0)) {
			direction = Vector2.zero;
		}
		if (Input.GetKeyUp (KeyCode.W) && direction == new Vector2 (0, 1)) {
			direction = Vector2.zero;
		}
		if (Input.GetKeyUp (KeyCode.D) && direction == new Vector2 (1, 0)) {
			direction = Vector2.zero;
		}
		if (Input.GetKeyUp (KeyCode.S) && direction == new Vector2 (0, -1)) {
			direction = Vector2.zero;
		}
	}
}
