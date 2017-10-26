using UnityEngine;

public class InputManager {

	public delegate void InputEvent ();
	public InputEvent OnChangeWeaponPicked;

	public bool Shooting {
		get{
			return shooting;
		}
	}

	public static InputManager Player1 {
		get {
			if (player1 == null) {
				player1 = new InputManagerPC ();
			}
			return player1;
		}
	}

	private static InputManager player1;

	protected Vector2 direction = Vector2.zero;
	protected Vector2 shootDirection = Vector2.zero;
	protected int weaponPicked = 0;
	protected bool shooting;


	public void Update () {
		ProcessInput ();
	}

	public void Reset () {
		direction = Vector2.zero;
		shootDirection = Vector2.zero;
		weaponPicked = 0;
		shooting = false;
	}

	public Vector2 Direction (){
		return direction;
	}

	public Vector2 ShootDirection (){
		return shootDirection;
	}

	public int WeaponPicked (){
		return weaponPicked;
	}

	protected virtual void ProcessInput () {
	}
}
