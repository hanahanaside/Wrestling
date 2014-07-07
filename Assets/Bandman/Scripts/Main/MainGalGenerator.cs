using UnityEngine;
using System.Collections;

public class MainGalGenerator : MonoBehaviour
{

	public GameObject mainGalPrefab;
	public GameObject kyabaPrefab;
	public Transform anchor;

	// Use this for initialization
	void Start ()
	{

		Hashtable playerData = PlayerDataDao.getInstance().getPlayerData();
		int mainGalSize = (int)playerData[PlayerDataDao.MAIN_GAL_SIZE];
		int kyabaSize = (int)playerData[PlayerDataDao.KYABA_SIZE];

		for (int i = 0; i<mainGalSize; i++) {
			float x = Random.Range (-2.0f, 2.0f);
			float y = Random.Range (-1.0f, 1.0f);
			GameObject mainGal = Instantiate (mainGalPrefab, new Vector3 (x, y, 10), Quaternion.identity)as GameObject;
			mainGal.transform.parent = anchor;
		}
		
		for(int i = 0;i<kyabaSize;i++){
			float x = Random.Range (-2.0f, 2.0f);
			float y = Random.Range (-1.0f, 1.0f);
			GameObject kyaba = Instantiate (kyabaPrefab, new Vector3 (x, y, 10), Quaternion.identity)as GameObject;
			kyaba.transform.parent =  anchor;
		}
	}
	

}
