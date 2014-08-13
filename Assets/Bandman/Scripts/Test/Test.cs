using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour {

	public GameObject youTubeViewPrefab;
	public GameObject uiRoot;
	
	public void OnButtonClick(){
		Debug.Log("ass");
		GameObject youTubeViewObject = Instantiate(youTubeViewPrefab) as GameObject;
		youTubeViewObject.transform.parent = uiRoot.transform;
		youTubeViewObject.transform.localPosition = new Vector3 (0, 0, 0);
		youTubeViewObject.transform.localScale = new Vector3 (1, 1, 1);
		youTubeViewObject.BroadcastMessage("Show",1);
	}


}
