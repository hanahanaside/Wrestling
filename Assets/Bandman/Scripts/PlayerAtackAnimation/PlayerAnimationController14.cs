using UnityEngine;
using System.Collections;

public class PlayerAnimationController14 : AbstractAnimationController
{

	
	public GameObject player;
	public GameObject message;

	public override void StartAnimation (Transform target)
	{
		base.StartAnimation (target);

		message.transform.localScale = new Vector3(1,1,1);
		player.transform.position = target.position;
		Destroy (target.gameObject);
		PlayAnimation (message);
		PlayAnimation(player);
		StartCoroutine(WaitForComplete());
	}
	
	public override void CompleteAnimation ()
	{

			AnimationListener.AnimationFinished (player.transform);

	}
}
