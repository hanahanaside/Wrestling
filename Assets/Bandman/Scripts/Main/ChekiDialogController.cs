using UnityEngine;
using System.Collections;

public class ChekiDialogController : MonoBehaviour
{
	public UILabel descriptionLabel;
	public UILabel pageCountLabel;
	public UISprite chekiSprite;
	private int mEvolutionPoint;
	private int mPageCount;
	private int mCleared;
	private string mName;

	void Start ()
	{

		Hashtable playerData = PlayerDataDao.getInstance().getPlayerData();
		mEvolutionPoint = (int)playerData[PlayerDataDao.EVOLUTION_POINT_FIELD];
		mCleared = (int)playerData[PlayerDataDao.CLEARED_FIELD];
		mPageCount = mEvolutionPoint;
		UpdateData ();

	}

	private void UpdateData ()
	{
		if (mCleared == 1 || mPageCount <= mEvolutionPoint) {
			Hashtable bandmanData = CharactorListDao.getInstance().getBandmanData(mPageCount);
			mName = (string)bandmanData[CharactorListDao.NAME_FIELD];
			descriptionLabel.text = (string)bandmanData[CharactorListDao.DESCRIPTION_FIELD];
			chekiSprite.spriteName = "cheki_" + mPageCount;
		} else {
			descriptionLabel.text = "???";
			chekiSprite.spriteName = "cheki_question";
		}
		pageCountLabel.text = mPageCount + "/23";

	}

	public void OnButtonClick ()
	{
		string buttonName = UIButton.current.name;
		if (buttonName == "BackButton") {
			GameObject.Find("UIFence").SetActive(false);
			Destroy (gameObject.transform.parent.gameObject);
		}
		if (buttonName == "PreviousChekiButton") {
			Debug.Log (buttonName);
			if (mPageCount <= 1) {
				mPageCount = 23;
			} else {
				mPageCount--;
			}
			UpdateData ();
		}

		if (buttonName == "NextChekiButton") {
			if (mPageCount >= 23) {
				mPageCount = 1;
			} else {
				mPageCount++;
			}

			UpdateData ();
		}
		if (buttonName == "TweetButton") {
			if (mCleared == 1 || mPageCount <= mEvolutionPoint) {
				PrefsManager.getInstance().SaveShareName(mName);
				PrefsManager.getInstance().SaveShareImageId(mPageCount);
				StartCoroutine(GameObject.Find("ShareManager").GetComponent<ShareManager>().showTweetComposer());
			}else {
				#if UNITY_ANDROID
				string title = "\u30c4\u30a4\u30fc\u30c8\u3067\u304d\u307e\u305b\u3093";
				string message = "\u30c4\u30a4\u30fc\u30c8\u3067\u304d\u308b\u306e\u306f\u9032\u5316\u3057\u305f\u30ad\u30e3\u30e9\u306e\u307f\u3067\u3059 ";
				EtceteraAndroid.showAlert(title,message,"OK");
#endif
			}

		}
	}
}
