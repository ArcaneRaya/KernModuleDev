using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid {
	public List<List<GridVector>> Cells {
		get {
			return cells;
		}
	}

	private List<List<GridVector>> cells;

	public Grid (int size){
		cells = new List<List<GridVector>>();
		for (int x = 0; x < size; x++) {
			cells.Add (new List<GridVector>());
			for (int y = 0; y < size; y++) {
				cells [x].Add (new GridVector ());
			}
		}
	}

	public GridVector AssignCell (int x, int y, OccupantType occupantType, GameObject objectRef){
		cells [x] [y] = new GridVector (x, y, occupantType, objectRef);
		return cells [x] [y];
	}

	public GridVector GetCell (int x, int y) {
		if (-1 < x && x < cells.Count && -1 < y && y < cells.Count) {
			return cells [x] [y];
		}
		return null;
	}
}