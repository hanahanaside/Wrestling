using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class HarajukuGalGenerator : MonoBehaviour
{
	public float interval;
	public int maxGalSize;
	public NotificationSender notificationSender;
	private List<GameObject> myList = new List<GameObject> ();

	// Use this for initialization
	void Start ()
	{
		for (int i = 0; i<maxGalSize; i++) {
			GameObject gal = GameObject.Find ("HarajukuGal" + i);
			myList.Add (gal);
		}

		for (int i = 0; i<myList.Count; i++) {
			myList [i].SetActive (false);
		}

		init ();
				
		StartCoroutine ("StartGenerating");
	}

	public void Pause ()
	{
		string exitTime = DateTime.Now.ToString ();
		PrefsManager.getInstance ().SaveExitTime (exitTime);
		Debug.Log ("exitTime = " + exitTime);
		int currentGalSize = GameObject.FindGameObjectsWithTag ("Gal").Length;
		PlayerDataDao.getInstance ().UpdateHarajukuGalSize (currentGalSize);
		//最大値で終了するとスグに通知が出るのを避ける 
		if (currentGalSize != maxGalSize) {
			int galDifference = maxGalSize - currentGalSize;
			double addSeconds = galDifference * 9;
			Debug.Log ("addSeconds = " + addSeconds);
			notificationSender.ScheduleLocalNotification (addSeconds);
		}
	}

	public void init ()
	{
		Hashtable playerData = PlayerDataDao.getInstance ().getPlayerData ();
		int harajukuGalSize = (int)playerData [PlayerDataDao.HARAJUKU_GAL_SIZE];
		string exitTime = PrefsManager.getInstance ().GetExitTime ();
		
		Debug.Log ("exitTime = " + exitTime);
		if (exitTime != "") {
			DateTime exitDateTime = DateTime.Parse (exitTime);
			DateTime restartDateTime = DateTime.Now;
			Debug.Log ("restartDateTime = " + restartDateTime);

			double sleepTime = (restartDateTime - exitDateTime).TotalSeconds;
			harajukuGalSize += (int)(sleepTime / interval);
			Debug.Log ("sleepTime = " + sleepTime);
		}
		
		
		for (int i =0; i<harajukuGalSize; i++) {
			GameObject gameObject = myList [i];
			gameObject.SetActive (true);
		}
	}

	private IEnumerator StartGenerating ()
	{
		yield return new WaitForSeconds (interval);
		Generate ();
		StartCoroutine ("StartGenerating");
	}

	private void Generate ()
	{
		foreach (GameObject gameObject in myList) {
			if (!gameObject.activeSelf) {
				gameObject.SetActive (true);
				break;
			}
		}
	}

}
