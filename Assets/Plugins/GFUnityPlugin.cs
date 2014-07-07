using UnityEngine;
using System.Runtime.InteropServices;

public class GFUnityPulugin { 
	
	// GFView.activate
	[DllImport("__Internal")]
	private static extern void activateGF_(string siteId, bool useCustom, bool useIcon, bool usePopup);
	public static void activate(string siteId, bool useCustom, bool useIcon, bool usePopup) {
		if (Application.platform != RuntimePlatform.OSXEditor) {
			activateGF_(siteId, useCustom, useIcon, usePopup);
		}
	}	

	// GFView.start
	[DllImport("__Internal")]
	private static extern void start_(string siteId);
	public static void start(string siteId) {
		if (Application.platform != RuntimePlatform.OSXEditor) {
			start_(siteId);
		}
	}

	// GFView.start delegate
	[DllImport("__Internal")]
	private static extern void startWtCallback_(string siteId, string objName);
	public static void startWtCallback(string siteId, string objName) {
		if (Application.platform != RuntimePlatform.OSXEditor) {
			startWtCallback_(siteId, objName);
		}
	}

	// GFView.conversionCheck
	[DllImport("__Internal")]
	private static extern bool conversionCheck_();
	public static bool conversionCheck() {
		if (Application.platform != RuntimePlatform.OSXEditor) {
			return conversionCheck_();
		}
		else {
			return false;
		}
	}

	// GFController.showGF
	[DllImport("__Internal")]
	private static extern void show_(string siteId);
	public static void show(string siteId) {
		if (Application.platform != RuntimePlatform.OSXEditor) {
			show_(siteId);
		}
	}

	// GFController.showGF delegate
	[DllImport("__Internal")]
	private static extern void showWtCallback_(string siteId, string objName);
	public static void showWtCallback(string siteId, string objName) {
		if (Application.platform != RuntimePlatform.OSXEditor) {
			showWtCallback_(siteId, objName);
		}
	}

	// GFController.backgroundTask
	[DllImport("__Internal")]
	private static extern void backgroundTask_();
	public static void backgroundTask() {
		if (Application.platform != RuntimePlatform.OSXEditor) {
			backgroundTask_();
		}
	}

	// GFController.conversionCheckStop
	[DllImport("__Internal")]
	private static extern void conversionCheckStop_();
	public static void conversionCheckStop() {
		if (Application.platform != RuntimePlatform.OSXEditor) {
			conversionCheckStop_();
		}
	}
	
	// GF IconController.init
	[DllImport("__Internal")]
	private static extern void initGFIconController_();
	public static void initGFIconController() {
		if (Application.platform != RuntimePlatform.OSXEditor) {
			initGFIconController_();
		}
	}
	
	// GF IconController.addIconView
	[DllImport("__Internal")]
	private static extern void addIconView_(int x, int y, int w, int h);
	public static void addIconView(int x, int y, int w, int h) {
		if (Application.platform != RuntimePlatform.OSXEditor) {
			addIconView_(x, y, w, h);
		}
	}
	
	// GF IconController.iconLoadAd
	[DllImport("__Internal")]
	private static extern void iconLoadAd_(string siteId);
	public static void iconLoadAd(string siteId) {
		if (Application.platform != RuntimePlatform.OSXEditor) {
			iconLoadAd_(siteId);
		}
	}
	
	// GF IconController.iconStopAd
	[DllImport("__Internal")]
	private static extern void iconStopAd_();
	public static void iconStopAd() {
		if (Application.platform != RuntimePlatform.OSXEditor) {
			iconStopAd_();
		}
	}
	
	// GF IconController.setIconAppNameColor
	[DllImport("__Internal")]
	private static extern void setIconAppNameColor_(float r, float g, float b);
	public static void setIconAppNameColor(float r, float g, float b) {
		if (Application.platform != RuntimePlatform.OSXEditor) {
			setIconAppNameColor_(r, g, b);
		}
	}
	
	// GF IconController.setIconAdRefreshTiming
	[DllImport("__Internal")]
	private static extern void setIconAdRefreshTiming_(int sec);
	public static void setIconAdRefreshTiming(int sec) {
		if (Application.platform != RuntimePlatform.OSXEditor) {
			setIconAdRefreshTiming_(sec);
		}
	}
	
	// GF IconController.removeIconAd
	[DllImport("__Internal")]
	private static extern void removeIconAd_();
	public static void removeIconAd() {
		if (Application.platform != RuntimePlatform.OSXEditor) {
			removeIconAd_();
		}
	}
	
	// GF IconController.invisibleIconAd
	[DllImport("__Internal")]
	private static extern void invisibleIconAd_();
	public static void invisibleIconAd() {
		if (Application.platform != RuntimePlatform.OSXEditor) {
			invisibleIconAd_();
		}
	}	
	
	// GF IconController.visibleIconAd
	[DllImport("__Internal")]
	private static extern void visibleIconAd_();
	public static void visibleIconAd() {
		if (Application.platform != RuntimePlatform.OSXEditor) {
			visibleIconAd_();
		}
	}	
	
	// GF PopupView.initGFPopupView
	[DllImport("__Internal")]
	private static extern void initGFPopupView_();
	public static void initGFPopupView() {
		if (Application.platform != RuntimePlatform.OSXEditor) {
			initGFPopupView_();
		}
	}

	// GF PopupView.initGFPopupViewCallback
	[DllImport("__Internal")]
	private static extern void initGFPopupViewCallback_(string objName);
	public static void initGFPopupViewCallback(string objName) {
		if (Application.platform != RuntimePlatform.OSXEditor) {
			initGFPopupViewCallback_(objName);
		}
	}
	
	// GF PopupView.setPopupAdSchedule
	[DllImport("__Internal")]
	private static extern void setPopupAdSchedule_(int num);
	public static void setPopupAdSchedule(int num) {
		if (Application.platform != RuntimePlatform.OSXEditor) {
			setPopupAdSchedule_(num);
		}
	}
	
	// GF PopupView.setPopupAdAnimation
	[DllImport("__Internal")]
	private static extern void setPopupAdAnimation_(bool flag);
	public static void setPopupAdAnimation(bool flag) {
		if (Application.platform != RuntimePlatform.OSXEditor) {
			setPopupAdAnimation_(flag);
		}
	}
	
	// GF PopupView.popupLoadAd
	[DllImport("__Internal")]
	private static extern void popupLoadAd_(string siteId);
	public static void popupLoadAd(string siteId) {
		if (Application.platform != RuntimePlatform.OSXEditor) {
			popupLoadAd_(siteId);
		}
	}
}
