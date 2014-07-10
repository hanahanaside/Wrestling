using UnityEngine;
using System.Collections;

public class NotificationSender : MonoBehaviour
{

	private static int notificationId;

	public void ScheduleLocalNotification (double addSeconds)
	{
		if (!PrefsManager.getInstance ().NotificationActive ()) {
			return;
		}

		#if UNITY_IPHONE
		LocalNotification localNotification = new LocalNotification ();
		localNotification.applicationIconBadgeNumber = 1;
		localNotification.fireDate = System.DateTime.Now.AddSeconds (addSeconds);
		localNotification.alertBody = "ジムが練習生だらけです";
		NotificationServices.CancelAllLocalNotifications ();
		NotificationServices.ScheduleLocalNotification (localNotification);
		#endif
		
		#if UNITY_ANDROID
		long secondsFromNow =  (long)addSeconds;
		string title = "ぼくたちのプロレス";
		string subTitle = "ジムが練習生だらけです";
		string tickerText = "ジムが練習生だらけです";
		string extraData = "extraData";
		EtceteraAndroid.cancelAllNotifications();
	 notificationId = EtceteraAndroid.scheduleNotification(secondsFromNow,title,subTitle,tickerText,extraData);
		Debug.Log("notificationId = "+notificationId);
		Debug.Log("secondsFromNow = "+secondsFromNow);
		#endif
	}

}
