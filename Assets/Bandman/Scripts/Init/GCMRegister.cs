
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GCMRegister : MonoBehaviour {
	
	private const string URL = "http://push.tt5.us/api/receive";
	public int projectId;
	public string senderId;

	#if UNITY_ANDROID
	void OnEnable()
	{
		Debug.Log("OnEnable Receiver",gameObject);
		// Listen to all events for illustration purposes
		GoogleCloudMessagingManager.notificationReceivedEvent += notificationReceivedEvent;
		GoogleCloudMessagingManager.registrationSucceededEvent += registrationSucceededEvent;
		GoogleCloudMessagingManager.unregistrationFailedEvent += unregistrationFailedEvent;
		GoogleCloudMessagingManager.registrationFailedEvent += registrationFailedEvent;
		GoogleCloudMessagingManager.unregistrationSucceededEvent += unregistrationSucceededEvent;
	}
	
	
	void OnDisable()
	{
		Debug.Log("OnDisable Receiver",gameObject);
		// Remove all event handlers
		GoogleCloudMessagingManager.notificationReceivedEvent -= notificationReceivedEvent;
		GoogleCloudMessagingManager.registrationSucceededEvent -= registrationSucceededEvent;
		GoogleCloudMessagingManager.unregistrationFailedEvent -= unregistrationFailedEvent;
		GoogleCloudMessagingManager.registrationFailedEvent -= registrationFailedEvent;
		GoogleCloudMessagingManager.unregistrationSucceededEvent -= unregistrationSucceededEvent;
	}
	
	void Start(){
		DontDestroyOnLoad(gameObject);
		ChceckRegistered();
		Debug.Log("Start Receiver");
	}
	
	void OnApplicationPause(bool pauseState){
		if(!pauseState){
			ChceckRegistered();
		}
	}

	private void ChceckRegistered(){
		if (PlayerPrefs.GetInt("registered") == 0) {
			GoogleCloudMessaging.register(senderId);
			Debug.Log ("登録開始");
		}else {
			Debug.Log ("登録済み");
			GoogleCloudMessaging.checkForNotifications();
			Debug.Log("checkForNotifications");
		}
	}
	
	void notificationReceivedEvent( Dictionary<string,object> dict )
	{
		Debug.Log( "notificationReceivedEvent" );
	}
	
	
	
	void registrationSucceededEvent( string registrationId )
	{
		Debug.Log( "registrationSucceededEvent: " + registrationId );
		StartCoroutine(PostRegistrationId(registrationId));
	}
	
	
	void unregistrationFailedEvent( string error )
	{
		Debug.Log( "unregistrationFailedEvent: " + error );
	}
	
	
	void registrationFailedEvent( string error )
	{
		Debug.Log( "registrationFailedEvent: " + error );
	}
	
	
	void unregistrationSucceededEvent()
	{
		Debug.Log( "UnregistrationSucceededEvent" );
	}
	
	private IEnumerator PostRegistrationId(string registrationId){
		Debug.Log("PostRegistrationId");
		string osVersion = SystemInfo.operatingSystem.Replace ("Android", "");
		string platform = "Android";
		Debug.Log ("osVersion = " + osVersion);
		Debug.Log ("platform = " + platform);
		WWWForm form = new WWWForm();
		form.AddField("v",0);
		form.AddField("pid",projectId);
		form.AddField ("os_version", osVersion);
		form.AddField ("device_token", registrationId);
		WWW www = new WWW (URL, form);
		yield return www;
		
		// check for errors
		if (www.error == null) {
			Debug.Log ("WWW Ok!: " + www.text);
			PlayerPrefs.SetInt("registered",1);
		} else {
			Debug.Log ("WWW Error: " + www.error);
		}
	}
	#endif
}
