using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDControl : MonoBehaviour {

	public Text yellowPelletDisplay;
	public Text redPelletDisplay;
	public Text bluePelletDisplay;
	public GameObject weapon01Display;
	public GameObject weapon02Display;
	public GameObject weapon03Display;
	private static HUDControl instance;

	private void Awake () {
		instance = this;
		weapon01Display.SetActive (false);
		weapon02Display.SetActive (false);
		weapon03Display.SetActive (false);
	}

	public static void UpdatePelletDisplay (PelletType pelletType, int amount) {
		switch (pelletType){
		case PelletType.yellow:
			instance.yellowPelletDisplay.text = amount.ToString ().PadLeft(3,'0');
			break;
		case PelletType.red:
			instance.redPelletDisplay.text = amount.ToString ().PadLeft(3,'0');
			break;
		case PelletType.blue:
			instance.bluePelletDisplay.text = amount.ToString ().PadLeft(3,'0');
			break;
		}
	}

	public static void UpdateWeaponDisplay (PelletType pelletType) {
		switch (pelletType){
		case PelletType.yellow:
			instance.weapon01Display.SetActive (true);
			break;
		case PelletType.red:
			instance.weapon02Display.SetActive (true);
			break;
		case PelletType.blue:
			instance.weapon03Display.SetActive (true);
			break;
		}
	}

}
