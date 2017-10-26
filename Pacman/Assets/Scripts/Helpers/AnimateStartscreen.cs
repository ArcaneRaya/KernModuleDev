using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateStartscreen : MonoBehaviour {

	private SpriteRenderer rend;

	// Use this for initialization
	void Start () {
		rend = GetComponent <SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		float colorVal = Mathf.PingPong (Time.time / 3.5f, .3f) + .7f;
		rend.color = new Color (colorVal, colorVal, colorVal);
	}
}
