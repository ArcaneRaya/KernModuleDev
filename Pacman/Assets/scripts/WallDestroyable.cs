using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WallDestroyable : MonoBehaviour, IHitable, IDestroyable {
	public void OnHit () {
		throw new NotImplementedException ();
	}
	public void OnDestroy () {
		throw new NotImplementedException ();
	}
}
