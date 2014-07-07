using UnityEngine;
using System.Collections;

public class CharactorListDao : ScriptableObject {

	public const string NAME_FIELD = "name";
	public const string EXP_POINT_FIELD = "exp_point";
	public const string DESCRIPTION_FIELD = "description";
	public const string FLAG_TWEET_FIELD = "flag_tweet";
	public const int NOT_TWEET = 0;
	public const int TWEETED = 1;

	private static CharactorListDao sInstance;
	private SQLiteDB mSqliteDB;
	private SQLiteQuery mSqliteQuery;

	public static CharactorListDao getInstance(){
		if(sInstance == null){
			sInstance = ScriptableObject.CreateInstance<CharactorListDao>();
		}
		return sInstance;
	}
	
	public Hashtable getBandmanData(int evolutionPoint){
		OpenDatabase();
		mSqliteQuery  = new SQLiteQuery (mSqliteDB, "select * from charactor_list where id = "+evolutionPoint + ";");
		Hashtable bandmanData = new Hashtable();
		while(mSqliteQuery.Step()){
			string name = mSqliteQuery.GetString(NAME_FIELD);
			int expPoint = mSqliteQuery.GetInteger(EXP_POINT_FIELD);
			string description = mSqliteQuery.GetString(DESCRIPTION_FIELD);
			int flagTweet = mSqliteQuery.GetInteger(FLAG_TWEET_FIELD);
			bandmanData.Add(NAME_FIELD,name);
			bandmanData.Add(EXP_POINT_FIELD,expPoint);
			bandmanData.Add(DESCRIPTION_FIELD,description);
			bandmanData.Add(FLAG_TWEET_FIELD,flagTweet);
		}
		mSqliteQuery.Release ();
		CloseDatabase();
		return bandmanData;
	}

	public void Reset(){
		OpenDatabase();
		mSqliteQuery = new SQLiteQuery (mSqliteDB, "update charactor_list set flag_tweet = 0;");
		mSqliteQuery.Step ();
		mSqliteQuery.Release ();
		CloseDatabase();
	}

	public void UpdateFlagTweet (int id,int flag){
		OpenDatabase();
		mSqliteQuery = new SQLiteQuery (mSqliteDB, "update charactor_list set flag_tweet = " + flag + " where id = " +id+";");
		mSqliteQuery.Step ();
		mSqliteQuery.Release ();
		CloseDatabase();
	}

	private void OpenDatabase(){
		string filename = Application.persistentDataPath + "/bandman.db";
		mSqliteDB = new SQLiteDB();
		mSqliteDB.Open(filename);
	}

	private void releaseQuery(){
		mSqliteQuery.Release();
	}
	
	private void CloseDatabase(){
		mSqliteDB.Close();
	}
}
