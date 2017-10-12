using UnityEngine;

public struct GridVector {
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