using UnityEngine;

public abstract class EnemyDirectionStrategy {

	public abstract Vector2 DecideDirection (Vector2 currentVector, Vector2 direction, Vector2 targetVector);
}
