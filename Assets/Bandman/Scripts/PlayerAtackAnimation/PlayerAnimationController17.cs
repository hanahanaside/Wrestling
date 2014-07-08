using UnityEngine;
using System.Collections;

public class PlayerAnimationController17 : AbstractAnimationController
{

	public GameObject player;
	public GameObject message;
	private GameObject backGround;
	private Vector3 startMessagePosition;
	
	void Awake ()
	{
		backGround = GameObject.Find ("BackGround");
		startMessagePosition = message.transform.position;
	}

	void CompletePlayerAnimation ()
	{
		iTween.MoveTo (backGround, GetBackGroundAnimation ());
		PlayAnimation (message);
	}

	public override void StartAnimation (Transform target)
	{
		iTween.Stop (backGround);
		backGround.transform.localPosition = new Vector3 (0, 0, 0);
		base.StartAnimation (target);
		message.transform.position = startMessagePosition;
		player.transform.position = target.transform.position;
		player.transform.localRotation = Quaternion.Euler (0, 0, 0);
		Destroy (target.gameObject);
		PlayAnimation (player);
		StartCoroutine (WaitForComplete ());
	}
	
	public override void CompleteAnimation ()
	{
		iTween.Stop (backGround);
		backGround.transform.localPosition = new Vector3 (0, 0, 0);
		AnimationListener.AnimationFinished (player.transform);

	}
	
	private Hashtable GetBackGroundAnimation ()
	{
		Hashtable table = new Hashtable ();
		table.Add ("time", 0.1f);	
		table.Add ("y", 1.0f);	
		table.Add ("easeType", "linear");
		table.Add ("loopType", "pingPong");
		return table;
	}

}
