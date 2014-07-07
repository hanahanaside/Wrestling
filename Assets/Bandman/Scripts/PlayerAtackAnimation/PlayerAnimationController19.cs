using UnityEngine;
using System.Collections;

public class PlayerAnimationController19 : AbstractAnimationController{

	public GameObject player;
	public GameObject enemy;
	public GameObject enemyParent;
	public GameObject message;
	private Vector3 startMessagePosition;
	
	void Awake(){
		startMessagePosition = message.transform.position;
	}
	
	
	public override void StartAnimation (Transform target)
	{
		base.StartAnimation (target);
		message.transform.position = startMessagePosition;
		enemy.transform.position = target.position;
		player.transform.position = target.position;
		Destroy (target.gameObject);
		PlayAnimation(enemyParent);
		PlayAnimation (enemy);
		PlayAnimation (message);
	}
	
	public override void CompleteAnimation ()
	{
		AnimationListener.AnimationFinished (player.transform);
	}

}
