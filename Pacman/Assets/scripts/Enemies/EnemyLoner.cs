using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLoner : EnemyBase {

	public override void Setup (Vector2 spawnPoint) {
		base.Setup (spawnPoint);
		strategy = EnemyDirectionStrategy_classic.Instance;
		ChangeState (EnemyState_patrolling.Instance);
		health = 10;
		visionReach = 6;
		maxSpawnpointdistance = 7;
		weakness = PelletType.blue;
	}

	public override void OnHit (PelletType pelletType){
		Debug.Log ("hit");
		if (pelletType == weakness) {
			if (UpdateHealth (-LOW_DAMAGE)){
				OnObjectDestroyed ();
			}
		}
	}
}
