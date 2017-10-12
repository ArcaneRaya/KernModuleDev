using UnityEngine;

public class InputManager {

	protected Vector2 direction = Vector2.zero;
	
	// Update is called once per frame
	public void Update () {
		ProcessInput ();
	}

	public Vector2 Direction (){
		return direction;
	}

	protected virtual void ProcessInput () {
	}
}
