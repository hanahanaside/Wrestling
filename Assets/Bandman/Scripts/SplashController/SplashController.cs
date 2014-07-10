using UnityEngine;
using System.Collections;

public class SplashController : MonoBehaviour
{

	// Use this for initialization
	IEnumerator Start ()
	{
		yield return new WaitForSeconds(0.5f);
		if (PrefsManager.getInstance ().GetSoundMode () == PrefsManager.SOUND_ON) {
			audio.Play ();
		}
		yield return new WaitForSeconds(1.5f);
		int isTutorialFinished = PrefsManager.getInstance ().isTutorialFinished ();
		if (isTutorialFinished != PrefsManager.TUTORIAL_FINISHED) {
			Application.LoadLevel ("Opening");
		} else {
			Application.LoadLevel ("Main");
		}

	}
	
}
