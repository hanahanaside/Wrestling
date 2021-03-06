﻿using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	
	public GameObject uiRoot;
	public BackGroundKeeper backGroundKeeper;
	private tk2dSpriteAnimator spriteAnimator;
	private tk2dSprite mTk2dSprite;
	private int evolutionPoint = 0;
	private Mover mover;
	private bool isEvolution = false;
	private GameObject animationObject;
	private AudioClip voiceAudioClip;
	private bool isAnimation = false;

	public void PlayVoce ()
	{
		if(isAnimation){
			return;
		}
		if (PrefsManager.getInstance ().GetSoundMode () == PrefsManager.SOUND_ON) {
			Debug.Log("play");
			audio.PlayOneShot (voiceAudioClip);
			isAnimation = true;
			StartCoroutine(CompleteVoice());
		}
	}

	private IEnumerator CompleteVoice(){
		yield return new WaitForSeconds(2.0f);
		isAnimation = false;
	}

	void OnEnable ()
	{
		AnimationListener.AnimationFinishedEvent += OnAnimationFinished;
	}

	void OnDisable ()
	{
		AnimationListener.AnimationFinishedEvent -= OnAnimationFinished;
	}

	// Use this for initialization
	void Start ()
	{
		spriteAnimator = GetComponent<tk2dSpriteAnimator> ();
		mTk2dSprite = GetComponent<tk2dSprite> ();
		mover = GetComponent<Mover> ();
		setEvolutionPoint ();
		backGroundKeeper.UpdateBackGround (evolutionPoint);
		SetVoiceAudioClip ();
		SetAnimationObject ();
		animationObject.SetActive (false);
		StartIdleAnimation ();
	}
	
	void OnAnimationFinished (Transform player)
	{
		isAnimation = false;
		gameObject.transform.position = player.position;
		renderer.enabled = true;
		animationObject.SetActive (false);
		if (isEvolution) {
			mover.StopWalking ();
			transform.localPosition = new Vector3 (0, 0);
			mTk2dSprite.SetSprite ("player" + evolutionPoint + "_a");
		}
	}

	public void Atack (Transform target)
	{
		if(isAnimation){
			return;
		}
		GameObject.Find ("StatusBoard").SendMessage ("updateEXPPoint", target.gameObject.tag);
		isAnimation = true;
		renderer.enabled = false;
		animationObject.SetActive (true);
		animationObject.BroadcastMessage ("StartAnimation", target);
	}
	
	void StartEvolution ()
	{
		Debug.Log ("StartEvolution");
		isEvolution = true;
		uiRoot.SetActive (false);
		gameObject.tag = "Untouch";
		GameObject.Find ("BackGround").GetComponent<MeshRenderer> ().enabled = false;
		mover.StopWalking ();

		spriteAnimator.Stop ();
		GameObject.Find ("BGM").audio.Stop ();
		GameObject[] galArray = GameObject.FindGameObjectsWithTag ("Gal");
		foreach (GameObject galObject in galArray) {
			galObject.tag = "UntouchGal";
			galObject.GetComponent<MeshRenderer> ().enabled = false;
		}
		
		GameObject[] kyabaArray = GameObject.FindGameObjectsWithTag ("Kyaba");
		foreach (GameObject kyabaObject in kyabaArray) {
			kyabaObject.tag = "UntouchKyaba";
			kyabaObject.GetComponent<MeshRenderer> ().enabled = false;
		}
		StartCoroutine ("Evolution");
	}

	IEnumerator Evolution ()
	{
		yield return new WaitForSeconds (4);
		GameObject.Find ("Evolution").audio.Play ();
		for (int i = 0; i<5; i++) {
			renderer.enabled = false;
			yield return new WaitForSeconds (0.82f);
			renderer.enabled = true;
			yield return new WaitForSeconds (0.82f);
		}
		Debug.Log("step2");
		for (int i = 0; i<8; i++) {
			renderer.enabled = false;
			yield return new WaitForSeconds (0.1f);
			renderer.enabled = true;
			yield return new WaitForSeconds (0.1f);
		}
		Debug.Log("step3");
		int loopCount = 0;
#if UNITY_IPHONE
		loopCount = 32;
#endif
#if UNITY_ANDROID
		loopCount = 70;
#endif
		for (int i = 0; i<loopCount; i++) {
			renderer.enabled = false;
			yield return new WaitForSeconds (0.01f);
			renderer.enabled = true;
			yield return new WaitForSeconds (0.01f);
		}

		if (evolutionPoint >= 23) {
			mTk2dSprite.SetSprite ("player" + (evolutionPoint + 1) + "_a");
			yield return new WaitForSeconds (6.0f);
			Application.LoadLevel ("Ending");
		} else {
			PlayerDataDao.getInstance ().UpdateEvolutionPoint (evolutionPoint + 1);
			setEvolutionPoint ();
			backGroundKeeper.UpdateBackGround (evolutionPoint);
			SetVoiceAudioClip ();
			SetAnimationObject ();
			animationObject.SetActive (false);
			GameObject.Find ("BackGround").GetComponent<MeshRenderer> ().enabled = true;
			StartIdleAnimation ();
			yield return new WaitForSeconds (6.0f);
			uiRoot.SetActive (true);
			GameObject.Find ("StatusBoard").SendMessage ("FinishEvolution", evolutionPoint);
			isEvolution = false;
			GameObject.Find ("BGM").audio.Play ();
			GameObject[] unTouchGalArray = GameObject.FindGameObjectsWithTag ("UntouchGal");
			foreach (GameObject galObject in unTouchGalArray) {
				galObject.tag = "Gal";
				galObject.GetComponent<MeshRenderer> ().enabled = true;
			}
			GameObject[] unTouchKyabaArray = GameObject.FindGameObjectsWithTag ("UntouchKyaba");
			foreach (GameObject kyabaObject in unTouchKyabaArray) {
				kyabaObject.tag = "Kyaba";
				kyabaObject.GetComponent<MeshRenderer> ().enabled = true;
			}
			gameObject.tag = "Player";
			mover.StartWalking ();
		}
	}

	private void setEvolutionPoint ()
	{
		Hashtable playerData = PlayerDataDao.getInstance ().getPlayerData ();
		evolutionPoint = (int)playerData [PlayerDataDao.EVOLUTION_POINT_FIELD];
	}

	private void StartIdleAnimation ()
	{
		spriteAnimator.Play ("Idle" + evolutionPoint);
	}

	private void SetAnimationObject ()
	{
		string path = "Prefabs/PlayerAtackAnimation_" + evolutionPoint;
		animationObject = Instantiate (Resources.Load (path)) as GameObject;
	}

	private void SetVoiceAudioClip ()
	{
		string path = "Audios/b"+(evolutionPoint -1);
		voiceAudioClip = (AudioClip)Resources.Load (path);
	}
}
