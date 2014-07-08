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

	public override void StartAnimation (Transform target)
	{
		base.StartAnimation (target);
		enemy.transform.position = target.position;
		player.transform.position = target.position;
		index = 0;
		MoveMessage();
		Destroy (target.gameObject);
		PlayAnimation (enemy);
		StartCoroutine("AnimationMessage");
	}


	private IEnumerator AnimationMessage(){
		yield return new WaitForSeconds(messageInterval);
		index++;
		MoveMessage();
		if(index < messagePositionArray.Length-1){
			StartCoroutine("AnimationMessage");
		}else {
			yield return new WaitForSeconds(2.0f);
			StartCoroutine(WaitForComplete(player.transform));
		}

	}
	
	private void MoveMessage(){
		message.transform.position = messagePositionArray[index].position;
	}
}
