using UnityEngine;
using System.Collections;

public class ReviewDialog : AlertDialog
{

	public string appStoreUrl;
	public string googlePlayUrl;
	private const string TITLE = "\u30c0\u30a6\u30f3\u30ed\u30fc\u30c9\u3042\u308a\u304c\u3068\u3046\u3054\u3056\u3044\u307e\u3059!!";
	private const string MESSAGE = "\u3053\u306e\u30a2\u30d7\u30ea\u306b\u30ec\u30d3\u30e5\u30fc\u3092\u3057\u3066\u9802\u3051\u308b\u3068\u5e78\u3044\u3067\u3059\u3002\uff08\u826f\u3044\u8a55\u4fa1\u3092\u9802\u3051\u308c\u3070\u3001\u4eca\u5f8c\u3082\u9811\u5f35\u3063\u3066\u30a2\u30d7\u30ea\u51fa\u3057\u3066\u3044\u3051\u307e\u3059!!\uff09";
	private const string POSITIVE_BUTTON = "\u5354\u529b\u3059\u308b";
	private const string NEGATIVE_BUTTON = "\u8868\u793a\u3057\u306a\u3044";
	public static bool reviewFlag{get;set;}

	public override void alertButtonClicked (string text)
	{
		if (text == POSITIVE_BUTTON) {
			if (!PrefsManager.getInstance ().GetReviewed ()) {
				PrefsManager.getInstance ().SetReviewed ();
				Hashtable playerData = PlayerDataDao.getInstance ().getPlayerData ();
				int kyabaSize = (int)playerData [PlayerDataDao.KYABA_SIZE];
				kyabaSize++;
				PlayerDataDao.getInstance ().UpdateKyabaSize (kyabaSize);
				reviewFlag = true;
			}
			#if UNITY_IPHONE
			Application.OpenURL(appStoreUrl);
			#endif

			#if UNITY_ANDROID
			Application.OpenURL(googlePlayUrl);
			#endif
		}

		if(text == NEGATIVE_BUTTON){
			//do not show
			PrefsManager.getInstance ().SetReviewed ();
		}
	}

	public override void Show ()
	{
		#if UNITY_IPHONE
		string[] buttons = new string[] {POSITIVE_BUTTON,"後で",NEGATIVE_BUTTON};
		EtceteraBinding.showAlertWithTitleMessageAndButtons(TITLE, MESSAGE, buttons);
		#endif
		
		#if UNITY_ANDROID
		EtceteraAndroid.showAlert(TITLE,MESSAGE,POSITIVE_BUTTON,NEGATIVE_BUTTON);
		#endif
	}

}
