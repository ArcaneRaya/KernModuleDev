using UnityEngine;
using System;

public abstract class EnemyBase : MonoBehaviour, IDestroyable, IHitable, IHealth {

	public int Health {
		get {
			throw new NotImplementedException ();
		}
	}
	public PelletType weakness;
	protected EnemyState currentState;

	public void UpdateEnemy (){
		currentState.Run();
	}

	public virtual bool UpdateHealth (int amount) {
		throw new NotImplementedException ();
	}
	public abstract void OnHit ();
	public abstract void OnDestroy ();

	protected abstract void OnEnemyDied (EnemyBase enemy);
	protected abstract void OnEnemyAttacked (EnemyBase enemy);
}
