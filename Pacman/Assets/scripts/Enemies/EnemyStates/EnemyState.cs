using System;

public abstract class EnemyState {
	public virtual void Setup (){
		throw new NotImplementedException ();
	}

	public virtual void Run (){
		throw new NotImplementedException ();
	}

	public virtual void Complete (){
		throw new NotImplementedException ();
	}
}