using UnityEngine;
using System.Collections.Generic;
using System;

public class GameobjectPool : MonoBehaviour {
	private List <GameObject> objectPool;
	private GameObject prefab;
	private bool resizable;
	private bool isSetup;

	public void Setup (GameObject pooledObject, int size, bool resizable) {
		DontDestroyOnLoad (gameObject);
		if (!isSetup) {
			prefab = pooledObject;
			objectPool = new List<GameObject> ();
			this.resizable = resizable;
			for (int i = 0; i < size; i++) {
				objectPool.Add ((GameObject)GameObject.Instantiate (prefab, transform));
				objectPool [i].SetActive (false);
			}
			isSetup = true;
		} else {
			throw new InvalidOperationException ("This objectpool had already been setup");
		}
	}

	public GameObject GetObject () {
		if (!isSetup) {
			throw new InvalidOperationException ("This objectpool has not yet been setup but a script is requesting an object");
		}

		foreach (GameObject obj in objectPool){
			if (!obj.activeSelf){
				return obj;
			}
		}
		if (resizable) {
			objectPool.Add ((GameObject)GameObject.Instantiate (prefab, transform));
			return objectPool [objectPool.Count - 1];
		}
		return null;
	}
}