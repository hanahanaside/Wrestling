using UnityEngine;
using System.Collections;

public class HarajukuController : MonoBehaviour
{
	public HarajukuGalGenerator harajukuGalGenerator;
	// Use this for initialization
	void Start ()
	{
		Debug.Log ("Start");
#if !UNITY_EDITOR
		GameObject.Find ("NendAdBanner").GetComponent<NendAdBanner> ().Show ();
		GameObject.Find ("NendAdIcon").GetComponent<NendAdIcon> ().Show ();
#endif
	
	}

	void Update ()
	{
		#if UNITY_ANDROID
		if( Input.GetKey(KeyCode.Escape) ){
			Application.LoadLevel("Main");
		}
		#endif
	}

	void OnFingerHover (FingerHoverEvent e)
	{
		// check the hover event phase to check if we're entering or exiting the object
		if (e.Phase == FingerHoverPhase.Enter) {
			touchedObject (e);
		}
	}

	void OnApplicationPause (bool pauseStatus)
	{
		if (pauseStatus) {
			harajukuGalGenerator.Pause ();
		} else {
			harajukuGalGenerator.init ();
		}
	}
	
	private void touchedObject (FingerHoverEvent e)
	{
		if (e.Selection.tag == "Gal") {
			Debug.Log ("Gal");
			e.Selection.SendMessage ("StartExitAnimation");
		}
	}
	
	public void onButtonClick ()
	{
		string buttonName = UIButton.current.name;
		if (buttonName == "BackButton") {
			OnBackButtonClick ();
		}
		if (buttonName == "CPIButton") {
#if UNITY_IPHONE
			if(ReleaseChecker.CheckOnSale()){
				GameFeatManager.instance.loadGF();
			}else {
				string title = "\u6e96\u5099\u4e2d\u3067\u3059";
				string message = "\u8fd1\u65e5\u516c\u958b!!";
				string[] buttons = {"OK"};
				EtceteraBinding.showAlertWithTitleMessageAndButtons(title,message,buttons);
			}
#endif
#if UNITY_ANDROID
			GameFeatManager.instance.loadGF();
#endif
		}
		
	}
	
	private void OnBackButtonClick ()
	{
		#if !UNITY_EDITOR
		GameObject.Find ("NendAdIcon").GetComponent<NendAdIcon> ().Hide ();
		#endif

		harajukuGalGenerator.Pause();

		Application.LoadLevel ("Main");
	}

}
