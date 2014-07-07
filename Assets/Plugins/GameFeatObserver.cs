using UnityEngine;
using System.Collections;
#if UNITY_ANDROID
public class  GameFeatObserver : MonoBehaviour {

	AndroidJavaClass plugin;
	AndroidJavaClass unityPlayer;
	AndroidJavaClass gfAppController;
	AndroidJavaObject activity;
	
	void Start () {
		
		unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

		plugin = new AndroidJavaClass("jp.basicinc.gamefeat.android.unity.GameFeatUnityPlugin");
		plugin.CallStatic("activateGF", activity, true, true, true);
		
		plugin.CallStatic("drawGFButton", activity, "button1", 40, 40, 10, 100);
		plugin.CallStatic("drawGFButton", activity, 10, 200);

		plugin.CallStatic("initIconAd", activity);
		plugin.CallStatic("setIconAd", activity, 120, 120, 0, 650);
		plugin.CallStatic("setIconAd", activity, 120, 120, 120, 650);
		plugin.CallStatic("setIconAd", activity, 120, 120, 240, 650);
		plugin.CallStatic("setIconAd", activity, 120, 120, 360, 650);
		plugin.CallStatic("loadIconAd", activity);
		
	}
	
	void OnApplicationPause (bool pauseStatus){
        if (pauseStatus) {
			plugin.CallStatic("stopIconAd", activity); 
		}else{
	       	unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
	
			plugin = new AndroidJavaClass("jp.basicinc.gamefeat.android.unity.GameFeatUnityPlugin");
			plugin.CallStatic("activateGF", activity, true, true, true);
			plugin.CallStatic("startIconAd", activity); 
		}
	}	

	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI()
	{
		Rect rect = new Rect(10, 10, 400, 100);
		bool isClicked = GUI.Button(rect, "GAME FEAT");
		if (isClicked)
		{
			loadGF();
		}
		
		Rect rect2 = new Rect(10, 250, 400, 100);
		bool isClicked2 = GUI.Button(rect2, "view popup");
		if (isClicked2)
		{
			loadPoupGF();
		}
		
		Rect rect3 = new Rect(10, 350, 400, 100);
		bool isClicked3 = GUI.Button(rect3, "close view pop");
		if (isClicked3)
		{
			loadExitPoupGF();
		}	
		
		Rect rect4 = new Rect(10, 450, 400, 100);
		bool isClicked4 = GUI.Button(rect4, "icon refresh stop");
		if (isClicked4)
		{
			stopIconAd();
		}

		Rect rect5 = new Rect(10, 550, 400, 100);
		bool isClicked5 = GUI.Button(rect5, "invisible icon");
		if (isClicked5)
		{
			invisibleIconAd();
		}

		Rect rect6 = new Rect(10, 650, 400, 100);
		bool isClicked6 = GUI.Button(rect6, "visible icon");
		if (isClicked6)
		{
			visibleIconAd();
		}	
	}
	
	void stopIconAd(){
		plugin.CallStatic("stopIconAd", activity);
	}	
	
	void startIconAd(){
		plugin.CallStatic("startIconAd", activity);
	}	
	
	void invisibleIconAd(){
		plugin.CallStatic("invisibleIconAd", activity);
	}
	
	void visibleIconAd(){
		plugin.CallStatic("visibleIconAd", activity);
	}
	
	void loadGF(){
		AndroidJavaObject plugin = new AndroidJavaObject("jp.basicinc.gamefeat.android.sdk.controller.GameFeatAppController");
		AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
		plugin.Call("show", activity);
	}
	
	void loadExitPoupGF(){
		AndroidJavaClass plugin = new AndroidJavaClass("jp.basicinc.gamefeat.android.unity.GameFeatUnityPlugin");
		AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
		
		plugin.CallStatic("loadExitPopupAd", activity, "Main Camera");
	}
	
	void loadPoupGF(){
		AndroidJavaClass plugin = new AndroidJavaClass("jp.basicinc.gamefeat.android.unity.GameFeatUnityPlugin");
		AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
		
		plugin.CallStatic("setPopupProbability", 1);
		plugin.CallStatic("loadPopupAd", activity, "Main Camera");
	}
	
	// GameFeat全画面が開いた時のcallback先
	void didShowGameFeatPopup(string msg) {
		// GameFeat全画面が開いた時の処理
		Debug.Log("didShowGameFeatPopup");
	}
	
	// GameFeat全画面が開いた時のcallback先
	void didCloseGameFeatPopup(string msg) {
		// GameFeat全画面が閉じた時の処理
		Debug.Log("didCloseGameFeatPopup");
	}
	
	// GameFeat全画面が開いた時のcallback先
	void didFinishGameFeatPopup(string msg) {
		// GameFeat全画面が閉じた時の処理
		Debug.Log("didFinishGameFeatPopup");
	 	Application.Quit();
	}
	
	// GameFeat全画面が開いた時のcallback先
	void failGameFeatPopupData(string msg) {
		// GameFeat全画面が開けなかった時の処理
		Debug.Log("failGameFeatPopupData");
	}

}
#endif