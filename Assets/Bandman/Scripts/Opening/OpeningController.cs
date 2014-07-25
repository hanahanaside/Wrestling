using UnityEngine;
using System.Collections;

public class OpeningController : MonoBehaviour {
	void Start () {
		AdManager.Instance.HideBannerAd ();
	}

	public void OnSkipClick () {

		transition ();

	}

	public void OnAnimationFinished () {
		transition ();
	}

	private void transition () {
		int isTutorialFinished = PrefsManager.getInstance ().isTutorialFinished ();
		if (isTutorialFinished == PrefsManager.TUTORIAL_FINISHED) {
			Application.LoadLevel ("Main");
		} else {
			Application.LoadLevel ("Tutorial");
		}
	}


}
