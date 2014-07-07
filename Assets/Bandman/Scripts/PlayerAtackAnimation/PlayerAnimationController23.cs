using UnityEngine;
using System.Collections;

public class PlayerAnimationController23 : AbstractAnimationController {

	public GameObject player;
	public GameObject message;
	private int completeCount;
	
	public override void StartAnimation (Transform target)
	{
		base.StartAnimation (target);
		completeCount = 0;
		player.transform.position = target.position;
		Destroy (target.gameObject);
		PlayAnimation(player);
		PlayAnimation(message);
	}
	
	public override void CompleteAnimation ()
	{
		completeCount++;
		if (completeCount % 6 == 0) {
			AnimationListener.AnimationFinished(player.transform);
		}
	}
}
