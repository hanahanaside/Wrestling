using UnityEngine;
using System.Collections;

public class PlayerAnimationController23 : AbstractAnimationController {

	public GameObject player;
	public GameObject message;

	
	public override void StartAnimation (Transform target)
	{
		base.StartAnimation (target);

		player.transform.position = target.position;
		Destroy (target.gameObject);
		PlayAnimation(player);
		PlayAnimation(message);
		StartCoroutine(WaitForComplete());
	}
	
	public override void CompleteAnimation ()
	{
			AnimationListener.AnimationFinished(player.transform);

	}
}
