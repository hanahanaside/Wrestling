using UnityEngine;
using System.Collections;

public class InitController : MonoBehaviour {

	public void CreatedDatabase(){
		int isTutorialFinished = PrefsManager.getInstance ().isTutorialFinished ();
		if (isTutorialFinished != PrefsManager.TUTORIAL_FINISHED) {
			Application.LoadLevel ("Opening");
		} else {
			Application.LoadLevel ("Main");
		}
	}
	

}
