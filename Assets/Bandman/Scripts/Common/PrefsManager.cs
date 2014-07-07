using UnityEngine;
using System.Collections;

public class PrefsManager : ScriptableObject
{

	public  const int TUTORIAL_FINISHED = 1;
	public const int SOUND_ON = 0;
	public const int SOUND_OFF = 1;
	private static PrefsManager sInstance;
	private const string IS_TUTORIAL_FINISHED = "isTutorialFinished";
	private const string IS_REGISTERED = "isRegistered";
	private const string SOUND_MODE = "soundMode";
	private const string START_COUNT = "startCount";
	private const string IS_REVIEWED = "isReviewed";
	private const string SHARE_NAME = "shareName";
	private const string SHARE_IMAGE_ID = "shareImageId";
	private const string PUBLICITY_INSTALLED = "publicityInstalled";
	private const string NOTIFICATION_ACTIVE_KEY = "notificationActive";
	private const string EXIT_TIME_KEY = "exitTime";
	private const string RECOMMEND_COUNT_KEY = "recommendCount";

	public static PrefsManager getInstance ()
	{
		if (sInstance == null) {
			sInstance = ScriptableObject.CreateInstance<PrefsManager> ();
		}
		return sInstance;
	}

	public void saveTutorialFinished ()
	{
		PlayerPrefs.SetInt (IS_TUTORIAL_FINISHED, 1);
		PlayerPrefs.Save ();
	}

	public int isTutorialFinished ()
	{
		return PlayerPrefs.GetInt (IS_TUTORIAL_FINISHED);
	}

	public void SaveRegistered ()
	{
		PlayerPrefs.SetInt (IS_REGISTERED, 1);
		PlayerPrefs.Save ();
	}

	public bool isRegistered ()
	{
		int isRegistered = PlayerPrefs.GetInt (IS_REGISTERED);
		if (isRegistered == 0) {
			return false;
		}
		return true;
	}

	public void SaveSoundMode (int mode)
	{
		PlayerPrefs.SetInt (SOUND_MODE, mode);
		PlayerPrefs.Save ();
	}

	public int GetSoundMode ()
	{
		return PlayerPrefs.GetInt (SOUND_MODE);
	}

	public void SaveStartCount (int startCount)
	{
		PlayerPrefs.SetInt (START_COUNT, startCount);
		PlayerPrefs.Save ();
	}

	public int GetStartCount ()
	{
		return PlayerPrefs.GetInt (START_COUNT);
	}

	public void SetReviewed ()
	{
		PlayerPrefs.SetInt (IS_REVIEWED, 1);
		PlayerPrefs.Save ();
	}

	public bool GetReviewed ()
	{
		if (PlayerPrefs.GetInt (IS_REVIEWED) == 0) {
			return false;
		}
		return true;
	}

	public void SavePublicityInstalled ()
	{
		PlayerPrefs.SetInt (PUBLICITY_INSTALLED, 1);
		PlayerPrefs.Save ();
	}

	public bool PublicityInstalled ()
	{
		if (PlayerPrefs.GetInt (PUBLICITY_INSTALLED) == 0) {
			return false;
		}
		return true;
	}

	public void SaveShareName (string name)
	{
		PlayerPrefs.SetString (SHARE_NAME, name);
		PlayerPrefs.Save ();
	}

	public string GetShareName ()
	{
		return PlayerPrefs.GetString (SHARE_NAME);
	}

	public void SaveShareImageId (int imageId)
	{
		PlayerPrefs.SetInt (SHARE_IMAGE_ID, imageId);
		PlayerPrefs.Save ();
	}

	public int GetShareImageId ()
	{
		return PlayerPrefs.GetInt (SHARE_IMAGE_ID);
	}

	public void SaveNotificationActive (bool state)
	{
		if (state) {
			PlayerPrefs.SetInt (NOTIFICATION_ACTIVE_KEY, 0);
		} else {
			PlayerPrefs.SetInt (NOTIFICATION_ACTIVE_KEY, 1);
		}
		PlayerPrefs.Save ();
	}

	public bool NotificationActive ()
	{
		int notificationActive = PlayerPrefs.GetInt (NOTIFICATION_ACTIVE_KEY);
		if (notificationActive == 0) {
			return true;
		}
		return false;
	}

	public void SaveExitTime(string exitTime){
		PlayerPrefs.SetString(EXIT_TIME_KEY,exitTime);
		PlayerPrefs.Save();
	}

	public string GetExitTime(){
		return PlayerPrefs.GetString(EXIT_TIME_KEY);
	}

	public void SaveRecommendCount(int recommendCount){
		PlayerPrefs.SetInt(RECOMMEND_COUNT_KEY,recommendCount);
		PlayerPrefs.Save();
	}
	
	public int GetRecommendCount(){
		return PlayerPrefs.GetInt(RECOMMEND_COUNT_KEY);
	}

}
