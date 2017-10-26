using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Startscreen : MonoBehaviour {

	InputManager inputManager;

	// Use this for initialization
	void Start () {
		inputManager = InputManager.Player1;
		AudioManager.PlayMusic ();
	}

	// Update is called once per frame
	void Update () {
		inputManager.Update ();
		if (inputManager.Direction () != Vector2.zero || inputManager.Shooting) {
			GameManager.SetScene (1);
		}
	}
}

