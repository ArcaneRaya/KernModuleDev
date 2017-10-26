using UnityEngine;
using System.Collections.Generic;

public class GridVector {
	public GridVector () {
		occupants = null;
		objectRef = null;
	}

	public GridVector (List <OccupantType> newoccupants){
		occupants = newoccupants;
		objectRef = new List<GameObject>();
		temporaryOccupants = new List<MonoBehaviour> ();
		if (occupants.Contains (OccupantType.wall) || occupants.Contains (OccupantType.statue)){
			walkable = false;
		} else {
			walkable = true;
		}
		activated = false;
	}

	readonly public bool walkable;
	public bool activated;
	public List<OccupantType> occupants;
	public List<MonoBehaviour> temporaryOccupants;
	public List<GameObject> objectRef;
}