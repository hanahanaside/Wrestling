using UnityEngine;
using System.Collections;

public class FinishedEvolutionDialog : AlertDialog
{
	private const string TITLE = "\u306b\u9032\u5316\u3057\u307e\u3057\u305f";
	private const string MESSAGE = "\u4eca\u56de\u306e\u9032\u5316\u3092Twitter\u3067\u30b7\u30a7\u30a2\u9802\u3044\u305f\u65b9\u306b\u3001\u30d0\u30f3\u30ae\u30e3\u30eb50\u4eba\u5206\u306e\u30ad\u30e3\u30d0\u5b22\u30921\u4eba\u30d7\u30ec\u30bc\u30f3\u30c8\uff01";
	private const string POSITIVE_BUTTON = "\u30b7\u30a7\u30a2\u3059\u308b"; // share
	private const string NEGATIVE_BUTTON = "\u30c1\u30a7\u30ad\u3092\u898b\u308b"; //cheki

	public override void alertButtonClicked (string text)
	{
		if (text == POSITIVE_BUTTON) {
			StartCoroutine (GameObject.Find ("ShareManager").GetComponent<ShareManager> ().showTweetComposer ());
		} else {
			GameObject.Find("ChekiButton").SendMessage("OnClick");
		}
		Destroy(gameObject);
	}

	public override void Show ()
	{

	}

	public void Show (string playerName)
	{
		string title = playerName + TITLE;
		#if UNITY_IPHONE
		string[] buttons = new string[] {POSITIVE_BUTTON,NEGATIVE_BUTTON};
		EtceteraBinding.showAlertWithTitleMessageAndButtons(title, MESSAGE, buttons);
		#endif
		
		#if UNITY_ANDROID
		EtceteraAndroid.showAlert(title,MESSAGE,POSITIVE_BUTTON,NEGATIVE_BUTTON);
		#endif
	}

}
