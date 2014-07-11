using UnityEngine;
using System.Collections.Generic;
using Prime31;

public class CommercialDialogController : MonoBehaviour
{
	public string[] skuArray;
	public string encodePublicKey;

	public bool isDialogShowing{ get; set; }

	void Awake ()
	{
		#if UNITY_ANDROID
		GoogleIAB.init("MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAsl3gJcgnduS5Z5cUGdRwy0eC1Rm97o8JWXMIRwKVmCp6YEKXnsK6yIdoJDSSsujm/MWnvhrlLXNvCTS/lTx7VoaDUzfUx5sDk6pXHIVs/XYqcPwaW3r9nUdAcnzynvcZKma9h2XL1Eirm0HeFoBZXSoafvzeDZ4VyXBe8ZimGBRjTuXdf9nRMTowAY83sbNxbeI45KC8ZAWYGMyDBs+wj3uKIDkkrg+9H5bfd3E3PHmc38ijrQFiJKc67NhEzv2xHu04qnUqfu80O0Atz2nVFauFh4Qnm6zYcVLOe5M7DlpImjMzQ2BHBCTnl69di0I7PcBLZqCXS8KM6w7RV658AQIDAQAB");
#endif
	}

	void Start ()
	{

		#if UNITY_ANDROID
		GoogleIAB.queryInventory( skuArray );
		#endif
	}

	public void OnButtonClick ()
	{
		Debug.Log ("OnButtonClick");

		if (isDialogShowing) {
			return;
		}



		string buttonName = UIButton.current.name;
		Debug.Log (buttonName);
		if (buttonName == "BackButton") {
			GameObject.Find ("MainController").GetComponent<MainController> ().isDialogShowing = false;
			GameObject.Find ("UIFence").SetActive (false);
			Destroy (gameObject.transform.parent.gameObject);
			return;
		}

		#if UNITY_IPHONE
		if(ProductListKeeper.instance.productList == null ){
			string title = "\u901a\u4fe1\u30a8\u30e9\u30fc";
			string message = "\u30a2\u30a4\u30c6\u30e0\u306e\u53d6\u5f97\u306b\u5931\u6557\u3057\u307e\u3057\u305f";
			string[] buttons = {"OK"};
			EtceteraBinding.showAlertWithTitleMessageAndButtons(title,message,buttons);
		}else {
			PurchaseProduct (buttonName);
		}
		#endif

#if UNITY_ANDROID
		PurchaseSku(buttonName);
#endif
	}

#if UNITY_IPHONE
	private void PurchaseProduct (string buttonName)
	{
		int productIndex = 0;
		if (buttonName == "FryerButton") {
			productIndex = 0;
		}
		
		if (buttonName == "StreetLiveButton") {
			productIndex = 1;
		}
		
		if (buttonName == "BroadcastVisionButton") {
			productIndex = 2;
		}
		
		if (buttonName == "GuerrillaLiveButton") {
			productIndex = 3;
		}
		List<StoreKitProduct> productList = ProductListKeeper.instance.productList;
		StoreKitProduct product = productList [productIndex];

			Debug.Log ("preparing to purchase product: " + product.productIdentifier);
			StoreKitBinding.purchaseProduct (product.productIdentifier, 1);
			EtceteraBinding.showBezelActivityViewWithLabel("Loading");
			isDialogShowing = true;

	}
#endif

#if UNITY_ANDROID
	private void PurchaseSku (string buttonName)
	{
		string sku = "";
		if (buttonName == "FryerButton") {
			sku = skuArray[0];
		}
		if (buttonName == "StreetLiveButton") {
			sku = skuArray[1];
		}
		if (buttonName == "BroadcastVisionButton") {
			sku = skuArray[2];
		}
		if (buttonName == "GuerrillaLiveButton") {
			sku = skuArray[3];
		}
		Debug.Log("sku = "+sku);
	//	GoogleIAB.purchaseProduct(sku);
	//	GoogleIAB.consumeProduct( "android.test.purchased" );
	//	GoogleIAB.purchaseProduct("android.test.purchased");
	//	GoogleIAB.consumeProduct("android.test.purchased");
		GoogleIAB.purchaseProduct(sku);
	}
#endif
}
