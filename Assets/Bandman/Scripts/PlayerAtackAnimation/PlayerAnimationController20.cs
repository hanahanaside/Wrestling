using UnityEngine;
using System.Collections;

public class PlayerAnimationController20 : AbstractAnimationController {

	public GameObject player;
	public GameObject enemy;
	public GameObject message;
	private Vector3 startMessagePosition;
	private int completeCount;
	
	void Awake(){
		startMessagePosition = message.transform.position;
	}
	
	public override void StartAnimation (Transform target)
	{
		completeCount = 0;
		base.StartAnimation(target);
		message.transform.position = startMessagePosition;
		enemy.transform.position = target.position;
		player.transform.position = target.position;
		player.transform.Translate(-1.0f,0,0);
		Destroy(target.gameObject);
		PlayAnimation(enemy);
		PlayAnimation(message);
	}
	
	public override void CompleteAnimation(){
		completeCount++;
		if(completeCount % 10==0){
			AnimationListener.AnimationFinished(player.transform);
		}
	}
}
