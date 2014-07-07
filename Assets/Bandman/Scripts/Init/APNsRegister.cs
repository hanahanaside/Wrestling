using UnityEngine;
using MiniJSON;
using System.Net;
using System.IO;
using System.Collections;

public class APNsRegister : MonoBehaviour
{
	private const string TT5_URL = "http://push.tt5.us/api/receive";
		public int projectId;

	#if UNITY_IPHONE
	
	void OnEnable ()
	{
		EtceteraManager.remoteRegistrationSucceededEvent += remoteRegistrationSucceeded;
		EtceteraManager.remoteRegistrationFailedEvent += remoteRegistrationFailed;
	}
	
	void OnDisable ()
	{
		EtceteraManager.remoteRegistrationSucceededEvent -= remoteRegistrationSucceeded;
		EtceteraManager.remoteRegistrationFailedEvent -= remoteRegistrationFailed;
	}

	void Start ()
	{
		if (!PrefsManager.getInstance ().isRegistered ()) {
			Debug.Log ("登録開始");
			DontDestroyOnLoad(gameObject);
			RegisterForRemoteNotifcations ();
		}else {
			Debug.Log ("登録済み");
		} 

	}

	void remoteRegistrationFailed (string error)
	{
		Debug.Log ("remoteRegistrationFailed : " + error);
	}
	
	void remoteRegistrationSucceeded (string deviceToken)
	{
		Debug.Log ("remoteRegistrationSucceeded : " + deviceToken);
		
		string osVersion = SystemInfo.operatingSystem.Replace ("iPhone OS ", "");
		string platform = iPhone.generation.ToString();
		Debug.Log ("osVersion = " + osVersion);
		Debug.Log ("platform = " + platform);
		Debug.Log ("projectId = " + projectId);
		WWWForm form = new WWWForm ();
		form.AddField ("v", 0);
		form.AddField ("pid", projectId);
		form.AddField ("os_version", osVersion);
		form.AddField ("platform", platform);
		form.AddField ("device_token", deviceToken);
		WWW www = new WWW (TT5_URL, form);
		StartCoroutine (SendDeviceToken (www));
	}


	private void RegisterForRemoteNotifcations ()
	{
	//	NotificationServices.RegisterForRemoteNotificationTypes (RemoteNotificationType.Alert | RemoteNotificationType.Badge | RemoteNotificationType.Sound);
		EtceteraBinding.registerForRemoteNotifcations (P31RemoteNotificationType.Alert | P31RemoteNotificationType.Badge | P31RemoteNotificationType.Sound);
	}
	

	private IEnumerator SendDeviceToken (WWW www)
	{
		Debug.Log ("SendDeviceToken");
		yield return www;
		
		// check for errors
		if (www.error == null) {
			Debug.Log ("WWW Ok!: " + www.text);
			PrefsManager.getInstance ().SaveRegistered ();
		} else {
			Debug.Log ("WWW Error: " + www.error);
		}
	}
	
#endif
}
