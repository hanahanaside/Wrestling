using UnityEngine;
using System.Collections;
using System.Text;

public class OpeningTypeWriter : MonoBehaviour {

	public GameObject labelObject;
	public string text;
	public float interval;
	private char[] mCharAraay;
	private int mIndex;
	private StringBuilder mStringBuilder;

	void Awake(){
		mCharAraay = text.ToCharArray();
		mStringBuilder = new StringBuilder();
	}

	// Use this for initialization
	IEnumerator Start () {
		if(mIndex >= mCharAraay.Length){
			yield return new WaitForSeconds(3.0f);
			int isTutorialFinished = PrefsManager.getInstance ().isTutorialFinished ();
			if (isTutorialFinished == PrefsManager.TUTORIAL_FINISHED) {
				Application.LoadLevel ("Main");
			} else {
				Application.LoadLevel ("Tutorial");
			}
		}
		yield return new WaitForSeconds(interval);
		mStringBuilder.Append(mCharAraay[mIndex]);
		labelObject.GetComponent<UILabel>().text = mStringBuilder.ToString();
		mIndex++;
		StartCoroutine(Start());
	}
	

}
