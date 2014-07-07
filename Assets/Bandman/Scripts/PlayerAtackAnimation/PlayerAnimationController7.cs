using UnityEngine;
using System.Collections;

public class PlayerAnimationController7 : AbstractAnimationController {

	public GameObject player;
	public GameObject message;
	private int completeCount;
	private Vector3 startMessagePosition;
	
	void Awake(){
		startMessagePosition = message.transform.position;
	}
	
	public override void StartAnimation (Transform target)
	{
		base.StartAnimation (target);
		completeCount = 0;
		message.transform.position = startMessagePosition;
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
