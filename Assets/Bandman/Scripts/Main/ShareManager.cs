using UnityEngine;
using System.Collections;
using System.IO;

public class ShareManager : MonoBehaviour
{

	public string consumerKey;
	public string consumerSecret;
	private string mImagePath;
	private string mTweetText;

	void OnEnable ()
	{
		#if UNITY_ANDROID
		EtceteraAndroidManager.promptFinishedWithTextEvent += promptFinishedWithTextEvent;
		EtceteraAndroidManager.promptCancelledEvent += promptCancelledEvent;
#endif
	}
	
	void OnDisable ()
	{
		#if UNITY_ANDROID
		EtceteraAndroidManager.promptFinishedWithTextEvent -= promptFinishedWithTextEvent;
		EtceteraAndroidManager.promptCancelledEvent -= promptCancelledEvent;	
#endif
	}

	
	void Awake ()
	{
		#if UNITY_IPHONE
		TwitterBinding.init(consumerKey,consumerSecret);
		#endif

#if UNITY_ANDROID
		TwitterAndroid.init( consumerKey, consumerSecret );
#endif
	}

	#if UNITY_ANDROID
	void promptFinishedWithTextEvent( string param )
	{
		Debug.Log( "promptFinishedWithTextEvent: " + param );
		StartCoroutine(	Tweet());
		
	}
	#endif
	
	void promptCancelledEvent()
	{
		Debug.Log( "promptCancelledEvent" );
	}

	
	public IEnumerator showTweetComposer ()
	{
		string name = PrefsManager.getInstance ().GetShareName ();
		int id = PrefsManager.getInstance ().GetShareImageId ();
		mTweetText = "【" + name + "に進化した！】ブサ可愛いバンドマン育成アプリ、これ面白いからやってみて！⇒ http://tt5.us/band  #V系バン麺";


		#if UNITY_IPHONE
		mImagePath = Application.dataPath + "/Raw/player" + id + "_a.png";
		yield return new WaitForSeconds (1.0f);
		TwitterBinding.showTweetComposer (mTweetText, mImagePath);
		#endif

#if UNITY_ANDROID
	//	mImagePath = "jar:file://" +Application.dataPath + "!/assets/player" + id + "_a.png";
		mImagePath = Path.Combine(Application.streamingAssetsPath,"player"+id + "_a.png");
		Debug.Log("logIn = "+TwitterAndroid.isLoggedIn());
		if(TwitterAndroid.isLoggedIn()){
			EtceteraAndroid.showAlertPrompt("Twitter" ,"Message","",mTweetText,"\u6295\u7a3f","\u30ad\u30e3\u30f3\u30bb\u30eb");
		}else {
			EtceteraAndroid.showProgressDialog("\u30ed\u30b0\u30a4\u30f3\u3057\u3066\u3044\u307e\u3059","\u30ed\u30b0\u30a4\u30f3\u3057\u3066\u3044\u307e\u3059");
			TwitterAndroid.showLoginDialog();
			Debug.Log("showLoginDialog");
		}
		yield return 0;
#endif   
		yield return 0;
	}

	#if UNITY_ANDROID
	private IEnumerator Tweet(){

		EtceteraAndroid.showProgressDialog("\u304a\u5f85\u3061\u304f\u3060\u3055\u3044","\u30c4\u30a4\u30fc\u30c8\u3057\u3066\u3044\u307e\u3059");
		WWW www = new WWW(mImagePath);
		yield return www;
		TwitterAndroid.postStatusUpdate (mTweetText, www.bytes);

	}
	#endif 

}
