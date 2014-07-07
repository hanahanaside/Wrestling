﻿using UnityEngine;
using System.Collections;

public class HarajukuGal : MonoBehaviour
{

	private Vector3 startPosition;
	private iTweenEvent idleEvent;

	void Start ()
	{ 
		startPosition = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
		idleEvent = iTweenEvent.GetEvent (gameObject, "idle");
	}

	void StartExitAnimation ()
	{
		idleEvent.Stop ();
		iTweenEvent exitEvent = iTweenEvent.GetEvent (gameObject, "exit");
		exitEvent.Start ();
	}

	void OnExitAnimationFinished ()
	{
		GameObject.Find ("Pon").audio.Play ();
		Hashtable playerData = PlayerDataDao.getInstance().getPlayerData();
		int mainGalSize = (int)playerData[PlayerDataDao.MAIN_GAL_SIZE];
		if (mainGalSize < 0) {
			mainGalSize = 0;
		}
		mainGalSize++;
		PlayerDataDao.getInstance ().UpdateMainGalSize (mainGalSize);
		transform.position = startPosition;
		idleEvent.Start ();
		gameObject.SetActive (false);
	}
}