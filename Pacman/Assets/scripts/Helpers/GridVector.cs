using UnityEngine;

public class GridVector {
	public GridVector () {
		x = -1;
		y = -1;
		occupant = OccupantType.empty;
		objectRef = null;
	}
	public GridVector (int x, int y, OccupantType occupant, GameObject objectRef){
		this.x = x;
		this.y = y;
		this.occupant = occupant;
		this.objectRef = objectRef;
	}
	public int x;
	public int y;
	public OccupantType occupant;
	public GameObject objectRef;
}