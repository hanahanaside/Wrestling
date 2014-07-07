using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ProductListKeeper : MonoBehaviour {
	
	public string[] productIdentifiers;
	#if UNITY_IPHONE
	private static ProductListKeeper sInstance;
	private static List<StoreKitProduct> _products;

	void OnEnable()
	{
		StoreKitManager.productListReceivedEvent += ReceivedProductsList;
	}
	
	void OnDisable()
	{
		StoreKitManager.productListReceivedEvent -= ReceivedProductsList;
	}
	
	
	// Use this for initialization
	void Start () {
		if(sInstance == null){
			sInstance = this;
		}
		if(_products == null){
			requestProductData();
		}
	}
	
	void ReceivedProductsList(List<StoreKitProduct> productsList){
		if(productsList != null && productsList.Count >0){
			_products = productsList;
			Debug.Log ("received total products: " + productsList.Count);
			Debug.Log("name = "+productList[0].productIdentifier);
		}
	}

	public static ProductListKeeper instance{
		get{
			return sInstance;
		}
	}

	public List<StoreKitProduct> productList{
		get{
			return _products;
		}
	}

	public void requestProductData(){
		Debug.Log("requestProductData");
		StoreKitBinding.requestProductData (productIdentifiers);
	}


#endif

}
