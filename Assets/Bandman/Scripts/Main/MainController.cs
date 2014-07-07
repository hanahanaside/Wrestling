﻿using UnityEngine;
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
			GameFeatManager.instance.loadGF();
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
				GameObject.Find ("StatusBoard").SendMessage ("updateEXPPoint", tag);
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
			GameObject.Find ("Player").SendMessage ("PlayNegativeVoice"); 
		}
		
	}
	
	private void GalTapped (TapGesture gesture)
	{
		Debug.Log ("gal tapped");
		playerController.Atack(gesture.Selection.transform);
	//	GameObject.Find ("Player").SendMessage ("PlayPositiveVoice");
	}

}