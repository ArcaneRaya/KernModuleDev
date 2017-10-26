using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartscreenControl : MonoBehaviour {

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
			SceneManager.LoadScene (1);
		}
	}
}
