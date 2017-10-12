using UnityEngine;

public class Patrolling : EnemyState {
	public override void Setup (){
		Debug.Log ("setup");
	}

	public override void Run (){
		Debug.Log ("run");
	}

	public override void Complete (){
		Debug.Log ("complete");
	}
}
