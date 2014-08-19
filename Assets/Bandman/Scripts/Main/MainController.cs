using UnityEngine;
using System.Collections;

public class MainController : MonoBehaviour {
	public GameObject chekiDialogPrefab;
	public GameObject settingDialogPrefab;
	public GameObject commercialDialogPrefab;
	public GameObject uiRoot;
	public GameObject uiFence;
	public GameObject cpiAdButton;
	public PlayerController playerController;

	public bool isDialogShowing{ get; set; }

	void Awake () {
		AdManager.Instance.ShowBannerAd ();
		uiFence.SetActive (false);
		#if UNITY_IPHONE
		if (!OnSaleChecker.CheckOnSale ()) {
			cpiAdButton.SetActive (false);
		}
		#endif
	}
	
	public void OnButtonClick () {
		string buttonName = UIButton.current.name;
		Debug.Log (buttonName);
		//AppC
		if (buttonName == "GameFeatButton") {
			AdManager.Instance.ShowWallAd();
		}
		
		//Harajuku
		if (buttonName == "HarajukuButton") {
			Application.LoadLevel ("Harajuku");
		}
		// chekiDialog
		if (buttonName == "ChekiButton") {				
			GameObject chekiDialog = Instantiate (chekiDialogPrefab)as GameObject;
			showDialog (chekiDialog);
		}
		//comercial
		if (buttonName == "ComercialButton") {
			#if UNITY_IPHONE
			GameObject commercialDialog = Instantiate(commercialDialogPrefab) as GameObject;
			showDialog(commercialDialog);
#endif

#if UNITY_ANDROID
			string title = "\u6e96\u5099\u4e2d\u3067\u3059";
			string message = "\u8fd1\u65e5\u516c\u958b!!";
			EtceteraAndroid.showAlert(title,message,"OK");
#endif
		}
		//SettingDialog
		if (buttonName == "SettingButton") {
			GameObject settingDialog = Instantiate (settingDialogPrefab) as GameObject;
			showDialog (settingDialog);
		}
		
		
	}
	
	public void showDialog (GameObject dialog) {
		uiFence.SetActive (true);
		dialog.transform.parent = uiRoot.transform;
		dialog.transform.localPosition = new Vector3 (0, 0, 0);
		dialog.transform.localScale = new Vector3 (1, 1, 1);
	}

	void OnTap (TapGesture gesture) {
		if (uiFence.activeSelf) {
			return;
		}
		if (gesture.Selection) {
			Debug.Log ("Tapped object: " + gesture.Selection.tag);
			string tag = gesture.Selection.tag;
			if (tag == "Player") {
				PlayerTapped ();
			} else if (tag == "Gal" || tag == "Kyaba") {
				GalTapped (gesture);
			}
		} else {
			Debug.Log ("No object was tapped at " + gesture.Position);
		}
	}

	private void PlayerTapped () {
		Debug.Log ("player tapped"); 
		playerController.PlayVoce ();
/*		GameObject[] galArray = GameObject.FindGameObjectsWithTag ("Gal");
		GameObject[] kyabaArray = GameObject.FindGameObjectsWithTag ("Kyaba");
		if (galArray.Length < 1 && kyabaArray.Length < 1) {
			playerController.PlayVoce();
		}
*/		
	}
	
	private void GalTapped (TapGesture gesture) {
		Debug.Log ("gal tapped");
		playerController.Atack (gesture.Selection.transform);
	}

}
