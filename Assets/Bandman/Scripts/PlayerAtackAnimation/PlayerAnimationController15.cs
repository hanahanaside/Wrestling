using UnityEngine;
using System.Collections;

public class PlayerAnimationController15 : AbstractAnimationController {

	public GameObject player;
	public GameObject enemy;
	public GameObject message;
	private Vector3 startMessagePosition;
	private GameObject backGround;

	void Awake ()
	{
		startMessagePosition = message.transform.position;
		backGround = GameObject.Find("BackGround");
	}

	void CompleteEnemyAnimation(){
		Hashtable anim = GetBackGroundAnimation();
		iTween.MoveTo(backGround,anim);
	}
	
	public override void StartAnimation (Transform target)
	{
		base.StartAnimation (target);
		message.transform.position = startMessagePosition;
		enemy.transform.position = target.position;
		player.transform.position = target.position;
		player.transform.Translate(1.0f,0,0);
		player.transform.localRotation = Quaternion.Euler(0,0,0);
		enemy.transform.localScale = new Vector3(1,1,1);
		Destroy (target.gameObject);
		PlayAnimation(player);
		PlayAnimation (enemy);
		PlayAnimation (message);
		StartCoroutine(WaitForComplete());
	}
	
	public override void CompleteAnimation ()
	{
		iTween.Stop(backGround);
		iTween.Stop(enemy);
		backGround.transform.localPosition = new Vector3(0,0,0);
		AnimationListener.AnimationFinished (player.transform);
	}

	private Hashtable GetBackGroundAnimation(){
		Hashtable table = new Hashtable();
		table.Add("time", 0.1f);	
		table.Add("x", 0.5f);	
		table.Add("easeType", "EaseInBack");
		table.Add("loopType", "loop");
		return table;
	}
}
