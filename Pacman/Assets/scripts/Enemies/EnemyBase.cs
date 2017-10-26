using UnityEngine;
using System.Collections.Generic;
using System;

public abstract class EnemyBase : MonoBehaviour, IDestroyable, IHitable, IHealth {
	public const int HIGH_DAMAGE = 10;
	public const int MEDIUM_DAMAGE = 5;
	public const int LOW_DAMAGE = 1;

	public EnemyManager.EnemyMessage IDied;

	#region Properties
	public Vector2 Direction {
		get {
			return direction;
		}
		set {
			if (value.x == value.y || Mathf.Abs (value.x) > 1 || Mathf.Abs (value.y) > 1) {
				Debug.Log (value);
				throw new ArgumentException ("Direction does not meet requirements");
			}
			targetVector = CurrentVector + value;
			direction = value;
		}
	}
	public Vector2 CurrentVector {
		get {
			return currentVector;
		}
		set {
			if (value != TargetVector) {
				Debug.LogWarning ("Current Vector is externally set to a value other than the Target Vector");
			}
			currentVector = value;
		}
	}
	public Vector2 TargetVector {
		get {
			return targetVector;
		}
	}
	public Vector2 SpawnpointVector {
		get {
			return spawnpointVector;
		}
	}
	public EnemyState CurrentState {
		get {
			return currentState;
		}
	}
	public EnemyDirectionStrategy Strategy {
		get {
			return strategy;
		}
	}
	public int Health {
		get {
			return health;
		}
	}
	public int VisionReach {
		get {
			return visionReach;
		}
	}
	public int MaxSpawnpointdistance {
		get {
			return maxSpawnpointdistance;
		}
	}
	public float Speed {
		get {
			return speed;
		}
	}
	#endregion

	protected PelletType weakness;
	protected EnemyState currentState;
	protected EnemyDirectionStrategy strategy;
	protected Vector2 spawnpointVector;
	protected Vector2 currentVector;
	protected Vector2 targetVector;
	protected Vector2 direction;
	protected int health = 10;
	protected float speed = 5.5f;
	protected int visionReach = 8;
	protected int maxSpawnpointdistance = 0;


	public virtual void Setup (Vector2 spawnPoint) {
		DontDestroyOnLoad (gameObject);
		spawnpointVector = spawnPoint;
		transform.position = spawnpointVector;
		gameObject.SetActive (true);
		currentVector = spawnpointVector;
		targetVector = spawnpointVector;
		Grid.GetCell (spawnpointVector).temporaryOccupants.Add (this);
		EnemyManager.Register (this);
		EnemyManager.OnEnemyAttacked += OnEnemyAttacked;
		EnemyManager.OnEnemyDied += OnEnemyDied;
	}

	public void UpdateEnemy (){
		currentState.Run(this);
	}

	public void SetNextPosition (Vector3 nextPosition) {
		transform.position = nextPosition;
	}

	public virtual bool UpdateHealth (int amount) {
		return (health += amount) <= 0;
	}

	public virtual void OnHit (PelletType pelletType){
		if (pelletType == weakness) {
			if (UpdateHealth (-HIGH_DAMAGE)) {
				OnObjectDestroyed ();
			}
		} else if (pelletType == weakness.Next ()){
			Debug.Log ("medium damage"); 
			if (UpdateHealth (-MEDIUM_DAMAGE)) {
				OnObjectDestroyed ();
			}
		}
	}

	public virtual void ChangeState (EnemyState newState) {
		if (currentState != null) {
			currentState.Complete (this);
		}
		currentState = newState;
		currentState.Setup (this);
	}

	public virtual void OnObjectDestroyed (){
		EnemyManager.OnEnemyAttacked -= OnEnemyAttacked;
		EnemyManager.OnEnemyDied -= OnEnemyDied;
		ChangeState (EnemyState_dead.Instance);
		if (IDied != null) {
			IDied (this);
		}
		EnemyManager.UnRegister (this);
		Grid.GetCell (currentVector).temporaryOccupants.Remove (this);
		GameManager.AddScore (250);
		gameObject.SetActive (false);
	}

	public virtual void DisableEnemy () {
		Grid.GetCell (currentVector).temporaryOccupants.Remove (this);
		EnemyManager.UnRegister (this);
		ChangeState (EnemyState_dead.Instance);
		gameObject.SetActive (false);
	}

	protected virtual void OnEnemyDied (EnemyBase enemy){
		if (enemy.weakness == weakness){
			Debug.Log ("oh no our brother died"); 
		}
	}

	protected virtual void OnEnemyAttacked (EnemyBase enemy){
		throw new NotImplementedException ();
	}

	protected void ReverseDirection () {
		Vector2 temp = targetVector;
		targetVector = currentVector;
		currentVector = temp;
		direction = -direction;
	}
}
