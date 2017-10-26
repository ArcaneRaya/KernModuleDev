using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndscreenControl : MonoBehaviour {

	public GameObject continueField;
	public Text scoreText;

	private InputManager inputManager;
	private float waitTime = 3;

	// Use this for initialization
	void Start () {
		inputManager = InputManager.Player1;
		inputManager.Reset ();
		continueField.SetActive (false);
		scoreText.text = GameManager.GetScore ().ToString ().PadLeft (4, '0');
	}

	// Update is called once per frame
	void Update () {
		waitTime -= Time.deltaTime;
		if (waitTime < 0) {
			if (!continueField.activeSelf) {
				continueField.SetActive (true);
			}
			inputManager.Update ();
			if (inputManager.Direction () != Vector2.zero || inputManager.Shooting) {
				GameManager.SetScene (1);
			}
		}
	}
}
