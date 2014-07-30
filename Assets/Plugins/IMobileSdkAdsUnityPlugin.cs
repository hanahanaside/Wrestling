using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public class IMobileSdkAdsUnityPlugin : MonoBehaviour {

	// 広告の向き
	public enum ImobileSdkAdsAdOrientation : int {
		IMOBILESDKADS_AD_ORIENTATION_AUTO,
		IMOBILESDKADS_AD_ORIENTATION_PORTRAIT,
		IMOBILESDKADS_AD_ORIENTATION_LANDSCAPE,
	}

    public enum AdAlignPosition{
        LEFT,
        CENTER,
        RIGHT
    }

    public enum AdValignPosition{
        BOTTOM,
        MIDDLE,
        TOP
    }

    public enum AdType{
        ICON,
        BANNER,
        BIG_BANNER,
		TABLET_BANNER,
		TABLET_BIG_BANNER,
        MEDIUM_RECTANGLE,
		BIG_RECTANGLE,
		SKYSCRAPER,
		WIDE_SKYSCRAPER,
		SQUARE,
		SMALL_SQUARE,
		HALFPAGE
    }

	#region Unity Pugin init
	#if UNITY_IPHONE
	[DllImport("__Internal")]
	private static extern void imobileAddObserver_(string gameObjectName);
	[DllImport("__Internal")]
	private static extern void imobileRemoveObserver_(string gameObjectName);
	[DllImport("__Internal")]
	private static extern void imobileRegisterWithPublisherID_(string publisherid, string mediaid, string spotid); 
	[DllImport("__Internal")]
	private static extern void imobileStart_();
	[DllImport("__Internal")]
	private static extern void imobileStop_();
	[DllImport("__Internal")]
	private static extern bool imobileStartBySpotID_(string spotid);
	[DllImport("__Internal")]
	private static extern bool imobileStopBySpotID_(string spotid);
	[DllImport("__Internal")]
	private static extern bool imobileShowBySpotID_(string spotid);
	[DllImport("__Internal")]
	private static extern bool imobileShowBySpotIDWithPositionAndIconParams_(string spotid,
	                                                                         string publisherid,
	                                                                         string mediaid,
	                                                                         int left,
	                                                                         int top,
	                                                                         int width,
	                                                                         int height,
	                                                                         int iconNumber,
	                                                                         int iconViewLayoutWidth,
	                                                                         bool iconTitleEnable,
	                                                                         string iconTitleFontColor,
	                                                                         bool iconTitleShadowEnable,
	                                                                         string iconTitleShadowColor,
	                                                                         int iconTitleShadowDx,
	                                                                         int iconTitleShadowDy,
	                                                                         int adViewId);
	[DllImport("__Internal")]
	private static extern void imobileSetAdOrientation_(ImobileSdkAdsAdOrientation orientation);
	[DllImport("__Internal")]
	private static extern void imobileSetVisibility_(int adViewId, bool visible);

	#elif UNITY_ANDROID
    private static AndroidJavaClass imobileSdkAdsAndroidPlugin = new AndroidJavaClass("jp.co.imobile.sdkads.android.unity.Plugin");
    #endif
	#endregion

	#region Unity Pugin Function
	public static void addObserver(string gameObjectName){
		#if UNITY_IPHONE
		if(Application.platform == RuntimePlatform.IPhonePlayer) {
			imobileAddObserver_(gameObjectName);
		}
		#elif UNITY_ANDROID
		if(Application.platform == RuntimePlatform.Android) {
			imobileSdkAdsAndroidPlugin.CallStatic("addObserver",gameObjectName);
		}
		#endif
	}

	public static void removeObserver(string gameObjectName){
		#if UNITY_IPHONE
		if(Application.platform == RuntimePlatform.IPhonePlayer) {
			imobileRemoveObserver_(gameObjectName);
		}
		#elif UNITY_ANDROID
		if(Application.platform == RuntimePlatform.Android) {
			imobileSdkAdsAndroidPlugin.CallStatic("removeObserver",gameObjectName);
		}
		#endif
	}
	
	public static void register(string partnerid, string mediaid, string spotid){

		IMobileSpotInfoManager.SetSpotInfo(spotid, partnerid, mediaid);

		#if UNITY_IPHONE
		if(Application.platform == RuntimePlatform.IPhonePlayer) {
			imobileRegisterWithPublisherID_(partnerid, mediaid, spotid);
			imobileStartBySpotID_(spotid);
		}
		#elif UNITY_ANDROID
		if(Application.platform == RuntimePlatform.Android) {
            imobileSdkAdsAndroidPlugin.CallStatic("registerFullScreen", partnerid, mediaid, spotid);
            imobileSdkAdsAndroidPlugin.CallStatic("start", spotid);
        }
		#endif
	}

    /// <summary>
    /// フルスクリーン広告のスポットを登録します
    /// </summary>
    /// <param name="partnerid">Partnerid.</param>
    /// <param name="mediaid">Mediaid.</param>
    /// <param name="spotid">Spotid.</param>
    public static void registerFullScreen(string partnerid, string mediaid, string spotid){
        register (partnerid, mediaid, spotid);
    }

    /// <summary>
    /// インライン広告のスポットを登録します
    /// </summary>
    /// <param name="partnerid">Partnerid.</param>
    /// <param name="mediaid">Mediaid.</param>
    /// <param name="spotid">Spotid.</param>
    public static void registerInline(string partnerid, string mediaid, string spotid){
        IMobileSpotInfoManager.SetSpotInfo(spotid, partnerid, mediaid);
    }

	public static void start(){
	}
	
	public static void stop(){
		#if UNITY_IPHONE
		if(Application.platform == RuntimePlatform.IPhonePlayer) {
			imobileStop_();
		}
		#elif UNITY_ANDROID
		if(Application.platform == RuntimePlatform.Android) {
			imobileSdkAdsAndroidPlugin.CallStatic("stop");
		}
		#endif
	}
	
    public static void start(string spotid){
	}
	
    public static void stop(string spotid){
		#if UNITY_IPHONE
		if(Application.platform == RuntimePlatform.IPhonePlayer) {
			imobileStopBySpotID_(spotid); 
		}
		#elif UNITY_ANDROID
		if(Application.platform == RuntimePlatform.Android) {
			imobileSdkAdsAndroidPlugin.CallStatic("stop", spotid);
		}
		#endif
	}
	
    public static void show(string spotid){
		#if UNITY_IPHONE
		if(Application.platform == RuntimePlatform.IPhonePlayer) {
			imobileShowBySpotID_(spotid);		
		}
		#elif UNITY_ANDROID
		if(Application.platform == RuntimePlatform.Android) {
			imobileSdkAdsAndroidPlugin.CallStatic("show", spotid);
		}
		#endif
	}

    public static int show(string spotid, AdType adType, int left, int top){
        return show(spotid, adType, left, top, null);
	}

    public static int show(string spotid, AdType adType, int left, int top, IMobileIconParams iconParams){

		iconParams = iconParams ?? new IMobileIconParams();

		#if UNITY_IPHONE  || UNITY_ANDROID
		string partnerId = IMobileSpotInfoManager.GetPartnerId(spotid);
		string mediaId = IMobileSpotInfoManager.GetMediaId(spotid);
		#endif

		int adViewId = IMobileAdViewIdManager.createId();

		#if UNITY_IPHONE
		int width = IMobileSdkAdsViewUtility.getWidth(adType, iconParams);
		int height = IMobileSdkAdsViewUtility.getHeight(adType);
		if(Application.platform == RuntimePlatform.IPhonePlayer) {
			imobileShowBySpotIDWithPositionAndIconParams_(spotid, 
			                                              partnerId,
			                                              mediaId,
			                                              left,
			                                              top,
			                                              width,
			                                              height,
			                                              iconParams.iconNumber,
			                                              iconParams.iconViewLayoutWidth,
			                                              iconParams.iconTitleEnable,
			                                              iconParams.iconTitleFontColor,
			                                              iconParams.iconTitleShadowEnable,
			                                              iconParams.iconTitleShadowColor,
			                                              iconParams.iconTitleShadowDx,
			                                              iconParams.iconTitleShadowDy,
			                                              adViewId);
		}
		#elif UNITY_ANDROID
        if(Application.platform == RuntimePlatform.Android) {
            imobileSdkAdsAndroidPlugin.CallStatic("show", partnerId, mediaId, spotid, adViewId, left, top,
                iconParams.iconNumber, 
                iconParams.iconViewLayoutWidth,
                iconParams.iconTitleEnable, 
                iconParams.iconTitleFontColor, 
                iconParams.iconTitleShadowEnable,
                iconParams.iconTitleShadowColor,
                iconParams.iconTitleShadowDx,
                iconParams.iconTitleShadowDy
                );
        }
		#endif
		
		return adViewId;
	}

    public static int show(string spotid, AdType adType, AdAlignPosition alignPosition, AdValignPosition valignPosition) {
        return show (spotid, adType, alignPosition, valignPosition, null);
    }

    public static int show(string spotid, AdType adType, AdAlignPosition alignPosition, AdValignPosition valignPosition, IMobileIconParams iconParams) {
		int leftPosition = IMobileSdkAdsViewUtility.getLeft(alignPosition, adType, iconParams);
		int topPosition = IMobileSdkAdsViewUtility.getTop(valignPosition, adType, iconParams);

        return show (spotid, adType, leftPosition, topPosition, iconParams);
    }
	
	public static void setAdOrientation(ImobileSdkAdsAdOrientation orientation){
		#if UNITY_IPHONE
		if(Application.platform == RuntimePlatform.IPhonePlayer) {
			imobileSetAdOrientation_(orientation);
			return;
		}
		#endif
	}

    public static void setVisibility(int adViewId, bool visible){
		#if UNITY_IPHONE
		if(Application.platform == RuntimePlatform.IPhonePlayer) {
			imobileSetVisibility_(adViewId, visible);
			return;
		}
        #elif UNITY_ANDROID
        if(Application.platform == RuntimePlatform.Android) {
            imobileSdkAdsAndroidPlugin.CallStatic("setVisibility", adViewId, visible);
        }
		#endif
    }
	#endregion
}