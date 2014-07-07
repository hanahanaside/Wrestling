using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour {
	
	public GameObject a;
	public Transform target;

	void Start(){
		a.BroadcastMessage("StartAnimation",target);
	}

	void OnAnimationFinished(Transform target){
		Debug.Log("finish");
	}
}
