using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyManager : MonoBehaviour {
	private static EnemyManager Instance {
		get {
			if (instance == null){
				instance = new GameObject ().AddComponent <EnemyManager>();
				DontDestroyOnLoad (instance.gameObject);
			}
			return instance;
		}
	}
	private static EnemyManager instance;

	private List<EnemyBase> enemies = new List<EnemyBase>();


	void Update (){
		foreach (EnemyBase enemy in enemies){
			if (enemy.CurrentState != EnemyState_dead.Instance){
				enemy.UpdateEnemy ();
			}
		}
	}


	public delegate void EnemyMessage(EnemyBase enemyInstance);
	public static EnemyMessage OnEnemyDied;
	public static EnemyMessage OnEnemyAttacked;

	public static void Register (EnemyBase enemyInstance){
		Instance.enemies.Add (enemyInstance);
		enemyInstance.IDied += EnemyDied;
	}

	public static void UnRegister (EnemyBase enemyInstance){
		Instance.enemies.Remove (enemyInstance);
		enemyInstance.IDied -= EnemyDied;
		if (Instance.enemies.Count == 0){
			Destroy (Instance.gameObject);
		}
	}

	public static void DisableEnemies () {
		while (Instance.enemies.Count > 0) {
			Instance.enemies [0].DisableEnemy ();
		}
	}

	private static void EnemyDied (EnemyBase enemyInstance) {
		if (OnEnemyDied != null) {
			OnEnemyDied (enemyInstance);
		}
	}
}
