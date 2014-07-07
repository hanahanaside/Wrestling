using UnityEngine;
using System.IO;
using System.Net;
using MiniJSON;
using System.Collections;

public class RecommendAppDialog : AlertDialog{

	private const string PUBLICITIES_URL = "http://push.tt5.us/showed_publicities/1.json";
	private long mPublicityId;
	private string mPublicityUrl;


	public override void alertButtonClicked (string text)
	{
		#if UNITY_IPHONE
		if (text == "\u30a4\u30f3\u30b9\u30c8\u30fc\u30eb") {
			StartCoroutine (SendInstallCount ());
			Application.OpenURL (mPublicityUrl);
		}
		#endif

		Destroy(gameObject);
	}

	public override void Show(){
		#if UNITY_IPHONE
		StartCoroutine(GetShowedPublicitiesData());
		#endif
	}
	#if UNITY_IPHONE
	private IEnumerator GetShowedPublicitiesData ()
	{
		WWW www = new WWW (PUBLICITIES_URL);
		yield return www;
		
		// check for errors
		if (www.error == null) {
			Debug.Log ("WWW Ok!: " + www.text);
			DeserializeData(www);
		} else {
			Debug.Log ("WWW Error: " + www.error);
		}
	}

	private void DeserializeData(WWW www){
		string json = www.text;
		IDictionary jsonObject = (IDictionary)Json.Deserialize (json);
		mPublicityId = (long)jsonObject ["publicity_id"];
		mPublicityUrl = (string)jsonObject ["url"];
		if(mPublicityId != 0){
			string message = (string)jsonObject ["message"];
			ShowRecommendDialog(message);
		}
	}

	private void ShowRecommendDialog(string message){
		string title = "\u30aa\u30b9\u30b9\u30e1\u30a2\u30d7\u30ea"; 
		string[] buttons = new string[]{"\u30ad\u30e3\u30f3\u30bb\u30eb","\u30a4\u30f3\u30b9\u30c8\u30fc\u30eb"};
		EtceteraBinding.showAlertWithTitleMessageAndButtons(title,message,buttons);
	}


	private IEnumerator SendInstallCount ()
	{
		Debug.Log("SendInstallCount");
		string url = "http://push.tt5.us/publicities/" + mPublicityId + "/installed_count.json?direction=up";
		WebRequest webRequest = WebRequest.Create (url);
		webRequest.Method = "PUT";
		Stream requestStream = webRequest.GetRequestStream ();
		requestStream.Flush ();
		requestStream.Close ();
		WebResponse response = webRequest.GetResponse ();
		Stream responseStream = response.GetResponseStream ();
		StreamReader streamReader = new StreamReader (responseStream);
		
		Debug.Log ("response = " + streamReader.ReadToEnd ());
		yield return true;
	}
	#endif

}
