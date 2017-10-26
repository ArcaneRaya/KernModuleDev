using System;

public abstract class EnemyState {
	public virtual void Setup (EnemyBase enemyInstance){
		throw new NotImplementedException ();
	}

	public virtual void Run (EnemyBase enemyInstance){
		throw new NotImplementedException ();
	}

	public virtual void Complete (EnemyBase enemyInstance){
		throw new NotImplementedException ();
	}
}