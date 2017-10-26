using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStalker : EnemyBase {

	public override void Setup (Vector2 spawnPoint) {
		base.Setup (spawnPoint);
		strategy = EnemyDirectionStrategy_classic.Instance;
		ChangeState (EnemyState_patrolling.Instance);
		health = 20;
		weakness = PelletType.red;
	}

	public override void ChangeState (EnemyState newState) {
		spawnpointVector = Grid.ProvideRandomPosition ();
		base.ChangeState (newState);
	}
}
