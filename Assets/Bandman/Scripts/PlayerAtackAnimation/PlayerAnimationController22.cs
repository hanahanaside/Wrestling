using UnityEngine;
using System.Collections;

public class PlayerAnimationController22 : AbstractAnimationController {

	public GameObject player;
	public GameObject enemy;
	public GameObject message;
	private Vector3 startMessagePosition;
	private Vector3 startPlayerPosition;
	private GameObject backGround;
	
	void Awake ()
	{
		startMessagePosition = message.transform.position;
		startPlayerPosition = player.transform.position;
		backGround = GameObject.Find("BackGround");
		iTween.MoveTo(player, GetPlayerAnimation());
	}
	
	public override void StartAnimation (Transform target)
	{
		iTween.Stop(player);
		iTween.Stop(backGround);
		base.StartAnimation (target);
		message.transform.position = startMessagePosition;
		player.transform.position = startPlayerPosition;
		enemy.transform.position = target.position;
		Destroy (target.gameObject);
		iTween.MoveTo(player, GetPlayerAnimation());
		PlayAnimation (message);
		StartCoroutine("PlayBackGroundAnimation");
	}
	
	public override void CompleteAnimation ()
	{
		iTween.Stop(backGround);
		backGround.transform.localPosition = new Vector3(0,0,0);
		AnimationListener.AnimationFinished (player.transform);
	}
	
	private IEnumerator PlayBackGroundAnimation(){
		yield return new WaitForSeconds(1.0f);
		iTween.MoveTo(backGround,GetBackGroundAnimation());
	}
	
	private Hashtable GetPlayerAnimation() {
		Vector3[] movePath = new Vector3[2];
		movePath[0] = new Vector3(enemy.transform.position.x+1.0f,6.0f,0);
		movePath[1] = enemy.transform.position;
		Hashtable table = new Hashtable();
		table.Add("time", 3.0f);	
		table.Add("path",movePath);
		table.Add("easeType", "EaseOutBounce");
		table.Add("oncomplete", "CompleteAnimation");
		table.Add("oncompletetarget", gameObject);
		return table;
	}
	
	private Hashtable GetBackGroundAnimation(){
		Hashtable table = new Hashtable();
		table.Add("time", 0.1f);	
		table.Add("y", 1.0f);	
		table.Add("easeType", "linear");
		table.Add("loopType", "pingPong");
		return table;
	}
}
