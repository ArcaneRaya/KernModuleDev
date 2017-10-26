using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	private static GameManager Instance {
		get {
			if (instance == null) {
				instance = new GameObject ("GameManager").AddComponent <GameManager> ();
				DontDestroyOnLoad (instance.gameObject);
			}
			return instance;
		}
	}
	private static GameManager instance;
	private int score;

	public static void ResetScore () {
		Instance.score = 0;
	}

	public static void AddScore (int amount) {
		Instance.score += amount;
	}

	public static int GetScore () {
		return Instance.score;
	}

	public static void SetScene (int scene) {
//		if (SceneManager.GetActiveScene ().buildIndex != 0 && SceneManager.GetActiveScene ().buildIndex != 2 ) {
//		}
		SceneManager.LoadScene (scene);
	}

	public static void Quit () {
		Application.Quit ();
	}
}
