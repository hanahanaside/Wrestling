using UnityEngine;
using System.Collections;

public class InnerFence : MonoBehaviour {


	// Use this for initialization
	void Start () {
		GameObject uiRoot = GameObject.Find("UI Root");
		transform.parent = uiRoot.transform;
		transform.localScale = new Vector3(1,1,1);
	}

}
