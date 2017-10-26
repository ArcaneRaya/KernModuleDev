using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
	private PelletType pelletType;
	private Vector2 direction;
	private Vector2 currentVector;
	private Vector2 targetVector;
	private float speed;

	private void Update () {
		CheckCollision ();

		if (currentVector != targetVector && Vector2.Distance ((Vector2)transform.position, targetVector) > speed * Time.deltaTime) {
			transform.position += (Vector3)direction * speed * Time.deltaTime;
		}
		else {
			float distanceTravelled = Vector2.Distance ((Vector2)transform.position, targetVector);
			if (currentVector != targetVector) {
				transform.position = (Vector3)targetVector + new Vector3 (0, 0, -1);
				currentVector = targetVector;
				CheckCollision ();
			}

			Directions allowedDirections = Grid.ProvideAllowedDirections (currentVector);
			if (allowedDirections == direction) {
				targetVector = currentVector + direction;
				transform.position += (Vector3)direction * Time.deltaTime * (speed-distanceTravelled);
			} else {
				DisableSelf ();
			}
		}


	}

	public void Setup (PelletType pelletType, Vector2 position, Vector2 direction) {
		this.pelletType = pelletType;
		switch (pelletType) {
		case PelletType.blue:
			speed = 10;
			break;
		case PelletType.red:
			speed = 10;
			break;
		case PelletType.yellow:
			speed = 10;
			break;
		}

		this.direction = direction;
		transform.position = position;
		currentVector = position;
		targetVector = position;
		gameObject.SetActive (true);
		Invoke ("DisableSelf", 5); 
	}

	private void CheckCollision () {
		foreach (MonoBehaviour mb in Grid.GetCell (currentVector).temporaryOccupants){
			if (mb != null) {
				if (mb.GetComponent<IHitable> () != null) {
					mb.GetComponent <IHitable> ().OnHit (pelletType);
					DisableSelf ();
					break;
				}
			}
		}
	}

	private void DisableSelf () {
		CancelInvoke ();
		gameObject.SetActive (false);
		// destroyed effect?
	}
}
