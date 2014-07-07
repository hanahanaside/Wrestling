using UnityEngine;
using System.Collections;

public abstract class AlertDialog : MonoBehaviour {

	void OnEnable ()
	{
		#if UNITY_IPHONE
		EtceteraManager.alertButtonClickedEvent += alertButtonClicked;
		#endif
		
		#if UNITY_ANDROID
		EtceteraAndroidManager.alertButtonClickedEvent += alertButtonClicked;
		EtceteraAndroidManager.alertCancelledEvent += alertCancelledEvent;
		#endif
	}
	
	void OnDisable ()
	{
		#if UNITY_IPHONE
		EtceteraManager.alertButtonClickedEvent -= alertButtonClicked;
		#endif

		#if UNITY_ANDROID
		EtceteraAndroidManager.alertButtonClickedEvent -= alertButtonClicked;
		EtceteraAndroidManager.alertCancelledEvent -= alertCancelledEvent;
		#endif
	}


	public abstract void alertButtonClicked (string text);

	public abstract void Show();

	#if UNITY_ANDROID
	public virtual void alertCancelledEvent ()
	{
		EtceteraAndroidManager.alertButtonClickedEvent -= alertButtonClicked;
		EtceteraAndroidManager.alertCancelledEvent -= alertCancelledEvent;
	}
	#endif

}