using UnityEngine;
using System.Collections;

public class EndingController : MonoBehaviour
{

	// Use this for initialization
	void Start () {
		GameObject.Find ("NendAdBanner").GetComponent<NendAdBanner> ().Hide ();
	}

}
