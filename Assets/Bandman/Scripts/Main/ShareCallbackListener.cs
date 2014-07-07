using UnityEngine;
using System.Collections;

public class ShareCallbackListener : MonoBehaviour
{

	public GameObject kyabaPrefab;
	public GameObject cameraAnchor;
	public ShareManager shareManager;

	void OnEnable ()
	{
		#if UNITY_IPHONE || UNITY_ANDROID
		TwitterManager.tweetSheetCompletedEvent += tweetSheetCompletedEvent;
		TwitterManager.requestDidFinishEvent += requestDidFinishEvent;
		TwitterManager.requestDidFailEvent += requestDidFailEvent;
		TwitterManager.loginSucceededEvent += loginSucceeded;
		TwitterManager.loginFailedEvent += loginFailed;
#endif


	}

	void OnDisable ()
	{
		#if UNITY_IPHONE || UNITY_ANDROID
		TwitterManager.tweetSheetCompletedEvent -= tweetSheetCompletedEvent;
		TwitterManager.requestDidFinishEvent -= requestDidFinishEvent;
		TwitterManager.requestDidFailEvent -= requestDidFailEvent;
		TwitterManager.loginSucceededEvent -= loginSucceeded;
		TwitterManager.loginFailedEvent -= loginFailed;
		#endif

	}


	void loginSucceeded (string username)
	{
		Debug.Log ("Successfully logged in to Twitter: " + username);
		#if UNITY_ANDROID
		EtceteraAndroid.hideProgressDialog();
		string title = "\u30ed\u30b0\u30a4\u30f3\u6210\u529f!!";
		string message = "\u3082\u30461\u5ea6\u30c4\u30a4\u30fc\u30c8\u3057\u3066\u304f\u3060\u3055\u3044";
		EtceteraAndroid.showAlert(title,message,"OK");
#endif
	}
	
	void loginFailed (string error)
	{
		#if UNITY_ANDROID
		EtceteraAndroid.hideProgressDialog();
		ShowErrorDialog();
		Debug.Log ("Twitter login failed: " + error);
#endif
	}

	void requestDidFailEvent( string error )
	{
		#if UNITY_ANDROID
		EtceteraAndroid.hideProgressDialog();
		ShowErrorDialog();  
		Debug.Log( "requestDidFailEvent: " + error );
#endif
	}

	private void ShowErrorDialog(){
		#if UNITY_ANDROID
		string title = "\u30c4\u30a4\u30fc\u30c8\u5931\u6557";
		string message = "\u518d\u5ea6\u304a\u3053\u306a\u3063\u3066\u304f\u3060\u3055\u3044 ";
		EtceteraAndroid.showAlert(title,message,"OK");
#endif
	}
	
	void requestDidFinishEvent( object result )
	{
		#if UNITY_ANDROID
		EtceteraAndroid.hideProgressDialog();
		#endif
		if( result != null )
		{
			Debug.Log( "requestDidFinishEvent" );
			Prime31.Utils.logObject( result );
			ShowCompleteDialog();
		}
	}

	void tweetSheetCompletedEvent (bool didSucceed)
	{
		#if UNITY_ANDROID
		EtceteraAndroid.hideProgressDialog();
		#endif
		Debug.Log ("tweetSheetCompletedEvent " + didSucceed);
		if (didSucceed) {
			ShowCompleteDialog ();
		}
	}

	private void ShowCompleteDialog ()
	{
		int shareImageId = PrefsManager.getInstance ().GetShareImageId ();
		Hashtable bandmanData = CharactorListDao.getInstance ().getBandmanData (shareImageId);
		int flagTweet = (int)bandmanData [CharactorListDao.FLAG_TWEET_FIELD];
		string title = "";
		string message = "";
		switch (flagTweet) {
		case CharactorListDao.NOT_TWEET:
			AddKyaba ();
			CharactorListDao.getInstance ().UpdateFlagTweet (shareImageId, CharactorListDao.TWEETED);
			title = "\u30c4\u30a4\u30fc\u30c8\u6210\u529f";
			message = "\u30ad\u30e3\u30d0\u5b22\u30b2\u30c3\u30c8!!";
			break;
		case CharactorListDao.TWEETED:
			title = "\u30c4\u30a4\u30fc\u30c8\u6210\u529f";
			message = "\u30ad\u30e3\u30d0\u5b22\u306f\u8ffd\u52a0\u6e08\u307f\u3067\u3059";
			break;
		}
		ShowOKDialog(title,message);
	}

	private void ShowOKDialog(string title,string message){
		#if UNITY_IPHONE
		string[] buttons = new string[] {"OK"};
		EtceteraBinding.showAlertWithTitleMessageAndButtons (title, message, buttons);
		#endif
		#if UNITY_ANDROID
		EtceteraAndroid.showAlert(title,message,"OK");
		#endif
	}

	private void AddKyaba ()
	{
		Hashtable playerData = PlayerDataDao.getInstance ().getPlayerData ();
		int kyabaSize = (int)playerData [PlayerDataDao.KYABA_SIZE];
		kyabaSize++;
		PlayerDataDao.getInstance ().UpdateKyabaSize (kyabaSize);
		GameObject kyaba = Instantiate (kyabaPrefab) as GameObject;
		kyaba.transform.parent = cameraAnchor.transform;
		kyaba.transform.position = cameraAnchor.transform.position;
	}

}
