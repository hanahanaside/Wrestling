using UnityEngine;
using System.Collections;

public class ResetDialog : AlertDialog
{
	
	#if UNITY_ANDROID
		public override void alertCancelledEvent ()
		{
		Show();
		}
	#endif
	

	private void Reset ()
	{
		PlayerDataDao.getInstance ().Reset ();
		CharactorListDao.getInstance ().Reset ();
		PlayerDataDao.getInstance ().UpdateCleared (1);
		Application.LoadLevel ("Main");
	}

	public override void alertButtonClicked (string text)
	{
		Reset ();
	}

	public override void Show ()
	{
		string title = "\u304a\u75b2\u308c\u69d8\u3067\u3057\u305f";
		string message = "\u6700\u521d\u304b\u3089\u59cb\u3081\u307e\u3059\u3088\uff1f";
		#if UNITY_IOS
		var buttons = new string[] { "OK"};
		EtceteraBinding.showAlertWithTitleMessageAndButtons(title,  message, buttons);
		#endif
		
		#if UNITY_ANDROID
		EtceteraAndroid.showAlert(title,message,"OK");
		#endif
	}
}
