using UnityEngine;

public class EnemyDirectionStrategy_random : EnemyDirectionStrategy {

	public override Vector2 DecideDirection (Vector2 currentVector, Vector2 direction, Vector2 targetVector) {
		Directions allowedDirections = Grid.ProvideAllowedDirections (currentVector);
		if ((int)allowedDirections > 1) {
			Vector2 desiredDirection;
			do {
				desiredDirection = RandomDirection ();
			} while ((desiredDirection == direction * -1) || allowedDirections != desiredDirection);
			return desiredDirection;
		}
		return direction * -1;
	}

	private Vector2 RandomDirection () {
		int value = Mathf.RoundToInt(Random.Range (0, 2)) == 0 ? -1 : 1;
		if ((Random.Range (0, 2)) == 0 ? true : false) {
			return new Vector2 (value, 0);
		} else {
			return new Vector2 (0, value);
		}
	}
}
