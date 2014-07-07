using UnityEngine;
using System.Collections;

public class SettingDialogController : MonoBehaviour
{
	public GameObject kyabaPrefab;
	public  ReviewDialog reviewDialogPrefab;
	private UISprite soundButtonSprite;
	private UISprite notificationButtonSprite;
	
	void Start ()
	{
		soundButtonSprite = GameObject.Find ("SoundButton").GetComponent<UISprite> ();
		notificationButtonSprite = GameObject.Find ("NotificationButton").GetComponent<UISprite> ();
		int soundMode = PrefsManager.getInstance ().GetSoundMode ();
		switch (soundMode) {
		case PrefsManager.SOUND_ON:
			soundButtonSprite.spriteName = "bt_on";
			break;
		case PrefsManager.SOUND_OFF:
			soundButtonSprite.spriteName = "bt_off";
			break;
		}
		if (PrefsManager.getInstance ().NotificationActive ()) {
			notificationButtonSprite.spriteName = "bt_on";
		} else {
			notificationButtonSprite.spriteName = "bt_off";
		}
	}
	
	public void OnButtonClick ()
	{
		string buttonName = UIButton.current.name;
		if (buttonName == "BackButton") {
			OnBackButtonClick ();
		}
		if (buttonName == "StoryButton") {
			OnStoryButtonClick ();
		}
		if (buttonName == "SoundButton") {
			OnSoundButtonClick ();
		}
		if (buttonName == "HowtoButton") {
			OnHowtoButtonClick ();
		}
		if (buttonName == "ShareButton") {
			OnShareButtonClick ();
		}
		if (buttonName == "ReviewButton") {
			OnReviewButtonClick ();
		}
		if (buttonName == "NotificationButton") {
			OnNotificationClick ();
		}
	}

	private void OnNotificationClick ()
	{
		if (PrefsManager.getInstance ().NotificationActive ()) {
			PrefsManager.getInstance ().SaveNotificationActive (false);
			#if UNITY_IOS
			NotificationServices.CancelAllLocalNotifications ();
#endif
			notificationButtonSprite.spriteName = "bt_off";
		} else {
			PrefsManager.getInstance ().SaveNotificationActive (true);
			notificationButtonSprite.spriteName = "bt_on";
		}
	}

	private void OnSoundButtonClick ()
	{
		int soundMode = PrefsManager.getInstance ().GetSoundMode ();
		switch (soundMode) {
		case PrefsManager.SOUND_ON:
			PrefsManager.getInstance ().SaveSoundMode (PrefsManager.SOUND_OFF);
			soundButtonSprite.spriteName = "bt_off";
			break;
		case PrefsManager.SOUND_OFF:
			PrefsManager.getInstance ().SaveSoundMode (PrefsManager.SOUND_ON);
			soundButtonSprite.spriteName = "bt_on";
			break;
		}
		GameObject soundManager = GameObject.Find ("SoundManager");
		soundManager.SendMessage ("SoundModeChanged");
	}

	private void OnHowtoButtonClick ()
	{
		Application.LoadLevel ("Tutorial");
	}

	private void OnShareButtonClick ()
	{

		Hashtable playerData = PlayerDataDao.getInstance ().getPlayerData ();
		int evolutionPoint = (int)playerData [PlayerDataDao.EVOLUTION_POINT_FIELD];
		PrefsManager.getInstance ().SaveShareImageId (evolutionPoint);
		
		string name = GameObject.Find ("Name").GetComponent<UILabel> ().text;
		PrefsManager.getInstance ().SaveShareName (name);
		StartCoroutine (GameObject.Find ("ShareManager").GetComponent<ShareManager> ().showTweetComposer ());
	}

	private void OnStoryButtonClick ()
	{
		Application.LoadLevel ("Opening");
	}

	private void OnReviewButtonClick ()
	{
		ReviewDialog reviewDialog = Instantiate(reviewDialogPrefab) as ReviewDialog;
		reviewDialog.Show();
	}

	private void OnBackButtonClick ()
	{
		GameObject.Find("UIFence").SetActive(false);
		Destroy (gameObject.transform.parent.gameObject);
	}
}
