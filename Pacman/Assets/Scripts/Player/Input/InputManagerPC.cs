using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManagerPC : InputManager {

	// build in system to avoid release problems with multiple keys

	protected override void ProcessInput () {
		// hardcoded exit
		if (Input.GetKeyDown (KeyCode.Escape)) {
			GameManager.Quit ();
		}

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
		if (Input.GetMouseButtonDown (0)){
			shooting = true;
		}
		if (Input.GetMouseButtonUp (0)) {
			shooting = false;
		}

		if (Input.GetKeyDown(KeyCode.Alpha1)) {
			ChangeWeaponPicked (0);
		}
		if (Input.GetKeyDown(KeyCode.Alpha2)) {
			ChangeWeaponPicked (1);
		}
		if (Input.GetKeyDown(KeyCode.Alpha3)) {
			ChangeWeaponPicked (2);
		}

		shootDirection = mouseDirection ();
	}

	private void ChangeWeaponPicked (int num) {
		weaponPicked = num;
		if (OnChangeWeaponPicked != null) {
			OnChangeWeaponPicked ();
		}
	}

	private Vector2 mouseDirection () {
		Vector2 inputMouse = Input.mousePosition;
		Vector2 centeredMousePosition = new Vector2 (inputMouse.x / Screen.width - 0.5f, inputMouse.y / Screen.height - 0.5f);
		if (centeredMousePosition == Vector2.zero)
			return Vector2.zero;

		if (Mathf.Abs (centeredMousePosition.x) > Mathf.Abs(centeredMousePosition.y)){
			return centeredMousePosition.x > 0 ? Vector2.right : Vector2.left;
		} else {
			return centeredMousePosition.y > 0 ? Vector2.up : Vector2.down;
		}
	}
}
