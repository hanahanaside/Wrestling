using UnityEngine;
using System.Collections;
using AppDriver.Android;

public class Test : MonoBehaviour {

	private int siteId = 15483;
	private string siteKey = "c0aa7a91246177f405056e78a765a235";

	void Start () {
		Debug.Log ("Start");
		AppDriverTracker.requestAppDriver (siteId, siteKey);
	}

	public void OnButtonClick () {
		Hashtable ht = new Hashtable ();
		ht.Add (AppDriverTracker.MEDIA_ID, 3437);
		ht.Add (AppDriverTracker.IDENTIFIER, "test");
		AppDriverTracker.openOfferwall (ht);
	}

}
