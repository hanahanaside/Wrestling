using UnityEngine;
using System.Collections;

public class YouTubeViewController : MonoBehaviour {

	public YouTubeIdKeeper youTubeIdKeeper;
	private GameObject mBgm;

	void Start(){
		mBgm = GameObject.Find ("BGM");
		mBgm.audio.Stop();
	}

	public void OnCloseClick(){
		GameObject.Find("UIFence").SetActive(false);
		mBgm.audio.Play();
		EtceteraBinding.inlineWebViewClose();
		Destroy(gameObject.transform.parent.gameObject);
	}
	
	void Show(int evolutionPoint){
	//	string url =  "http://www.youtube.com/embed/"+youTubeIdKeeper.idArray[evolutionPoint-1]+"?playsinline=1";
		string url = "https://www.youtube.com/embed/GOiIxqcbzyM?feature=player_detailpage&playsinline=1";
#if UNITY_IPHONE
		EtceteraBinding.inlineWebViewShow( 10, 100, 300, 300 );
		EtceteraBinding.inlineWebViewSetUrl( url );
#endif
	}

}
