using UnityEngine;
using System.Collections;

public class GameFeatManager : MonoBehaviour
{

	public string iosMediaId;
	private static GameFeatManager sInstance;

	public static GameFeatManager instance {
		get {
			return sInstance;
		}
	}

	void Start ()
	{
		if (sInstance == null) {
			sInstance = this;
#if UNITY_IPHONE
			// GAMEFEAT 初期化（必ず記述して下さい）
			GFUnityPulugin.activate (iosMediaId, false, false, false);

#endif



#if UNITY_ANDROID
			initAndroid();
#endif
		} 
	}
	
	#if UNITY_ANDROID
	private void initAndroid(){
		DontDestroyOnLoad(gameObject);
		AndroidJavaClass unityPlayer = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
		AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject> ("currentActivity");
		AndroidJavaClass plugin = new AndroidJavaClass ("jp.basicinc.gamefeat.android.unity.GameFeatUnityPlugin");
		plugin.CallStatic ("activateGF", activity, false, true, true);
	}
	#endif


	void OnApplicationPause (bool pauseStatus)
	{
#if UNITY_ANDROID
		if (!pauseStatus) {
			initAndroid();
		}
#endif
	}


	public void loadGF ()
	{
		#if UNITY_IPHONE
		// オファーウォール型広告の実装サンプル
		GFUnityPulugin.start (iosMediaId);
		#endif

#if UNITY_ANDROID
		AndroidJavaObject plugin = new AndroidJavaObject("jp.basicinc.gamefeat.android.sdk.controller.GameFeatAppController");
		AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
		plugin.Call("show", activity);
		Debug.Log("Show");
#endif


	}



}
