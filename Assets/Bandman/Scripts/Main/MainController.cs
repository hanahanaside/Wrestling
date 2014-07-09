using UnityEngine;
using System.Collections;

public class MainController : MonoBehaviour
{
	public GameObject chekiDialogPrefab;
	public GameObject settingDialogPrefab;
	public GameObject commercialDialogPrefab;
	public GameObject uiRoot;
	public GameObject uiFence;
	public PlayerController playerController;
	public bool isDialogShowing{get;set;}

	void Awake ()
	{
		GameObject.Find ("NendAdBanner").GetComponent<NendAdBanner> ().Show ();
		uiFence.SetActive(false);
	}
	
	public void OnButtonClick ()
	{
		string buttonName = UIButton.current.name;
		Debug.Log (buttonName);
		//AppC
		if (buttonName == "GameFeatButton") {
			#if UNITY_IPHONE
			if(ReleaseChecker.CheckOnSale()){
				APUnityPlugin.ShowAppliPromotionWall();
			}else {
				string title = "\u6e96\u5099\u4e2d\u3067\u3059";
				string message = "\u5b8c\u6210\u307e\u3067\u3061\u3087\u3063\u3068\u5f85\u3063\u3066\u304f\u308c\u3088\u306a";
				string[] buttons = {"OK"};
				EtceteraBinding.showAlertWithTitleMessageAndButtons(title,message,buttons);
			}
#endif

#if UNITY_ANDROID
			GameFeatManager.instance.loadGF();
#endif
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
			string title = "準備中です";
			string message = "完成まで待ってくれよな ";
			EtceteraAndroid.showAlert(title,message,"OK");
#endif
		}
		//SettingDialog
		if (buttonName == "SettingButton") {
			GameObject settingDialog = Instantiate (settingDialogPrefab) as GameObject;
			showDialog (settingDialog);
		}
		
		
	}
	
	public void showDialog (GameObject dialog)
	{
		uiFence.SetActive(true);
		dialog.transform.parent = uiRoot.transform;
		dialog.transform.localPosition = new Vector3 (0, 0, 0);
		dialog.transform.localScale = new Vector3 (1, 1, 1);
	}

	void OnTap (TapGesture gesture)
	{
		if(uiFence.activeSelf){
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

	private void PlayerTapped ()
	{
		Debug.Log ("player tapped"); 
		GameObject[] galArray = GameObject.FindGameObjectsWithTag ("Gal");
		GameObject[] kyabaArray = GameObject.FindGameObjectsWithTag ("Kyaba");
		if (galArray.Length < 1 && kyabaArray.Length < 1) {
			playerController.PlayVoce();
		}
		
	}
	
	private void GalTapped (TapGesture gesture)
	{
		Debug.Log ("gal tapped");
		playerController.Atack(gesture.Selection.transform);
	}

}
