using UnityEngine;
using System.Collections;

public class PlayerAnimationController2 : AbstractAnimationController
{

	public GameObject player;
	public GameObject message;
	private Vector3 startMessagePosition;
	
	void Awake ()
	{
		startMessagePosition = message.transform.position;
	}
	
	public override void StartAnimation (Transform target)
	{
		base.StartAnimation (target);
		message.transform.position = startMessagePosition;
		player.transform.position = target.position;
		Destroy (target.gameObject);
		PlayAnimation (player);
		PlayAnimation (message);
		StartCoroutine (WaitForComplete ());
	}
	
	public override void CompleteAnimation ()
	{

		AnimationListener.AnimationFinished (player.transform);

	}
}
