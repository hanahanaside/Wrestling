using UnityEngine;
using System.Collections;

public class PlayerDataDao : ScriptableObject
{

	public const string EVOLUTION_POINT_FIELD = "evolution_point";
	public const string EXP_POINT_FIELD = "exp_point";
	public const string MAIN_GAL_SIZE = "main_gal_size";
	public const string HARAJUKU_GAL_SIZE = "harajuku_gal_size";
	public const string EXIT_TIME = "exit_time";
	public const string KYABA_SIZE = "kyaba_size";
	public const string CLEARED_FIELD = "cleared";
	private static PlayerDataDao sInstance;
	private SQLiteDB mSqliteDB;
	private SQLiteQuery mSqliteQuery;

	public static PlayerDataDao getInstance ()
	{
		if (sInstance == null) {
			sInstance = ScriptableObject.CreateInstance<PlayerDataDao> ();
		}
		return sInstance;
	}

	public Hashtable getPlayerData(){
		OpenDatabase ();
		mSqliteQuery = new SQLiteQuery (mSqliteDB, "select * from player_data;");
		Hashtable playerData = new Hashtable();
		while(mSqliteQuery.Step()){
			int evolutionPoint = mSqliteQuery.GetInteger(EVOLUTION_POINT_FIELD);
			int expPoint = mSqliteQuery.GetInteger(EXP_POINT_FIELD);
			int mainGalSize = mSqliteQuery.GetInteger(MAIN_GAL_SIZE);
			int harajukuGalSize = mSqliteQuery.GetInteger(HARAJUKU_GAL_SIZE);
			float exitTime = (float)mSqliteQuery.GetDouble(EXIT_TIME);
			int kyabaSize = mSqliteQuery.GetInteger(KYABA_SIZE);
			int cleared = mSqliteQuery.GetInteger(CLEARED_FIELD);
			playerData.Add(EVOLUTION_POINT_FIELD,evolutionPoint);
			playerData.Add(EXP_POINT_FIELD,expPoint);
			playerData.Add(MAIN_GAL_SIZE,mainGalSize);
			playerData.Add(HARAJUKU_GAL_SIZE,harajukuGalSize);
			playerData.Add(EXIT_TIME,exitTime);
			playerData.Add(KYABA_SIZE,kyabaSize);
			playerData.Add(CLEARED_FIELD,cleared);
		}
		mSqliteQuery.Release ();
		CloseDatabase();
		return playerData;
	}

	private void OpenDatabase ()
	{
		string filename = Application.persistentDataPath + "/bandman.db";
		mSqliteDB = new SQLiteDB ();
		mSqliteDB.Open (filename);
	}

	private void CloseDatabase ()
	{
		mSqliteDB.Close ();
	}

	private void releaseQuery ()
	{
		mSqliteQuery.Release ();
	}

	public void Reset ()
	{
		OpenDatabase ();
		mSqliteQuery = new SQLiteQuery (mSqliteDB, "update player_data set "
			+ EVOLUTION_POINT_FIELD + " = 1,"
			+ EXP_POINT_FIELD + " = 0,"
			+ MAIN_GAL_SIZE + " = 0,"
			+ HARAJUKU_GAL_SIZE + " = 0,"
			+ EXIT_TIME + " = 0;"
		);
		mSqliteQuery.Step ();
		mSqliteQuery.Release ();
		CloseDatabase ();
	}

	public void UpdateExpPoint (int expPoint)
	{
		OpenDatabase ();
		mSqliteQuery = new SQLiteQuery (mSqliteDB, "update player_data set exp_point = " + expPoint + ";");
		mSqliteQuery.Step ();
		mSqliteQuery.Release ();
		CloseDatabase ();
	}

	public void UpdateMainGalSize (int mainGalSize)
	{
		OpenDatabase ();
		mSqliteQuery = new SQLiteQuery (mSqliteDB, "update player_data set main_gal_size = " + mainGalSize + ";");
		mSqliteQuery.Step ();
		mSqliteQuery.Release ();
		CloseDatabase ();
	}

	public void UpdateKyabaSize (int kyabaSize)
	{
		OpenDatabase ();
		mSqliteQuery = new SQLiteQuery (mSqliteDB, "update player_data set kyaba_size = " + kyabaSize + ";");
		mSqliteQuery.Step ();
		mSqliteQuery.Release ();
		CloseDatabase ();
	}

	public void UpdateExitTime (float exitTime)
	{
		OpenDatabase ();
		mSqliteQuery = new SQLiteQuery (mSqliteDB, "update player_data set exit_time = " + exitTime + ";");
		mSqliteQuery.Step ();
		mSqliteQuery.Release ();
		CloseDatabase ();
	}

	public void UpdateHarajukuGalSize (int harajukuGalSize)
	{
		OpenDatabase ();
		mSqliteQuery = new SQLiteQuery (mSqliteDB, "update player_data set harajuku_gal_size = " + harajukuGalSize + ";");
		mSqliteQuery.Step ();
		mSqliteQuery.Release ();
		CloseDatabase ();
	}

	public void UpdateEvolutionPoint (int evolutionPoint)
	{
		OpenDatabase ();
		mSqliteQuery = new SQLiteQuery (mSqliteDB, "update player_data set evolution_point = " + evolutionPoint + ";");
		mSqliteQuery.Step ();
		mSqliteQuery.Release ();
		CloseDatabase ();
	}

	public void UpdateCleared(int state){
		OpenDatabase ();
		mSqliteQuery = new SQLiteQuery (mSqliteDB, "update player_data set cleared = " + state + ";");
		mSqliteQuery.Step ();
		mSqliteQuery.Release ();
		CloseDatabase ();
	}
}
