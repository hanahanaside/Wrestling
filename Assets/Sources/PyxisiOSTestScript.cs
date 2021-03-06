using UnityEngine;
using System.Collections;

/**
 *	Pyxis Unity Pluginを簡易にテストするためのサンプルコードです。
 *	GameObjectに紐付けを行い、実行時にボタンを押すだけです。
 *	@ver	1.0.0
 *	@auther	株式会社セプテーニ
 */
public class PyxisiOSTestScript : MonoBehaviour {

	/**
	 *	この値はUnityEditorのプロパティで変更して下さい。
	 *	ソースコードからの修正は効きません。
	 */
	public string cpi_mv = "";
	public string cpi_suid = "";
	public string cpi_sales = "";
	public string cpi_volume = "";
	public string cpi_profit = "";
	public string cpi_others = "";

	public string app_mv = "";
	public string app_suid = "";
	public string app_sales = "";
	public string app_volume = "";
	public string app_profit = "";
	public string app_others = "";
	public string app_limit_mode = "";

	void Awake() {
		// initTrackはできるだけ早い段階で呼び出しを行います。
		PyxisiOSPlugin.initTrack();
		
		PyxisiOSPlugin.setOptOut("0");

		/*
	 	 * DAU計測をされる場合には、initTrack ()の呼び出しの直後に記述するとともに、onApplicationPauseメソッド内に同じ命令を記述してください。
	 	 */
		/*PyxisiOSPlugin.saveAndSendTrackApp(
			" mv を記入してください",
			"",
			"",
			"",
			"",
			"",
			"DAU" );*/

		// initTrackからできるだけ早い段階でtrackInstallを呼び出します。
		PyxisiOSPlugin.trackInstall(
								cpi_mv,
								cpi_suid,
								cpi_sales,
								cpi_volume,
								cpi_profit, 
								cpi_others);
	
		/*
		 * DAU計測をされる場合には、trackInstall ()の呼び出しの直後に以下を記述してください。
	 	*/
		/*PyxisiOSPlugin.saveAndSendTrackApp( 
			                                   " mvを記入してください",
			                                   "",
			                                   "",
			                                   "",
			                                   "",
			                                   " ",
			                                   "DAU"); 
		*/
	}

	void OnGUI() {
		GUI.Label(new Rect(0, 0, 400, 30), "Pyxis Unity Plugin Sample ver 1.00");

		GUI.TextField(new Rect(10, 30, 200, 20), "cpi_mv = " + cpi_mv, 64);
		GUI.TextField(new Rect(10, 50, 200, 20), "cpi_suid = " + cpi_suid, 30);
		GUI.TextField(new Rect(10, 70, 200, 20), "cpi_sales = " + cpi_sales, 30);
		GUI.TextField(new Rect(10, 90, 200, 20), "cpi_volume = " + cpi_volume, 30);
		GUI.TextField(new Rect(10, 110, 200, 20), "cpi_profit = " + cpi_profit, 30);
		GUI.TextField(new Rect(10, 130, 200, 20), "cpi_others = " + cpi_others, 30);

		GUI.TextField(new Rect(210, 30, 200, 20), "app_mv = " + app_mv, 64);
		GUI.TextField(new Rect(210, 50, 200, 20), "app_suid = " + app_suid, 30);
		GUI.TextField(new Rect(210, 70, 200, 20), "app_sales = " + app_sales, 30);
		GUI.TextField(new Rect(210, 90, 200, 20), "app_volume = " + app_volume, 30);
		GUI.TextField(new Rect(210, 110, 200, 20), "app_profit = " + app_profit, 30);
		GUI.TextField(new Rect(210, 130, 200, 20), "app_others = " + app_others, 30);
		GUI.TextField(new Rect(210, 150, 200, 20), "app_limit_mode = " + app_limit_mode, 30);
		
		if (GUI.Button(new Rect(10, 180, 400, 70), "saveTrackApp\n成果の保存"))
		{
			PyxisiOSPlugin.saveTrackApp(
								app_mv,
								app_suid,
								app_sales,
								app_volume,
								app_profit,
								app_others,
								app_limit_mode);
		}
		if (GUI.Button(new Rect(10, 250, 400, 70), "sendTrackApp\n成果の送信"))
		{
			PyxisiOSPlugin.sendTrackApp();
		} 
		
		if (GUI.Button(new Rect(10, 320, 400, 70), "saveAndSendTrackApp\n成果の保存と送信"))
		{
			PyxisiOSPlugin.saveAndSendTrackApp(
								app_mv,
								app_suid,
								app_sales,
								app_volume,
								app_profit,
								app_others,
								app_limit_mode);
		}
		
		/*
		 *  以下はfacebookへの成果送信のテスト用ボタンです。
		 *  テストしたい項目のコメントを外して使用 してください。
		*/
		 /*
		 if (GUI.Button(new Rect(10, 390, 100, 70), "_valueToSum\n1.1"))
		{
			PyxisiOSPlugin.setValueToSum("1.1");
		} 
		if (GUI.Button(new Rect(110, 390, 100, 70), "fb_level\n123"))
		{
			PyxisiOSPlugin.setFbLevel("123");
		} 
		if (GUI.Button(new Rect(210, 390, 100, 70), "fb_success\n0"))
		{
			PyxisiOSPlugin.setFbSuccess("0");
		} 
		if (GUI.Button(new Rect(310, 390, 100, 70), "fb_content_type\nコンテントタイプ"))
		{
			PyxisiOSPlugin.setFbContentType("コンテントタイプ");
		} 
		if (GUI.Button(new Rect(10, 460, 100, 70), "fb_content_id\nコンテントアイディ"))
		{
			PyxisiOSPlugin.setFbContentId("コンテントアイディ");
		} 
		if (GUI.Button(new Rect(110, 460, 100, 70), "fb_registration_method\nレジストメソッド"))
		{
			PyxisiOSPlugin.setFbRegistrationMethod("レジストメソッド");
		} 
		if (GUI.Button(new Rect(210, 460, 100, 70), "fb_payment_info_available\n1"))
		{
			PyxisiOSPlugin.setFbPaymentInfoAvailable("1");
		} 
		if (GUI.Button(new Rect(310, 460, 100, 70), "fb_max_rationg_value\n123"))
		{
			PyxisiOSPlugin.setFbMaxRatingValue("123 ");
		} 
		if (GUI.Button(new Rect(10, 530, 100, 70), "fb_num_items\n123"))
		{
			PyxisiOSPlugin.setFbNumItems("123");
		} 
		if (GUI.Button(new Rect(110, 530, 100, 70), "fb_search_string\nサーチストリング"))
		{
			PyxisiOSPlugin.setFbSearchString("サーチストリング");
		} 
		if (GUI.Button(new Rect(210, 530, 100, 70), "fb_description\nディスクリプション"))
		{
			PyxisiOSPlugin.setFbDescription("ディスクリプション"); 
		} 
		*/
	}

	/*
	 * DAU計測をされる場合には、onApplicationPauseメソッド内で以下を記述してください。
	 */
	/*void OnApplicationPause (bool pauseStatus) 
	{
			PyxisiOSPlugin.saveAndSendTrackApp( 
			                                   " mvを記入してください",
			                                   "",
			                                   "",
			                                   "",
			                                   "",
			                                   " ",
			                                   "DAU"); 
	}*/
}
