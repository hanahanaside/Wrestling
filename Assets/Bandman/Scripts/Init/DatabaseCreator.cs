using UnityEngine;
using System.IO;
using System.Collections;

public class DatabaseCreator : MonoBehaviour {

	public string databaseFileName;
	public InitController initController;

	// Use this for initialization
	void Start () {
		#if UNITY_IPHONE
		string baseFilePath = Application.streamingAssetsPath + "/" + databaseFileName;
		string filePath = Application.persistentDataPath + "/" + databaseFileName;
		if(!File.Exists(filePath)){
		File.Delete(filePath);
			File.Copy( baseFilePath, filePath); 
			Debug.Log("create Database");
		}
		initController.CreatedDatabase();
		#endif

		#if UNITY_ANDROID
		string baseFilePath = Path.Combine (Application.streamingAssetsPath, databaseFileName);
		string filePath = Application.persistentDataPath + "/" + databaseFileName;
		if(File.Exists(filePath)){
			File.Delete(filePath);
			StartCoroutine(CreateAndroidDatabase(baseFilePath,filePath));
		}

/*		if(File.Exists(filePath)){
			initController.CreatedDatabase();
		}else {
			StartCoroutine(CreateAndroidDatabase(baseFilePath,filePath));
		}
*/
#endif
	}

	#if UNITY_ANDROID
	private IEnumerator CreateAndroidDatabase (string baseFilePath,string filePath)
	{
		Debug.Log ("CreateAndroidDatabase");
		Debug.Log ("baseFilePath = " + baseFilePath);
		Debug.Log ("filePath = " + filePath);
		WWW www = new WWW (baseFilePath);
		yield return www;
		File.WriteAllBytes (filePath, www.bytes);
		Debug.Log ("create finished");
		initController.CreatedDatabase();
	}
	#endif

}
