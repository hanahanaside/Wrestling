using UnityEngine;
using System.Runtime.InteropServices;

public class APUnityPlugin {
	
/* 
 * Wall yes
 */
	[DllImport("__Internal")]
	private static extern void showAppliPromotionWall_(string orientation , bool isClose, bool onStatusArea);
	public static void ShowAppliPromotionWall(string orientation="" ,bool isClose = false, bool onStatusArea = false) {
		if (Application.platform != RuntimePlatform.OSXEditor) {
			Debug.Log("APUnityPlugin.ShowAppliPromotionWall");
			showAppliPromotionWall_(orientation, isClose, onStatusArea);
		}
	}
	
	[DllImport("__Internal")]
	private static extern void sendUUID_();
	public static void SendUUID() {
		if (Application.platform != RuntimePlatform.OSXEditor) {
			sendUUID_();
		}
	}

	[DllImport("__Internal")]
	private static extern void sendTriggerID_(string triggerId, string callback);
	public static void sendTriggerID(string triggerId = "", string callback = "") {
		if (Application.platform != RuntimePlatform.OSXEditor) {
			sendTriggerID_(triggerId, callback);
		}
	}

	[DllImport("__Internal")]
	private static extern void pushTrigger_(string orientation, string triggerId, bool onStatusArea);
	public static void pushTrigger(string orientation="", string triggerId = "", bool onStatusArea = false) {
		if (Application.platform != RuntimePlatform.OSXEditor) {
			pushTrigger_(orientation, triggerId, onStatusArea);
		}
	}

	[DllImport("__Internal")]
	private static extern void popupDisp_(string orientation, string triggerId, bool onStatusArea, string callback);
	public static void popupDisp(string orientation="", string triggerId = "", bool onStatusArea = false, string callback = "") {
		if (Application.platform != RuntimePlatform.OSXEditor) {
			popupDisp_(orientation, triggerId, onStatusArea, callback);
		}
	}
	
	[DllImport("__Internal")]
	private static extern bool isFirstTimeInToday_();
	public static bool IsFirstTimeInToday() {
		if (Application.platform != RuntimePlatform.OSXEditor) {
			return isFirstTimeInToday_();
		}
		return false;
	}
}
