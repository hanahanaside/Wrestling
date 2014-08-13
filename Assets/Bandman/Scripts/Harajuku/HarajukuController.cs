using UnityEngine;
using System.Collections;

public class HarajukuController : MonoBehaviour {
	public HarajukuGalGenerator harajukuGalGenerator;
	public GameObject cpiAdButton;


	// Use this for initialization
	void Start () {
		Debug.Log ("Start");
#if UNITY_IPHONE
		if (!OnSaleChecker.CheckOnSale ()) {
			cpiAdButton.SetActive (false);
		}else {
			AdManager.Instance.ShowIconAd ();
		}
#endif

#if UNITY_ANDROID
		AdManager.Instance.ShowIconAd ();
#endif

#if !UNITY_EDITOR
		AdManager.Instance.ShowBannerAd ();
#endif
	
	}

	void Update () {
		#if UNITY_ANDROID
		if( Input.GetKey(KeyCode.Escape) ){
			Application.LoadLevel("Main");
		}
		#endif
	}

	void OnFingerHover (FingerHoverEvent e) {
		// check the hover event phase to check if we're entering or exiting the object
		if (e.Phase == FingerHoverPhase.Enter) {
			touchedObject (e);
		}
	}

	void OnApplicationPause (bool pauseStatus) {
		if (pauseStatus) {
			harajukuGalGenerator.Pause ();
		} else {
			harajukuGalGenerator.init ();
		}
	}
	
	private void touchedObject (FingerHoverEvent e) {
		if (e.Selection.tag == "Gal") {
			Debug.Log ("Gal");
			e.Selection.SendMessage ("StartExitAnimation");
		}
	}
	
	public void onButtonClick () {
		string buttonName = UIButton.current.name;
		if (buttonName == "BackButton") {
			OnBackButtonClick ();
		}
		if (buttonName == "CPIButton") {
			AdManager.Instance.ShowWallAd();
		}
		
	}
	
	private void OnBackButtonClick () {
		#if !UNITY_EDITOR
		AdManager.Instance.HideIconAd ();
		#endif

		harajukuGalGenerator.Pause ();

		Application.LoadLevel ("Main");
	}

}
