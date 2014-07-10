using UnityEngine;
using System.Collections;

public class PlayerAnimationController10 :AbstractAnimationController
{

	public GameObject player;
	public GameObject enemy;
	public GameObject message;
	
	public override void StartAnimation (Transform target)
	{
		base.StartAnimation (target);
		enemy.transform.position = target.position;
		player.transform.position = target.position;
		player.transform.Translate(0.5f,0.5f,0);
		player.transform.localRotation = Quaternion.Euler (0, 0, 0);
		message.transform.localScale = new Vector3(1,1,1);
		Destroy (target.gameObject);
		PlayAnimation (player);
		PlayAnimation (message);
		StartCoroutine(WaitForComplete());
	}
	
	public override void CompleteAnimation ()
	{

			AnimationListener.AnimationFinished (player.transform);

	}
}
