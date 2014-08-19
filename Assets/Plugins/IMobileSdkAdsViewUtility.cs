using UnityEngine;
using System;
using System.Runtime.InteropServices;

internal static class IMobileSdkAdsViewUtility
{

	/// <summary>
	/// 広告の表示領域を返します
	/// </summary>
	/// <returns>広告の表示領域</returns>
	/// <param name="alignPosition">AdAlignPosition</param>
	/// <param name="valignPosition">AdValignPosition</param>
	/// <param name="adType">AdType</param>
	/// <param name="iconParams">IMobileIconParams</param>
	internal static Rect getAdRect(IMobileSdkAdsUnityPlugin.AdAlignPosition alignPosition, IMobileSdkAdsUnityPlugin.AdValignPosition valignPosition, IMobileSdkAdsUnityPlugin.AdType adType, IMobileIconParams iconParams){
		int left = getLeft (alignPosition, adType, iconParams);
		int top = getTop (valignPosition, adType, iconParams);
		return getAdRect (left, top, adType, iconParams);
	}

	/// <summary>
	/// 広告の表示領域を返します
	/// </summary>
	/// <returns>広告の表示領域</returns>
	/// <param name="left">Left</param>
	/// <param name="top">Top</param>
	/// <param name="adType">AdType</param>
	/// <param name="iconParams">IMobileIconParams</param>
	internal static Rect getAdRect(int left, int top, IMobileSdkAdsUnityPlugin.AdType adType, IMobileIconParams iconParams){
		iconParams = iconParams ?? new IMobileIconParams ();
		Rect adRect = new Rect ();
		adRect.width = getWidth (adType, iconParams);
		adRect.height = getHeight (adType, iconParams);
		adRect.x = left;
		adRect.y = top;
		return adRect;
	}
	
    private static int getLeft(IMobileSdkAdsUnityPlugin.AdAlignPosition alignPosition, IMobileSdkAdsUnityPlugin.AdType adType, IMobileIconParams iconParams){
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

    private static int getTop(IMobileSdkAdsUnityPlugin.AdValignPosition valignPosition, IMobileSdkAdsUnityPlugin.AdType adType, IMobileIconParams iconParams){
        int screenHeight = getDensitySize (Screen.height);
        int topPosition = 0;
        switch (valignPosition) {
        case IMobileSdkAdsUnityPlugin.AdValignPosition.TOP:
            topPosition = 0;
            break;
        case IMobileSdkAdsUnityPlugin.AdValignPosition.MIDDLE:
            topPosition = (screenHeight / 2) - (getHeight(adType, iconParams) / 2);
            break;
        case IMobileSdkAdsUnityPlugin.AdValignPosition.BOTTOM:
            topPosition = screenHeight - getHeight(adType, iconParams);
            break;
        }
        return topPosition;
    }

	private static int getWidth(IMobileSdkAdsUnityPlugin.AdType adType, IMobileIconParams iconParams){
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
            width = getIconAdWidth(iconParams);
            break;
        }

        return width;
    }

	private static int getHeight(IMobileSdkAdsUnityPlugin.AdType adType, IMobileIconParams iconParams){
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
			height = getIconAdHeight(iconParams);
			break;
		}
		
		return height;
	}


	private const int IconDefaultWidth = 57;
	private const int IconMinimumWidth = 47;
	private const int IconDefaultMargin = 18;
	private const int IconMinimumMargin = 4;

	private static int getIconAdWidth(IMobileIconParams iconParams){
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
				int iconWidth = (iconParams.iconSize > 0) ? Math.Max (iconParams.iconSize, IconMinimumWidth) : IconDefaultWidth;
				return (iconWidth + IconDefaultMargin) * iconParams.iconNumber;
            } else {
                return minWidth;
            }
        }

		return minWidth;
    }

	private static int getIconAdHeight(IMobileIconParams iconParams){

		int iconAdWidth = getIconAdWidth (iconParams);
		int iconImageWidth = getIconImageWidth (iconParams, iconAdWidth);
		int iconImageMargin = getIconImageMargin (iconParams, iconAdWidth, iconImageWidth);

		if (!iconParams.iconTitleEnable) {
			return iconImageWidth;
		}

		int iconTitleOffset = (iconParams.iconTitleOffset > 0) ? iconParams.iconTitleOffset : 4;
		int iconTitleFontSize = (iconParams.iconTitleFontSize > 0) ? Math.Max (iconParams.iconTitleFontSize, 8) : 10;
		int iconTitleShadowDy = (iconParams.iconTitleShadowEnable) ? iconParams.iconTitleShadowDy : 0;
		int iconAdButtomMargin = 3;

		bool isSingleLineIconTitleDisplayable = (iconImageWidth + iconImageMargin) >= (IconDefaultWidth + IconDefaultMargin / 2);

		if (isSingleLineIconTitleDisplayable) {
			return iconImageWidth + iconTitleOffset + iconTitleFontSize + iconTitleShadowDy + iconAdButtomMargin;
		} else {
			return iconImageWidth + iconTitleOffset + iconTitleFontSize * 2 + iconTitleShadowDy + iconAdButtomMargin;
		}
	}

	private static int getIconImageWidth(IMobileIconParams iconParams, int iconAdWidth){
		if (iconParams.iconSize > 0) {
			return Math.Max (iconParams.iconSize, IconMinimumWidth);
		}

		int minimumAdViewWidthWithDefaultIconSize = (IconDefaultWidth + IconDefaultMargin) * iconParams.iconNumber;

		if (iconAdWidth >= minimumAdViewWidthWithDefaultIconSize) {
			return IconDefaultWidth;
		} else {
			return IconMinimumWidth;
		}
	}

	private static int getIconImageMargin(IMobileIconParams iconParams, int iconAdWidth, int iconImageWidth)
	{
		int totalMargin = iconAdWidth - iconImageWidth * iconParams.iconNumber;
		int iconImageMargin = (int)Math.Ceiling ((double)totalMargin / iconParams.iconNumber);
		return Math.Max (iconImageMargin, IconMinimumMargin);
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

