using UnityEngine;
using System.Collections;

public class AdManager : MonoBehaviour {

	public string publisherId;
	private static AdManager sInstance;
	private int mBannerViewId = -1;
	private int mIconViewId = -1;
	public Account account;
	public NendAdBanner nendAdBanner;
	public NendAdIcon nendAdIcon;
	public GameFeatManager gamefeatManager;
	
	void Start () {
		if (sInstance == null) {
			sInstance = this;
			DontDestroyOnLoad (gameObject);
		}
		string bannerMediaId;
		string bannerSpotId;
		string iconMediaId;
		string iconSpotId;
		string wallMediaId;
		string wallSpotId;
#if UNITY_IPHONE
			bannerMediaId = account.iOS.bannerMediaId;
			bannerSpotId = account.iOS.bannerSpotId;
			iconMediaId = account.iOS.iconMediaId;
			iconSpotId = account.iOS.iconSpotId;
			wallMediaId = account.iOS.wallMediaId;
			wallSpotId = account.iOS.wallSpotId;
#endif

#if UNITY_ANDROID
		return;
//			bannerMediaId = account.android.bannerMediaId;
//			bannerSpotId = account.android.bannerSpotId;
//			iconMediaId = account.android.iconMediaId;
//			iconSpotId = account.android.iconSpotId;
//			wallMediaId = account.android.wallMediaId;
//			wallSpotId = account.android.wallSpotId;
#endif

		//prepare banner
		IMobileSdkAdsUnityPlugin.registerInline (publisherId, bannerMediaId, bannerSpotId);
		IMobileSdkAdsUnityPlugin.start (bannerSpotId);
		mBannerViewId = IMobileSdkAdsUnityPlugin.show (bannerSpotId, IMobileSdkAdsUnityPlugin.AdType.BANNER, 
			                               IMobileSdkAdsUnityPlugin.AdAlignPosition.CENTER, 
			                               IMobileSdkAdsUnityPlugin.AdValignPosition.BOTTOM);

		//prepare icon
		IMobileSdkAdsUnityPlugin.registerInline (publisherId, iconMediaId, iconSpotId);
		IMobileSdkAdsUnityPlugin.start (iconSpotId);
		IMobileIconParams iconParams = new IMobileIconParams ();
		iconParams.iconNumber = 2;
		iconParams.iconTitleEnable = false;
		mIconViewId = IMobileSdkAdsUnityPlugin.show (account.iOS.iconSpotId, IMobileSdkAdsUnityPlugin.AdType.ICON, 0, 10, iconParams);

		//prepare wall
		IMobileSdkAdsUnityPlugin.registerFullScreen (publisherId, wallMediaId, wallSpotId);
		IMobileSdkAdsUnityPlugin.start (wallSpotId);

		HideBannerAd ();
		HideIconAd (); 
	}

	public static AdManager Instance {
		get {
			return sInstance;
		}
	}

	public void ShowIconAd () {
		#if UNITY_IPHONE
		if (mIconViewId != -1) {
			IMobileSdkAdsUnityPlugin.setVisibility (mIconViewId, true);
		}
#endif
#if UNITY_ANDROID
		nendAdIcon.Show();
#endif
	}

	public void ShowBannerAd () {
		#if UNITY_IPHONE
		if (mBannerViewId != -1) {
			IMobileSdkAdsUnityPlugin.setVisibility (mBannerViewId, true);
		}
#endif
#if UNITY_ANDROID
		nendAdBanner.Show();
#endif
	}

	public void HideIconAd () {
		#if UNITY_IPHONE
		if (mIconViewId != -1) {
			IMobileSdkAdsUnityPlugin.setVisibility (mIconViewId, false);
		}
#endif
#if UNITY_ANDROID
		nendAdIcon.Hide();
#endif
	}

	public void HideBannerAd () {
		#if UNITY_IPHONE
		if (mBannerViewId != -1) {
			IMobileSdkAdsUnityPlugin.setVisibility (mBannerViewId, false);
		}
#endif
#if UNITY_ANDROID
		nendAdBanner.Hide();
#endif
	}

	public void ShowWallAd () {
		string wallSpotId;
		#if UNITY_IPHONE
		wallSpotId = account.iOS.wallSpotId;
#endif
#if UNITY_ANDROID
		gamefeatManager.loadGF();
		return;
	//	wallSpotId = account.android.wallSpotId;
#endif
		IMobileSdkAdsUnityPlugin.show (wallSpotId);

	}

	[System.SerializableAttribute]
	public class Account {
		public ImobileID android;
		public ImobileID iOS;
	}

	[System.SerializableAttribute]
	public class ImobileID {
		public string bannerMediaId;
		public string bannerSpotId;
		public string iconMediaId;
		public string iconSpotId;
		public string wallMediaId;
		public string wallSpotId;
	}


}
