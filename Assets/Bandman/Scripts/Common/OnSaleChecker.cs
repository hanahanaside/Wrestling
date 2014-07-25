using UnityEngine;
using System;
using System.Collections;

public class OnSaleChecker : MonoBehaviour {

	public static bool CheckOnSale(){
	//	DateTime releaseDateTime =  DateTime.Parse("2014/8/1");
		DateTime releaseDateTime =  DateTime.Parse("2014/8/2");
		TimeSpan timeSpan = DateTime.Now - releaseDateTime;
		if(timeSpan.Seconds >0){
			return true;
		}else {
			return false;
		}
	}
}
