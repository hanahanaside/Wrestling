using UnityEngine;
using System.Collections;
using System.Text;

public class EndingTypeWriter : MonoBehaviour {

	public GameObject labelObject;
	public ResetDialog resetDialog;
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
			yield return new WaitForSeconds (6);
			resetDialog.Show();
		}
		yield return new WaitForSeconds(interval);
		mStringBuilder.Append(mCharAraay[mIndex]);
		labelObject.GetComponent<UILabel>().text = mStringBuilder.ToString();
		mIndex++;
		StartCoroutine(Start());
	}
}
