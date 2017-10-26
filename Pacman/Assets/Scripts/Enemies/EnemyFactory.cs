using UnityEngine;
using System.Collections.Generic;

public class EnemyFactory {
	public static EnemyBase ProvideEnemy (EnemyTypes enemyType, LevelLoader loader){
		foreach (EnemyBase enemy in enemies) {
			if (enemy.CurrentState is EnemyState_dead){
				switch (enemyType){
				case EnemyTypes.Herd:
					if (enemy is EnemyHerd){
						return enemy;
					}
					break;
				case EnemyTypes.Loner:
					if (enemy is EnemyLoner){
						return enemy;
					}
					break;
				case EnemyTypes.Stalker:
					if (enemy is EnemyStalker){
						return enemy;
					}
					break;
				}
			}
		}

		EnemyBase newEnemy;
		GameObject enemyObj = GameObject.Instantiate (loader.ghostPrefab);

		switch (enemyType){
		case EnemyTypes.Herd:
			newEnemy = enemyObj.AddComponent <EnemyHerd> ();
			enemyObj.GetComponent <SpriteRenderer>().color = new Color(1,1,0);
			break;
		case EnemyTypes.Loner:
			newEnemy = enemyObj.AddComponent <EnemyLoner> ();
			enemyObj.GetComponent <SpriteRenderer>().color = new Color(0,0,1);
			break;
		case EnemyTypes.Stalker:
			newEnemy = enemyObj.AddComponent <EnemyStalker> ();
			enemyObj.GetComponent <SpriteRenderer>().color = new Color(1,0,0);
			break;
		default:
			throw new System.ArgumentException ("This enemytype has not been defined");
		}
		enemies.Add (newEnemy);
		return newEnemy;
	}

	private static List<EnemyBase> enemies = new List<EnemyBase>();

	public enum EnemyTypes {
		Herd,
		Loner,
		Stalker
	}
}
