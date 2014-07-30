using UnityEngine;
using System;
using System.Runtime.InteropServices;

internal static class IMobileSdkAdsViewUtility
{
    internal static int getLeft(IMobileSdkAdsUnityPlugin.AdAlignPosition alignPosition, IMobileSdkAdsUnityPlugin.AdType adType, IMobileIconParams iconParams){
        int screenWidth = getDensitySize (Screen.width);
        int leftPosition = 0;
        switch (alignPosition) {
        case IMobileSdkAdsUnityPlugin.AdAlignPosition.CENTER:
            leftPosition = screenWidth - getWidth(adType, iconParams);
            if(leftPosition != 0) {
                leftPosition = leftPosition / 2;
            }
            break;
        case IMobileSdkAdsUnityPlugin.AdAlignPosition.LEFT:
            leftPosition = 0;
            break;
        case IMobileSdkAdsUnityPlugin.AdAlignPosition.RIGHT:
            leftPosition = screenWidth - getWidth(adType, iconParams);
            break;
        }
        return leftPosition;
    }

    internal static int getTop(IMobileSdkAdsUnityPlugin.AdValignPosition valignPosition, IMobileSdkAdsUnityPlugin.AdType adType, IMobileIconParams iconParams){
        int screenHeight = getDensitySize (Screen.height);
        int topPosition = 0;
        switch (valignPosition) {
        case IMobileSdkAdsUnityPlugin.AdValignPosition.TOP:
            topPosition = 0;
            break;
        case IMobileSdkAdsUnityPlugin.AdValignPosition.MIDDLE:
            topPosition = (screenHeight / 2) - (getHeight(adType) / 2);
            break;
        case IMobileSdkAdsUnityPlugin.AdValignPosition.BOTTOM:
            topPosition = screenHeight - getHeight(adType);
            break;
        }
        return topPosition;
    }

    internal static int getWidth(IMobileSdkAdsUnityPlugin.AdType adType, IMobileIconParams iconParams){
        int width = -1;
        switch (adType) {
        case IMobileSdkAdsUnityPlugin.AdType.BANNER:
            width = 320;
            break;
        case IMobileSdkAdsUnityPlugin.AdType.BIG_BANNER:
            width = 320;
            break;
		case IMobileSdkAdsUnityPlugin.AdType.TABLET_BANNER:
			width = 468;
			break;
		case IMobileSdkAdsUnityPlugin.AdType.TABLET_BIG_BANNER:
			width = 728;
			break;
        case IMobileSdkAdsUnityPlugin.AdType.MEDIUM_RECTANGLE:
            width = 300;
            break;
		case IMobileSdkAdsUnityPlugin.AdType.BIG_RECTANGLE:
			width = 336;
			break;
		case IMobileSdkAdsUnityPlugin.AdType.SKYSCRAPER:
			width = 120;
			break;
		case IMobileSdkAdsUnityPlugin.AdType.WIDE_SKYSCRAPER:
			width = 160;
			break;
		case IMobileSdkAdsUnityPlugin.AdType.SQUARE:
			width = 250;
			break;
		case IMobileSdkAdsUnityPlugin.AdType.SMALL_SQUARE:
			width = 200;
			break;
		case IMobileSdkAdsUnityPlugin.AdType.HALFPAGE:
			width = 300;
			break;
        case IMobileSdkAdsUnityPlugin.AdType.ICON:
            width = getIconWidth(iconParams);
            break;
        }

        return width;
    }

    private static int getIconWidth(IMobileIconParams iconParams){
		int screenWidth = getDensitySize (Screen.width);
		int screenHeight = getDensitySize (Screen.height);
        int minWidth = screenWidth > screenHeight ? screenHeight : screenWidth;

        if (iconParams == null) {
            return minWidth;
        }

        if (iconParams.iconViewLayoutWidth > 0) {
            return iconParams.iconViewLayoutWidth;
        }

        if (iconParams.iconNumber > 0) {
            if (iconParams.iconNumber < 4) {
                return 75 * iconParams.iconNumber;
            } else {
                return minWidth;
            }
        }

        return minWidth;
    }

    internal static int getHeight(IMobileSdkAdsUnityPlugin.AdType adType){
        int height = -1;
        switch (adType) {
        case IMobileSdkAdsUnityPlugin.AdType.BANNER:
            height = 50;
            break;
        case IMobileSdkAdsUnityPlugin.AdType.BIG_BANNER:
            height = 100;
            break;
		case IMobileSdkAdsUnityPlugin.AdType.TABLET_BANNER:
			height = 60;
			break;
		case IMobileSdkAdsUnityPlugin.AdType.TABLET_BIG_BANNER:
			height = 90;
			break;
		case IMobileSdkAdsUnityPlugin.AdType.MEDIUM_RECTANGLE:
            height = 250;
            break;
		case IMobileSdkAdsUnityPlugin.AdType.BIG_RECTANGLE:
			height = 280;
			break;
		case IMobileSdkAdsUnityPlugin.AdType.SKYSCRAPER:
			height = 600;
			break;
		case IMobileSdkAdsUnityPlugin.AdType.WIDE_SKYSCRAPER:
			height = 600;
			break;
		case IMobileSdkAdsUnityPlugin.AdType.SQUARE:
			height = 250;
			break;
		case IMobileSdkAdsUnityPlugin.AdType.SMALL_SQUARE:
			height = 200;
			break;
		case IMobileSdkAdsUnityPlugin.AdType.HALFPAGE:
			height = 600;
			break;
        case IMobileSdkAdsUnityPlugin.AdType.ICON:
            height = 75;
            break;
        }

        return height;
    }

	#if UNITY_IPHONE
	[DllImport("__Internal")]
	private static extern float getScreenScale_();
	#endif

    private static int getDensitySize(int size) {
		#if UNITY_EDITOR
		return 0;
		#elif UNITY_IPHONE
        float screenScale = getScreenScale_();
        return (int)(size / screenScale + 0.5f);
        #elif UNITY_ANDROID
        AndroidJavaObject displayMetrics = new AndroidJavaObject ("android.util.DisplayMetrics");
        AndroidJavaClass unityPlayerClass = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
        AndroidJavaObject activityObject = unityPlayerClass.GetStatic<AndroidJavaObject> ("currentActivity");
        AndroidJavaObject windowManagerObject = activityObject.Call<AndroidJavaObject> ("getWindowManager");
        AndroidJavaObject displayObject = windowManagerObject.Call<AndroidJavaObject> ("getDefaultDisplay");
        displayObject.Call ("getMetrics", displayMetrics);
        float density = displayMetrics.Get<float> ("density");
        return (int)(((size / density) + 0.5f));
        #endif
    }
}

