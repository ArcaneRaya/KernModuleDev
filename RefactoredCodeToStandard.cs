namespace DialogueSystem {

	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	public class DS_PlayerObject : MonoBehaviour {

		// public
		public AudioSource Source {
			get {
				return source;
			}
			set {
				if (source == null) {
					source = value;
				}
			}
		}
		public bool Paused {
			get {
				return paused;
			}
		}
		public DS_VoiceLineID voiceLine;

		// private
		private enum fadeDirection {up, down}
		private AudioSource source;
		private bool paused;


		private void Update () {
			if (source != null) {
				if (!source.isPlaying && source.time == 0 && !paused) {
					Destroy (gameObject);
				}
			}
		}


		public void Play () {
			if (source == null) {
				source = gameObject.AddComponent<AudioSource> ();
			}
			source.clip = voiceLine.LoadClip ();
			source.Play ();
		}


		public void PlayScheduled (double delay) {
			if (source == null) {
				source = gameObject.AddComponent<AudioSource> ();
			}
			source.clip = voiceLine.LoadClip ();
			source.PlayScheduled (delay);
		}


		public void Pause (){
			paused = true;
			StartCoroutine(Fade (source, fadeDirection.down, 0.25f, false));
		}


		public void UnPause (){
			paused = false;
			source.Play ();
			StartCoroutine(Fade (source, fadeDirection.up, 0.15f, false));
		}


		public void Stop () {
			StartCoroutine(Fade (source, fadeDirection.down, 0.15f, true));
		}


		private IEnumerator Fade (AudioSource audioSource, fadeDirection direction, float FadeTime, bool kill) {
			float startVolume = audioSource.volume;

			if (direction == fadeDirection.down) {
				while (audioSource.volume > 0) {
					audioSource.volume -= 1 * Time.deltaTime / FadeTime;
					yield return null;
				}
			} 
			else {
				while (audioSource.volume < 1) {
					audioSource.volume += 1 * Time.deltaTime / FadeTime;
					yield return null;
				}
			}

			if (kill) {
				audioSource.Stop ();
				Destroy (gameObject);
			}

			if (direction == fadeDirection.down) {
				audioSource.volume = 0;
				audioSource.Pause ();
			} 
			else {
				audioSource.volume = 1;
			}
		}


	}
}