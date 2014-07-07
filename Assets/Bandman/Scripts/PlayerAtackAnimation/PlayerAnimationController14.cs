using UnityEngine;
using System.Collections;

public class PlayerAnimationController14 : AbstractAnimationController
{

	
	public GameObject player;
	public GameObject message;
	private int completeCount;

	public override void StartAnimation (Transform target)
	{
		base.StartAnimation (target);
		completeCount = 0;
		message.transform.localScale = new Vector3(1,1,1);
		player.transform.position = target.position;
		Destroy (target.gameObject);
		PlayAnimation (message);
		PlayAnimation(player);
	}
	
	public override void CompleteAnimation ()
	{
		completeCount++;
		if(completeCount % 5 == 0){
			AnimationListener.AnimationFinished (player.transform);
		}
	}
}
