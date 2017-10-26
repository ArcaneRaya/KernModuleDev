using System.Collections.Generic;
using UnityEngine;
using System;

public static class ObjectPool {
	public static void AddPool (OccupantType occupantType, GameObject prefab, int size, bool resizable) {
		if (!pools.ContainsKey (occupantType)) {
			pools.Add (occupantType, new GameObject ("ObjectPool_" + occupantType.ToString ())
				.AddComponent <GameobjectPool>());
			pools [occupantType].Setup (prefab, size, resizable);
		} else {
			throw new ArgumentException ("A pool already exists for " + occupantType);
		}
	}

	public static GameObject GetObject (OccupantType occupantType) {
		if (!pools.ContainsKey (occupantType)){
			throw new ArgumentException ("There is no pool for objects of type " + occupantType);
		}
		return pools [occupantType].GetObject ();
	}

	public static bool ContainsKey (OccupantType occupantType) {
		return pools.ContainsKey (occupantType);
	}

	public static bool IsEntirelyInactive (OccupantType occupantType) {
		return pools [occupantType].IsEntirelyInactive ();
	}

	private static Dictionary <OccupantType, GameobjectPool> pools = new Dictionary<OccupantType, GameobjectPool>();
}