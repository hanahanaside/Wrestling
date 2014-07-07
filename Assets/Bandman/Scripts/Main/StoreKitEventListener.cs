using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StoreKitEventListener : MonoBehaviour
{
	public GameObject kyabaPrefab;
	public CommercialDialogController commercialDialogController;

#if UNITY_IPHONE
	void OnEnable()
	{
		// Listens to all the StoreKit events. All event listeners MUST be removed before this object is disposed!
		StoreKitManager.transactionUpdatedEvent += transactionUpdatedEvent;
		StoreKitManager.productPurchaseAwaitingConfirmationEvent += productPurchaseAwaitingConfirmationEvent;
		StoreKitManager.purchaseSuccessfulEvent += purchaseSuccessfulEvent;
		StoreKitManager.purchaseCancelledEvent += purchaseCancelledEvent;
		StoreKitManager.purchaseFailedEvent += purchaseFailedEvent;
		StoreKitManager.productListReceivedEvent += productListReceivedEvent;
		StoreKitManager.productListRequestFailedEvent += productListRequestFailedEvent;
		StoreKitManager.restoreTransactionsFailedEvent += restoreTransactionsFailedEvent;
		StoreKitManager.restoreTransactionsFinishedEvent += restoreTransactionsFinishedEvent;
		StoreKitManager.paymentQueueUpdatedDownloadsEvent += paymentQueueUpdatedDownloadsEvent;

	}
	
	
	void OnDisable()
	{
		// Remove all the event handlers
		StoreKitManager.transactionUpdatedEvent -= transactionUpdatedEvent;
		StoreKitManager.productPurchaseAwaitingConfirmationEvent -= productPurchaseAwaitingConfirmationEvent;
		StoreKitManager.purchaseSuccessfulEvent -= purchaseSuccessfulEvent;
		StoreKitManager.purchaseCancelledEvent -= purchaseCancelledEvent;
		StoreKitManager.purchaseFailedEvent -= purchaseFailedEvent;
		StoreKitManager.productListReceivedEvent -= productListReceivedEvent;
		StoreKitManager.productListRequestFailedEvent -= productListRequestFailedEvent;
		StoreKitManager.restoreTransactionsFailedEvent -= restoreTransactionsFailedEvent;
		StoreKitManager.restoreTransactionsFinishedEvent -= restoreTransactionsFinishedEvent;
		StoreKitManager.paymentQueueUpdatedDownloadsEvent -= paymentQueueUpdatedDownloadsEvent;

	}
	
	
	
	void transactionUpdatedEvent( StoreKitTransaction transaction )
	{
		Debug.Log( "transactionUpdatedEvent: " + transaction );
	}

	
	void productListReceivedEvent( List<StoreKitProduct> productList )
	{
		Debug.Log( "productListReceivedEvent. total products received: " + productList.Count );
		hideActivityView();
		
		// print the products to the console
		foreach( StoreKitProduct product in productList )
			Debug.Log( product.ToString() + "\n" );
	}
	
	
	void productListRequestFailedEvent( string error )
	{
		Debug.Log( "productListRequestFailedEvent: " + error );
		hideActivityView();
		string[] buttons = new string[] {"OK"};
		string title = "\u63a5\u7d9a\u3067\u304d\u307e\u305b\u3093\u3067\u3057\u305f\u3002";
		string message = "\u3082\u3046\uff11\u5ea6\u304a\u8a66\u3057\u304f\u3060\u3055\u3044 ";
		EtceteraBinding.showAlertWithTitleMessageAndButtons (title, message, buttons);
	}
	

	void purchaseFailedEvent( string error )
	{
		Debug.Log( "purchaseFailedEvent: " + error );
		hideActivityView();
		string message = "\u8cfc\u5165\u3057\u307e\u305b\u3093\u3067\u3057\u305f";
		string[] buttons = {"OK"};
		EtceteraBinding.showAlertWithTitleMessageAndButtons(error,message,buttons);
	}
	

	void purchaseCancelledEvent( string error )
	{
		Debug.Log( "purchaseCancelledEvent: " + error );
		hideActivityView();
	}
	
	
	void productPurchaseAwaitingConfirmationEvent( StoreKitTransaction transaction )
	{
		Debug.Log( "productPurchaseAwaitingConfirmationEvent: " + transaction );
	}
	
	
	void purchaseSuccessfulEvent( StoreKitTransaction transaction )
	{
		Debug.Log("purchaseSuccessfulEvent");
		//complete purchase
		Debug.Log("productIdentifier = "+transaction.productIdentifier);
		hideActivityView();
		string productIdentifer = transaction.productIdentifier;
		int count = 0;
		if(productIdentifer == "item_1"){
			count = 2;
		}
		if(productIdentifer == "item_2"){
			count = 5;
		}
		if(productIdentifer == "item_3"){
			count = 8;
		}
		if(productIdentifer == "item_4"){
			count = 14;
		}
		Hashtable playerData = PlayerDataDao.getInstance().getPlayerData();
		int kyabaSize = (int)playerData[PlayerDataDao.KYABA_SIZE];
		PlayerDataDao.getInstance ().UpdateKyabaSize (kyabaSize + count);
		for(int i = 0;i < count;i++){
			float x = Random.Range (-2.0f, 2.0f);
			float y = Random.Range (-1.0f, 1.0f);
			GameObject kyabaObject = Instantiate (kyabaPrefab, new Vector3 (x, y, 10), Quaternion.identity) as GameObject;
			GameObject anchor = GameObject.Find("Anchor");
			kyabaObject.transform.parent = anchor.transform;
		}
		string title = "\u8cfc\u5165\u5b8c\u4e86";
		string message = "\u30ad\u30e3\u30d0\u5b22\u30b2\u30c3\u30c8!!";
		string[] buttons = new string[] {"YES"};
		EtceteraBinding.showAlertWithTitleMessageAndButtons (title, message, buttons);
	}
	
	
	void restoreTransactionsFailedEvent( string error )
	{
		Debug.Log( "restoreTransactionsFailedEvent: " + error );
	}
	
	
	void restoreTransactionsFinishedEvent()
	{
		Debug.Log( "restoreTransactionsFinished" );
	}
	
	
	void paymentQueueUpdatedDownloadsEvent( List<StoreKitDownload> downloads )
	{
		Debug.Log( "paymentQueueUpdatedDownloadsEvent: " );
		foreach( var dl in downloads )
			Debug.Log( dl );
	}

	private void hideActivityView(){
		commercialDialogController.isDialogShowing = false;
		EtceteraBinding.hideActivityView();
	}
	
#endif
}

