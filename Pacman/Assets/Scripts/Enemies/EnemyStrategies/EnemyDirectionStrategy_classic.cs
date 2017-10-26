using UnityEngine;

public class EnemyDirectionStrategy_classic : EnemyDirectionStrategy {

	public static EnemyDirectionStrategy_classic Instance {
		get {
			if (instance == null) {
				instance = new EnemyDirectionStrategy_classic ();
			}
			return instance;
		}
	}
	private static EnemyDirectionStrategy_classic instance;
	
	public override Vector2 DecideDirection (Vector2 currentVector, Vector2 direction, Vector2 targetVector) {
		Directions allowedDirections = Grid.ProvideAllowedDirections (currentVector);
		if ((int)allowedDirections > 1) {
			Vector2[] desiredDirections = ProvideDirections (targetVector - currentVector);
			foreach (Vector2 desiredDirection in desiredDirections){
				if (allowedDirections == desiredDirection && desiredDirection != direction * -1)
					return desiredDirection;
			}
		}
		return direction * -1;
	}

	// calculate directions from most to least preferred
	private Vector2[] ProvideDirections (Vector2 playDir) {
		if (playDir == Vector2.zero) {
			return new Vector2[] {Vector2.up, Vector2.left, Vector2.down, Vector2.right};
		}
		Vector2[] desiredDirections = new Vector2[4];
		desiredDirections [0] = 
			Mathf.Abs (playDir.x) > Mathf.Abs (playDir.y) ? 
			new Vector2((playDir.x > 0 ? 1 : -1), 0) : 
			new Vector2(0, (playDir.y > 0 ? 1 : -1));

		desiredDirections [1] =
			Mathf.Abs (playDir.x) > Mathf.Abs (playDir.y) ? 
			new Vector2(0, (playDir.y > 0 ? 1 : -1)) :
			new Vector2((playDir.x > 0 ? 1 : -1), 0);

		desiredDirections [2] =
			Mathf.Abs (playDir.x) > Mathf.Abs (playDir.y) ? 
			new Vector2(0, (playDir.y > 0 ? -1 : 1)) :
			new Vector2((playDir.x > 0 ? -1 : 1), 0);

		desiredDirections [3] = 
			Mathf.Abs (playDir.x) > Mathf.Abs (playDir.y) ? 
			new Vector2((playDir.x > 0 ? -1 : 1), 0) : 
			new Vector2(0, (playDir.y > 0 ? -1 : 1));

		return desiredDirections;
	}
}
