using UnityEngine;
using System.Collections;

public class ReviewAppChecker : MonoBehaviour {

	public ReviewDialog reviewDialogPrefab;
	public GameObject kyabaPrefab;
	public GameObject anchor;

	// Use this for initialization
	void Start () {
		if (PrefsManager.getInstance ().GetReviewed ()) {
			return;
		}
		int startCount = PrefsManager.getInstance ().GetStartCount ();
		startCount++;
		PrefsManager.getInstance ().SaveStartCount (startCount);
		if (startCount % 10 == 0 && ! PrefsManager.getInstance ().GetReviewed ()) {
			ReviewDialog reviewDialog = Instantiate(reviewDialogPrefab) as ReviewDialog;
			reviewDialog.Show();
		}
	}
	
	void OnApplicationPause(bool pauseStatus) {
		if(!pauseStatus && ReviewDialog.reviewFlag){
			GameObject kyabaObject = Instantiate (kyabaPrefab) as GameObject;
			kyabaObject.transform.parent = anchor.transform;
			kyabaObject.transform.position = anchor.transform.position;
			string title = "\u30ec\u30d3\u30e5\u30fc\u3042\u308a\u304c\u3068\u3046\u3054\u3056\u3044\u307e\u3059\uff01";
			string message = "\u30ad\u30e3\u30d0\u5b22\u3092\u8ffd\u52a0\u3057\u307e\u3057\u305f!!";
			ReviewDialog.reviewFlag = false;
#if UNITY_IPHONE
			string[] buttons = {"OK"};
			EtceteraBinding.showAlertWithTitleMessageAndButtons(title,message,buttons);
#endif
#if UNITY_ANDROID
			EtceteraAndroid.showAlert(title,message,"OK");
#endif
		}
	}
}
