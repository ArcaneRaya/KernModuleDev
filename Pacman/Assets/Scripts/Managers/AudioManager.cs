using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

	private static AudioManager Instance {
		get {
			if (instance == null) {
				instance = new GameObject ("AudioManager").AddComponent <AudioManager> ();
			}
			return instance;
		}
	}
	private static AudioManager instance;

	public AudioClip soundtrack;
	public AudioClip chomp;

	private AudioSource sourceMusic;
	private AudioSource sourceChomp;

	// Use this for initialization
	void Awake () {
		DontDestroyOnLoad (gameObject);
		soundtrack = (AudioClip) Resources.Load ("Audio/PacmanDubstepRemix");
		chomp = (AudioClip) Resources.Load ("Audio/PacmanChomp");
		sourceMusic = gameObject.AddComponent <AudioSource> ();
		sourceChomp = gameObject.AddComponent <AudioSource> ();
	}

	public static void PlayMusic () {
		Instance.sourceMusic.loop = true;
		Instance.sourceMusic.clip = Instance.soundtrack;
		Instance.sourceMusic.Play ();
	}

	public static void PlayChomp () {
		Instance.sourceChomp.loop = false;
		Instance.sourceChomp.PlayOneShot (Instance.chomp);
	}
}
