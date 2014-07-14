using UnityEngine;
using System.Collections;

public class StatusBoard : MonoBehaviour
{
	public FinishedEvolutionDialog finishedEvolutionDialogPrefab;
	private UILabel mCurrentExpLabel;
	private UILabel mNextExpLabel;
	private UILabel mNameLabel;
	private UIProgressBar mExpProgressBar;

	// Use this for initialization
	void Start ()
	{
		mCurrentExpLabel = GameObject.Find ("CurrentEXP").GetComponent<UILabel> ();
		mNextExpLabel = GameObject.Find ("NextEXP").GetComponent<UILabel> ();
		mNameLabel = gameObject.transform.FindChild ("Name").GetComponent<UILabel> ();
		mExpProgressBar = gameObject.transform.FindChild ("ProgressBar").GetComponent<UIProgressBar> ();
	}
	
	private void updateEXPPoint (string tag)
	{
		int currentExpPoint = int.Parse (mCurrentExpLabel.text);
		Hashtable playerData = PlayerDataDao.getInstance ().getPlayerData ();
		int mainGalSize = (int)playerData [PlayerDataDao.MAIN_GAL_SIZE];
		int kyabaSize = (int)playerData [PlayerDataDao.KYABA_SIZE];
		if (tag == "Gal") {
			currentExpPoint++;
			if (mainGalSize <= 0) {
				mainGalSize = 0;
			} else {
				mainGalSize--;
			}
			PlayerDataDao.getInstance ().UpdateMainGalSize (mainGalSize);
		} else if (tag == "Kyaba") {
			currentExpPoint += 50;
			if (kyabaSize <= 0) {
				kyabaSize = 0;
			} else {
				kyabaSize--;
			}
			PlayerDataDao.getInstance ().UpdateKyabaSize (kyabaSize);
		}
		PlayerDataDao.getInstance ().UpdateExpPoint (currentExpPoint);
		mCurrentExpLabel.text = "" + currentExpPoint;
		int nextExpPoint = int.Parse (mNextExpLabel.text);
		float progress = (float)currentExpPoint / (float)nextExpPoint;
		mExpProgressBar.value = progress;

		if (currentExpPoint >= nextExpPoint) {
			GameObject player = GameObject.Find ("Player");
			player.SendMessage ("StartEvolution");
		}

	}

	private void FinishEvolution (int evolutionPoint)
	{

		PlayerDataDao.getInstance ().UpdateExpPoint (0);
		Hashtable bandmanData = CharactorListDao.getInstance ().getBandmanData (evolutionPoint);
		mNameLabel.text = (string)bandmanData [CharactorListDao.NAME_FIELD];
		mNextExpLabel.text = "" + (int)bandmanData [CharactorListDao.EXP_POINT_FIELD];

		Hashtable playerData = PlayerDataDao.getInstance ().getPlayerData ();
		int expPoint = (int)playerData [PlayerDataDao.EXP_POINT_FIELD];
		mCurrentExpLabel.text = "" + expPoint;
		mExpProgressBar.value = 0;
		PrefsManager.getInstance ().SaveShareName (mNameLabel.text);
		PrefsManager.getInstance ().SaveShareImageId (evolutionPoint);
		FinishedEvolutionDialog dialog = Instantiate (finishedEvolutionDialogPrefab) as FinishedEvolutionDialog;
		dialog.Show (mNameLabel.text);

	}
}
