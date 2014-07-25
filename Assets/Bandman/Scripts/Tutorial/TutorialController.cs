using UnityEngine;
using System.Collections;

public class TutorialController : MonoBehaviour {
	public GameObject closeButton;
	public UISprite tutorialSprite;
	private int mPageCount = 0;

	void Awake () {
		AdManager.Instance.ShowBannerAd ();
	}

	void Start () {
		closeButton.SetActive (false);
	}

	void OnTap () {
		if (mPageCount < 5) {
			mPageCount++;
			tutorialSprite.spriteName = "tutorial_" + mPageCount;
		}
		if (mPageCount >= 5) {
			closeButton.SetActive (true);
		}
	}

	public void OnCloseClick () {
		PrefsManager.getInstance ().saveTutorialFinished ();
		Application.LoadLevel ("Main");
	}
	
}
