using UnityEngine;
using System.Collections;

public class PlayerAnimationController18 : AbstractAnimationController
{

	public GameObject player;
	public GameObject enemy;
	public GameObject message;
	private int completeCount;
	
	public override void StartAnimation (Transform target)
	{
		completeCount = 0;
		base.StartAnimation (target);
		enemy.transform.position = target.position;
		player.transform.position = target.position;
		player.transform.Translate (0.5f, 0.5f, 0);
		player.transform.localRotation = Quaternion.Euler (0, 0, 0);
		message.transform.localScale = new Vector3 (1, 1, 1);
		Destroy (target.gameObject);
		PlayAnimation (player);
		PlayAnimation (message);
	}
	
	public override void CompleteAnimation ()
	{
		completeCount++;
		if (completeCount % 5 == 0) {
			AnimationListener.AnimationFinished (player.transform);
		}
	}
}
