using UnityEngine;
using System.Collections;

public class AdManager : MonoBehaviour {

	public NendAdBanner nendAdBanner;
	public NendAdIcon nendAdIcon;
	private static AdManager sInstance;

	void Start () {
		if (sInstance == null) {
			sInstance = this;
			DontDestroyOnLoad (gameObject);
		}
		if (OnSaleChecker.CheckOnSale ()) {
			nendAdBanner.enabled = true;
			nendAdIcon.enabled = true;
		}
	}

	public static AdManager Instance {
		get {
			return sInstance;
		}
	}

	public void ShowIconAd () {
		if (nendAdIcon.enabled) {
			nendAdIcon.Show ();
		}
	}

	public void ShowBannerAd () {
		if (nendAdBanner.enabled) {
			nendAdBanner.Show ();
		}
	}

	public void HideIconAd () {
		if (nendAdIcon.enabled) {
			nendAdIcon.Hide ();
		}
	}

	public void HideBannerAd () {
		if (nendAdBanner.enabled) {
			nendAdBanner.Hide ();
		}
	}
}
