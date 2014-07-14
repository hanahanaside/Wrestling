using UnityEngine;
using System.Collections;

public class ChekiDialogController : MonoBehaviour
{
	public UILabel descriptionLabel;
	public UILabel pageCountLabel;
	public UISprite chekiSprite;
	public GameObject youTubeViewPrefab;
	private int mEvolutionPoint;
	private int mPageCount;
	private int mCleared;
	private string mName;
	private GameObject mUiRoot;

	void Start ()
	{
		Hashtable playerData = PlayerDataDao.getInstance ().getPlayerData ();
		mEvolutionPoint = (int)playerData [PlayerDataDao.EVOLUTION_POINT_FIELD];
		mCleared = (int)playerData [PlayerDataDao.CLEARED_FIELD];
		mPageCount = mEvolutionPoint;
		mUiRoot = GameObject.Find ("UI Root");
		UpdateData ();

	}

	private void UpdateData ()
	{
		if (mCleared == 1 || mPageCount <= mEvolutionPoint) {
			Hashtable bandmanData = CharactorListDao.getInstance ().getBandmanData (mPageCount);
			mName = (string)bandmanData [CharactorListDao.NAME_FIELD];
			descriptionLabel.text = (string)bandmanData [CharactorListDao.DESCRIPTION_FIELD];
			chekiSprite.spriteName = "cheki_" + mPageCount;
		} else {
			descriptionLabel.text = "???";
			chekiSprite.spriteName = "cheki_question";
		}
		pageCountLabel.text = mPageCount + "/24";

	}

	public void OnButtonClick ()
	{
		string buttonName = UIButton.current.name;
		if (buttonName == "BackButton") {
			GameObject.Find ("UIFence").SetActive (false);
			Destroy (gameObject.transform.parent.gameObject);
		}
		if (buttonName == "PreviousChekiButton") {
			Debug.Log (buttonName);
			if (mPageCount <= 1) {
				mPageCount = 24;
			} else {
				mPageCount--;
			}
			UpdateData ();
		}

		if (buttonName == "NextChekiButton") {
			if (mPageCount >= 24) {
				mPageCount = 1;
			} else {
				mPageCount++;
			}

			UpdateData ();
		}
		if (buttonName == "TweetButton") {
			if (mCleared == 1 || mPageCount <= mEvolutionPoint) {
				PrefsManager.getInstance ().SaveShareName (mName);
				PrefsManager.getInstance ().SaveShareImageId (mPageCount);
				StartCoroutine (GameObject.Find ("ShareManager").GetComponent<ShareManager> ().showTweetComposer ());
			} else {
				#if UNITY_ANDROID
				string title = "\u30c4\u30a4\u30fc\u30c8\u3067\u304d\u307e\u305b\u3093";
				string message = "\u30c4\u30a4\u30fc\u30c8\u3067\u304d\u308b\u306e\u306f\u9032\u5316\u3057\u305f\u30ad\u30e3\u30e9\u306e\u307f\u3067\u3059 ";
				EtceteraAndroid.showAlert(title,message,"OK");
#endif
			}

		}
		if (buttonName == "YouTubeButton") {
			GameObject youTubeViewObject = Instantiate (youTubeViewPrefab) as GameObject;
			youTubeViewObject.transform.parent = mUiRoot.transform;
			youTubeViewObject.transform.localPosition = new Vector3 (0, 0, 0);
			youTubeViewObject.transform.localScale = new Vector3 (1, 1, 1);
			youTubeViewObject.BroadcastMessage ("Show", mEvolutionPoint);
			Destroy (gameObject.transform.parent.gameObject);
		}
	}
}
