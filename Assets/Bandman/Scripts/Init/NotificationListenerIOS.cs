using UnityEngine;
using System.Collections;

public class NotificationListenerIOS : MonoBehaviour
{

	public RecommendAppDialog recommendAppDialogPrefab;

#if UNITY_IPHONE
	void OnEnable ()
	{
		EtceteraManager.localNotificationWasReceivedAtLaunchEvent += localNotificationWasReceivedAtLaunchEvent;
		EtceteraManager.localNotificationWasReceivedEvent += localNotificationWasReceivedEvent;
		EtceteraManager.remoteNotificationReceivedAtLaunchEvent += remoteNotificationReceivedAtLaunch;
		EtceteraManager.remoteNotificationReceivedEvent += remoteNotificationReceived;
	}

	void OnDisable ()
	{
		EtceteraManager.localNotificationWasReceivedAtLaunchEvent -= localNotificationWasReceivedAtLaunchEvent;
		EtceteraManager.localNotificationWasReceivedEvent -= localNotificationWasReceivedEvent;
		EtceteraManager.remoteNotificationReceivedAtLaunchEvent -= remoteNotificationReceivedAtLaunch;
		EtceteraManager.remoteNotificationReceivedEvent -= remoteNotificationReceived;
	}

	void Start()
	{
		DontDestroyOnLoad(gameObject);
	}

	void OnApplicationPause(bool pauseStatus) {
		if(!pauseStatus){
			ClearNotifications();
		}
	}

	void localNotificationWasReceivedEvent (IDictionary notification)
	{
		Debug.Log ("localNotificationWasReceivedEvent");
		Prime31.Utils.logObject (notification);
	}
	
	void localNotificationWasReceivedAtLaunchEvent (IDictionary notification)
	{
		Debug.Log ("localNotificationWasReceivedAtLaunchEvent");
		Prime31.Utils.logObject (notification);
		ClearNotifications();
	}

	void remoteNotificationReceivedAtLaunch (IDictionary notification)
	{
		Debug.Log ("remoteNotificationReceivedAtLaunch");
		Prime31.Utils.logObject (notification);
		ClearNotifications();
		RecommendAppDialog recommendAppDialog = Instantiate(recommendAppDialogPrefab) as RecommendAppDialog;
		recommendAppDialog.Show();
	}

	void remoteNotificationReceived( IDictionary notification )
	{
		Debug.Log( "remoteNotificationReceived" );
		Prime31.Utils.logObject( notification );
	}

	private void ClearNotifications ()
	{
		LocalNotification localNtification = new LocalNotification ();
		localNtification.applicationIconBadgeNumber = -1;
		NotificationServices.PresentLocalNotificationNow (localNtification);
		NotificationServices.CancelAllLocalNotifications ();
		NotificationServices.ClearRemoteNotifications ();
		NotificationServices.ClearLocalNotifications ();
	}



#endif
}
