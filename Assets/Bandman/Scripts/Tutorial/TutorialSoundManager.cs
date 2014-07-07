using UnityEngine;
using System.Collections;

public class TutorialSoundManager : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
		AudioSource audioSource = GameObject.Find ("BGM").audio;
		if (PrefsManager.getInstance ().GetSoundMode () == PrefsManager.SOUND_ON) {
			audioSource.mute = false;
			audioSource.Play ();
		} else {
			audioSource.mute = true;
		}
	}

}
