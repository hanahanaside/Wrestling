using UnityEngine;
using System.Collections;

public class ExitDialogController : MonoBehaviour
{
	
	void Start () {
		GameObject.Find ("NendAdRectangle").GetComponent<NendAdBanner> ().Show ();
	}

	public void OnExitClick () {
		
		Application.Quit ();
	}

	public void OnCloseClick () {
		
		GameObject.Find ("MainController").SetActive (true);
		Destroy (transform.parent.gameObject);
		GameObject.Find ("NendAdRectangle").GetComponent<NendAdBanner> ().Hide ();
	}

}
