using System;
using UnityEngine;
using System.Collections;

namespace AppDriver.Android
{
	public class AppDriverTracker : MonoBehaviour
	{
		
		private static AndroidJavaClass getTracker()
		{
			return new AndroidJavaClass("net.adways.appdriver.sdk.unity.AppDriverTrackerforUnity");
		}
		
		private static AndroidJavaObject getActivity()
		{
			AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			return unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
		}

		public static void setDevMode (bool b) 
		{ 
			getTracker().CallStatic("setDevMode", b);
		}
		
		public static void setTestMode (bool b) 
		{ 
			getTracker().CallStatic("setTestMode", b);
		}
		
		public static void setRefresh (int time, string url) 
		{ 
			getTracker().CallStatic("setRefresh", time, url);
	    }
		
		public static void requestAppDriver (int id, string key) 
		{ 
			getTracker().CallStatic("requestAppDriver", getActivity(), id, key);
		}
		
		public static void requestAppDriver (int id, string key, int mode)
		{
			getTracker().CallStatic("requestAppDriver", getActivity(), id, key, mode);
		}
		
		public static void requestAppDriver (int id, string key, int mode, string identifier)
		{
			getTracker().CallStatic("requestAppDriver", getActivity(), id, key, mode, identifier);
		}
		
		public static void actionComplete (string thanks)
		{
			getTracker().CallStatic("actionComplete", getActivity(), thanks);
		}
		
		public static void targetComplete (string target)
		{
			getTracker().CallStatic("targetComplete", target);
		}
		
		public static void targetCancel (string target)
		{
			getTracker().CallStatic("targetCancel", target);
		}

    	public static void paymentComplete ( string transactionIdentifier, string productIdentifier,
			                                 float price, int quantity, string currency ){
		    getTracker().CallStatic("paymentComplete", transactionIdentifier, productIdentifier, price, quantity, currency);
		}
		
		public static void setVerboseMode (bool b){ 
			getTracker().CallStatic("setVerboseMode", b);
		}
		
    	public static void childActionComplete (string advertisement){ 
			getTracker().CallStatic("childActionComplete", advertisement);
		}

    	public static void childActionComplete (string advertisement, string requirement){ 
			getTracker().CallStatic("childActionComplete", advertisement, requirement);
		}

     	public static void openOfferwall(Hashtable ht){
		
			AndroidJavaClass factoryClass = new AndroidJavaClass("net.adways.appdriver.sdk.AppDriverFactory");
			AndroidJavaObject promotionObj = factoryClass.CallStatic<AndroidJavaObject>( "getPromotionClass", getActivity() );
			AndroidJavaObject objIntent = new AndroidJavaObject("android.content.Intent", getActivity(), promotionObj );

		    foreach (DictionaryEntry de in ht) {
		        if(MEDIA_ID.Equals(de.Key)){
					objIntent.Call<AndroidJavaObject> ("putExtra", MEDIA_ID, de.Value);
				}
				if(IDENTIFIER.Equals(de.Key)){
					objIntent.Call<AndroidJavaObject> ("putExtra", IDENTIFIER, de.Value);
				}
				if(PROMOTION_ID.Equals(de.Key)){
					objIntent.Call<AndroidJavaObject> ("putExtra", PROMOTION_ID, de.Value);
				}
				if(ITEM_IDENTIFIER.Equals(de.Key)){
					objIntent.Call<AndroidJavaObject> ("putExtra", ITEM_IDENTIFIER, de.Value);
				}
				if(ITEM_PRICE.Equals(de.Key)){
					objIntent.Call<AndroidJavaObject> ("putExtra", ITEM_PRICE, de.Value);
				}
				if(ITEM_NAME.Equals(de.Key)){
					objIntent.Call<AndroidJavaObject> ("putExtra", ITEM_NAME, de.Value);
				}
				if(ITEM_IMAGE.Equals(de.Key)){
					objIntent.Call<AndroidJavaObject> ("putExtra", ITEM_IMAGE, de.Value);
				}
				if(CLICK_PROMOTION.Equals(de.Key)){
					objIntent.Call<AndroidJavaObject> ("putExtra", CLICK_PROMOTION, de.Value);
				}
			}

			getActivity().Call( "startActivity", objIntent );
			
			objIntent.Dispose();
			promotionObj.Dispose();
			factoryClass.Dispose();
			getActivity().Dispose();
		}
		
		public static int SETTING_MODE   = 0;
		public static int PROMTION_MODE  = 1;
		public static int GYRO_MODE      = 2;
		public static int ANALYTICS_MODE = 4;
		public static int DEFAULT_MODE   = 3;
    	public static string MEDIA_ID = "MEDIA_ID";
		public static string IDENTIFIER = "IDENTIFIER";
		public static string PROMOTION_ID = "PROMOTION_ID";
		public static string ITEM_IDENTIFIER = "ITEM_IDENTIFIER";
		public static string ITEM_PRICE = "ITEM_PRICE";
        public static string ITEM_NAME = "ITEM_NAME";
		public static string ITEM_IMAGE = "ITEM_IMAGE";
		public static string CLICK_PROMOTION = "CLICK_PROMOTION";
	
	}
}