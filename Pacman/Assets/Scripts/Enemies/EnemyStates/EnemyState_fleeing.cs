using UnityEngine;

public class EnemyState_fleeing : EnemyState {

	public static EnemyState_fleeing Instance {
		get {
			if (instance == null) {
				instance = new EnemyState_fleeing ();
			}
			return instance;
		}
	}
	private static EnemyState_fleeing instance;


	public override void Setup (EnemyBase enemyInstance){
		Debug.Log ("setup");
	}

	public override void Run (EnemyBase enemyInstance){
		if (Vector2.Distance ((Vector2)enemyInstance.transform.position, enemyInstance.TargetVector) 
		    <= (Time.deltaTime * enemyInstance.Speed)) {
			enemyInstance.SetNextPosition((Vector3)enemyInstance.TargetVector + new Vector3(0, 0, -1));
			Grid.GetCell (enemyInstance.CurrentVector).temporaryOccupants.Remove (enemyInstance);
			Grid.GetCell (enemyInstance.TargetVector).temporaryOccupants.Add (enemyInstance);
			enemyInstance.CurrentVector = enemyInstance.TargetVector;
		} else {
			enemyInstance.SetNextPosition (
				enemyInstance.transform.position + 
				(Vector3)enemyInstance.Direction * 
				Time.deltaTime * 
				enemyInstance.Speed
			);
		}
		if (enemyInstance.CurrentVector == enemyInstance.TargetVector) {
			enemyInstance.Direction = enemyInstance.Strategy.DecideDirection (
				enemyInstance.CurrentVector, 
				enemyInstance.Direction,
				enemyInstance.SpawnpointVector
			);
		}
	}

	public override void Complete (EnemyBase enemyInstance){
		Debug.Log ("complete");
	}
}