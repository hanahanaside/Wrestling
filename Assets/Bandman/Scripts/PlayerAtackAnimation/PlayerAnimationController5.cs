using UnityEngine;
using System.Collections;

public class PlayerAnimationController5 : AbstractAnimationController
{

	public GameObject player;
	public GameObject enemy;
	public GameObject message;
	public Transform[] messagePositionArray;
	public float messageInterval;
	private int index = 0;
	private bool isPlaying = false;

	public override void StartAnimation (Transform target)
	{
		if(isPlaying){
			StopCoroutine("AnimationMessage");
		}
		isPlaying = true;
		base.StartAnimation (target);
		enemy.transform.position = target.position;
		player.transform.position = target.position;
		index = 0;
		MoveMessage();
		Destroy (target.gameObject);
		PlayAnimation (enemy);
		StartCoroutine("AnimationMessage");
	}
	
	public override void CompleteAnimation ()
	{
		isPlaying = false;
		AnimationListener.AnimationFinished (player.transform);
	}

	private IEnumerator AnimationMessage(){
		yield return new WaitForSeconds(messageInterval);
		index++;
		MoveMessage();
		if(index < messagePositionArray.Length-1){
			StartCoroutine("AnimationMessage");
		}else {
			yield return new WaitForSeconds(2.5f);
			CompleteAnimation();
		}

	}
	
	private void MoveMessage(){
		message.transform.position = messagePositionArray[index].position;
	}
}
