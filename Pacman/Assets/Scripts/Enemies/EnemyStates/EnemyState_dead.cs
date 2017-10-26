using UnityEngine;

public class EnemyState_dead : EnemyState {

	public static EnemyState_dead Instance {
		get {
			if (instance == null) {
				instance = new EnemyState_dead ();
			}
			return instance;
		}
	}
	private static EnemyState_dead instance;


	public override void Setup (EnemyBase enemyInstance){
	}

	public override void Run (EnemyBase enemyInstance){
	}

	public override void Complete (EnemyBase enemyInstance){
	}
}
