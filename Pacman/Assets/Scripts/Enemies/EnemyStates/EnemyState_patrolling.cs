using UnityEngine;

public class EnemyState_patrolling : EnemyState {

	public static EnemyState_patrolling Instance {
		get {
			if (instance == null) {
				instance = new EnemyState_patrolling ();
			}
			return instance;
		}
	}
	private static EnemyState_patrolling instance;


	public override void Setup (EnemyBase enemyInstance){
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

		float distanceToSpawnpoint = Vector2.Distance ((Vector2)enemyInstance.transform.position, enemyInstance.SpawnpointVector);
		if (Vector2.Distance ((Vector2)enemyInstance.transform.position, (Vector2)Player.instance.transform.position) < enemyInstance.VisionReach &&
		    (distanceToSpawnpoint < enemyInstance.MaxSpawnpointdistance - 2 || enemyInstance.MaxSpawnpointdistance == 0)){
			enemyInstance.ChangeState (EnemyState_stalking.Instance);
			return;
		}
		if (distanceToSpawnpoint < 2) {
			enemyInstance.ChangeState (this);
			return;
		}
	}

	public override void Complete (EnemyBase enemyInstance){
	}
}
