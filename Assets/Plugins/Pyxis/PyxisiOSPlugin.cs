using UnityEngine;
using System;
using System.Runtime.InteropServices;


/**
 *	PyxisSDKをUnity3Dから使用するためのiOS専用プラグインクラスです。
 *	このファイルはGameObjectに紐付け不可となっています。
 *	また、クラスメソッド(staticメンバ関数)のみで構成されています。
 *	iOS実機環境以外では計測を行わない仕組みになっています。
 *
 *	[ファイル構成]
 *	 StringCode:UTF-16
 *	 ReturnCode:LF
 *	 TAB:4
 *	[Unity3D環境の動作条件]
 *	 Unity3D 3.5.x (3.5.7が3.5.xの最終バージョンです)
 *	 iOSアドオン
 *	 AndroidOSアドオン
 *	 ※iOSアドオンはAndroid環境では動作しません。逆も同様です。
 *	[使用方法]
 *	 使用方法のサンプルはPyxisiOSTestScript.csを参照して下さい。
 *
 *	@author		株式会社セプテーニ
 *	@version	1.0.0
 */
public sealed class PyxisiOSPlugin {
	
	
	/**
	 *	Pyxis Pluginを初期化します。
	 *	このメソッドが呼ばれないと他のメソッドは処理に成功しません。
	 *	お客様の開発アプリ環境において、アプリ起動直後のできるだけ早い段階にインスタンス化されるクラスのAwake()またはStart()で呼ぶことを推奨します。
	 *	戻り値はありません。デバッグコンソールでチェックして下さい。
	 *	@since	1.0
	 */
	public static void initTrack() {

		DebugMessage.log("PyxisiOSPlugin::initTrack()");
		// 多重呼出し、初期化ミス防止フラグを処理します。
		if (!_isInitialized) {
			_isInitialized = true;
			// ターゲットとUnity Editorで挙動を変化させます。
			if (TargetChecker.isiOS()) {
				InitTrack();
			}
			else {
				DebugMessage.warning("iOS上でのみ実行可能です。");
			}
		}
		// INFO:
		// 多重初期化は無視しても問題ないため、メッセージは出しません。
	}

	/**
	 *	Pyxisのインストールを行います。
	 *	initTrackを先に呼ばないと処理が成功しません。
	 *	戻り値はありません。デバッグコンソールでチェックして下さい。
	 *	@param	mv			株式会社セプテーニから渡されたmv値を入れてください。
	 *	@param	suid		御社管理のsuid値を入れてください。Null,空文字,数値以外は無効となります。
	 *	@param	sales		売上金額を指定して下さい。Null,空文字,数値以外は無効となります。
	 *	@param	volume		成果タイプが定率の場合に購入数を指定して下さい。Null,空文字,数値以外は無効となります。
	 *	@param	profit		成果タイプが定率の場合に購金額合計値を指定して下さい。Null,空文字,数値以外は無効となります。
	 *	@param	others		Null,空文字,数値以外は無効となります。
	 *	@since	1.0
	 *	@see initTrack
	 */
	public static void trackInstall(
							string mv,
							string suid,
							string sales,
							string volume,
							string profit,
							string others) {

		DebugMessage.log("PyxisiOSPlugin::trackInstall(mv:" + mv + ", suid:" + suid + ", sales, volume, profit, others)");
		if (!isInitialized()) {
			DebugMessage.error("Pyxisが初期化されていません。InitTrack()メソッドを先に呼び出す必要があります。");
			return;
		}
		if (string.IsNullOrEmpty(mv)) {
			DebugMessage.log("mv引数にNullまたは空文字は指定できません。");
			return;
		}
		if (!string.IsNullOrEmpty(sales)) {
			if (!isNumeric(ref sales)) {
				// Null、空文字、数値以外の場合はエラーとする。
				DebugMessage.error("sales引数にNull、空文字、数値以外は指定できません。 値=" + sales);
			}
		}
		if (!string.IsNullOrEmpty(volume)) {
			if (!isNumeric(ref volume)) {
				// Null、空文字、数値以外の場合はエラーとする。
				DebugMessage.error("volume引数にNull、空文字、数値以外は指定できません。 値=" + volume);
			}
		}
		if (!string.IsNullOrEmpty(profit)) {
			if (!isNumeric(ref profit)) {
				// Null、空文字、数値以外の場合はエラーとする。
				DebugMessage.error("profit引数にNull、空文字、数値以外は指定できません。 値=" + profit);
			}
		}
		if (TargetChecker.isiOS()) {
			TrackInstall(mv, suid, sales, volume, profit, others);
		}
		else { 
			DebugMessage.warning("iOS上でのみ実行可能です。");
		}
	}

	/**
	 *	計測情報をローカルDBに保存します。
	 *	initTrackを先に呼ばないと処理が成功しません。
	 *	戻り値はありません。デバッグコンソールでチェックして下さい。
	 *	呼び出しコストが高いため、Update()等での連続呼び出しは行わないようご注意下さい。
	 *	@param	mv		株式会社セプテーニから渡されたmv値を入れてください。Null,空文字は無効です。
	 *	@param	suid		御社管理のsuid値を入れてください。
	 *	@param	sales		売上金額を指定して下さい。Null,空文字,数値以外は無効となります。
	 *	@param	volume		成果タイプが定率の場合に購入数を指定して下さい。Null,空文字,数値以外は無効となります。
	 *	@param	profit		成果タイプが定率の場合に購金額合計値を指定して下さい。Null,空文字,数値以外は無効となります。
	 *	@param	others		Null,空文字,数値以外は無効となります。
	 *	@param app_limit_mode
	 *	@since	1.0
	 *	@see	initTrack
	 *	@see	trackInstall
	 */
	
	
 	public static void saveTrackApp(
							string mv,
							string suid,
							string sales,
							string volume,
							string profit,
							string others,
							string app_limit_mode) {

		DebugMessage.log("PyxisiOSPlugin::saveTrackApp(mv, suid, sales, volume, profit, others, app_limit_mode)");
		if (!isInitialized()) {
			DebugMessage.error("Pyxisが初期化されていません。InitTrack()メソッドを先に呼び出す必要があります。");
			return;
		}
		if (string.IsNullOrEmpty(mv)) {
			DebugMessage.log("mv引数にNullまたは空文字は指定できません。");
			return;
		}
		if (!string.IsNullOrEmpty(sales)) {
			if (!isNumeric(ref sales)) {
				// Null、空文字、数値以外の場合はエラーとする。
				DebugMessage.error("sales引数にNull、空文字、数値以外は指定できません。 値=" + sales);
			}
		}
		if (!string.IsNullOrEmpty(volume)) {
			if (!isNumeric(ref volume)) {
				// Null、空文字、数値以外の場合はエラーとする。
				DebugMessage.error("volume引数にNull、空文字、数値以外は指定できません。 値=" + volume);
			}
		}
		if (!string.IsNullOrEmpty(profit)) {
			if (!isNumeric(ref profit)) {
				// Null、空文字、数値以外の場合はエラーとする。
				DebugMessage.error("profit引数にNull、空文字、数値以外は指定できません。 値=" + profit);
			}
		}
		if (TargetChecker.isiOS()) {
			SaveTrackApp(mv, suid, sales, volume, profit, others, app_limit_mode);
		}
		else {
			DebugMessage.warning("iOS上でのみ実行可能です。");
		}
	}

	/**
	 *	DBに保存した計測情報をサーバへ送信します。
 	 *	initTrackを先に呼ばないと処理が成功しません。
	 *	@since	1.0
	 *	@see	initTrack
	 *	@see	saveTrackApp
	 */
	public static void sendTrackApp() {
		DebugMessage.log("PyxisiOSPlugin::SendTrackApp");
		if (TargetChecker.isiOS()) {
			SendTrackApp();
		}
		else {
			DebugMessage.warning("iOS上でのみ実行可能です。");
		}
	}
	
	/**
	 *	計測情報をローカルDBに保存した後、成果送信を行います。
	 *	initTrackを先に呼ばないと処理が成功しません。
	 *	戻り値はありません。デバッグコンソールでチェックして下さい。
	 *	呼び出しコストが高いため、Update()等での連続呼び出しは行わないようご注意下さい。
	 *	@param	 mv		株式会社セプテーニから渡されたmv値を入れてください。Null,空文字は無効です。
	 *	@param	suid		御社管理のsuid値を入れてください。
	 *	@param	sales		売上金額を指定して下さい。Null,空文字,数値以外は無効となります。
	 *	@param	volume		成果タイプが定率の場合に購入数を指定して下さい。Null,空文字,数値以外は無効となります。
	 *	@param	profit		成果タイプが定率の場合に購金額合計値を指定して下さい。Null,空文字,数値以外は無効となります。
	 *	@param	others		Null,空文字,数値以外は無効となります。
	 *	@since	2.0
	 *	@see	initTrack
	 *	@see	trackInstall
	 */
	public static void saveAndSendTrackApp(
							string mv,
							string suid,
							string sales,
							string volume,
							string profit,
							string others,
							string app_limit_mode) {

		DebugMessage.log("PyxisiOSPlugin::saveAndSendTrackApp(mv, suid, sales, volume, profit, others, app_limit_mode )");
		if (!isInitialized()) {
			DebugMessage.error("Pyxisが初期化されていません。InitTrack()メソッドを先に呼び出す必要があります。");
			return;
		}
		if (string.IsNullOrEmpty(mv)) {
			DebugMessage.log("mv引数にNullまたは空文字は指定できません。");
			return;
		}
		if (!string.IsNullOrEmpty(sales)) {
			if (!isNumeric(ref sales)) {
				// Null、空文字、数値以外の場合はエラーとする。
				DebugMessage.error("sales引数にNull、空文字、数値以外は指定できません。 値=" + sales);
			}
		}
		if (!string.IsNullOrEmpty(volume)) {
			if (!isNumeric(ref volume)) {
				// Null、空文字、数値以外の場合はエラーとする。
				DebugMessage.error("volume引数にNull、空文字、数値以外は指定できません。 値=" + volume);
			}
		}
		if (!string.IsNullOrEmpty(profit)) {
			if (!isNumeric(ref profit)) {
				// Null、空文字、数値以外の場合はエラーとする。
				DebugMessage.error("profit引数にNull、空文字、数値以外は指定できません。 値=" + profit);
			}
		}
		if (TargetChecker.isiOS()) {
			SaveAndSendTrackApp(mv, suid, sales, volume, profit, others, app_limit_mode);
		}
		else {
			DebugMessage.warning("iOS上でのみ実行可能です。");
		}
	}
	
	/**
	 *	オプトアウトの設定を行います。
	 *	戻り値はありません。デバッグコンソールでチェックして下さい。
	 *	@param	flg		オプトアウトをOnにする場合は1を、Offにする場合は0を指定します。
	 *	@since	2.0
	 */
	public static void setOptOut(string flg) {
		DebugMessage.log("PyxisiOSPlugin::SetOptOut");
		if (TargetChecker.isiOS()) {
			SetOptOut(flg);
		}
		else {
			DebugMessage.warning("iOS上でのみ実行可能です。");
		}
	}
	
	/**
	 *	オプトアウトの設定を取得します。
	 *	戻り値はbool型となります。
	 *	@return bool
	 *	@since	2.0
	 */
	public static bool getOptOut() {
		DebugMessage.log("PyxisiOSPlugin::GetOptOut");
		if (TargetChecker.isiOS()) {
			return GetOptOut();
		}
		else {
			DebugMessage.warning("iOS上でのみ実行可能です。");
		}
		return false;
	}
	
	/**
	 *	facebookに送信する値(_valueToSum)の設定を行います。
	 *	戻り値はありません。デバッグコンソールでチェックして下さい。
	 *	@_valueToSum		_valueToSumに設定したい値を設定します。
	 *	@since	2.0
	 */
	public static void setValueToSum(string _valueToSum) {
		DebugMessage.log("PyxisiOSPlugin::SetValueToSum");
		if (TargetChecker.isiOS()) {
			SetValueToSum(_valueToSum);
		}
		else {
			DebugMessage.warning("iOS上でのみ実行可能です。");
		}
	}
	
	/**
	 *	facebookに送信する値(fb_level)の設定を行います。
	 *	戻り値はありません。デバッグコンソールでチェックして下さい。
	 *	@fb_level		fb_levelに設定したい値を設定します。
	 *	@since	2.0
	 */
	public static void setFbLevel(string fb_level) {
		DebugMessage.log("PyxisiOSPlugin::SetFbLevel");
		if (TargetChecker.isiOS()) {
			SetFbLevel(fb_level);
		}
		else {
			DebugMessage.warning("iOS上でのみ実行可能です。");
		}
	}
	
	/**
	 *	facebookに送信する値(fb_success)の設定を行います。
	 *	戻り値はありません。デバッグコンソールでチェックして下さい。
	 *	@fb_success		fb_successに設定したい値を設定します。
	 *	@since	2.0
	 */
	public static void setFbSuccess(string fb_success) {
		DebugMessage.log("PyxisiOSPlugin::SetFbSuccess");
		if (TargetChecker.isiOS()) {
			SetFbSuccess(fb_success);
		}
		else {
			DebugMessage.warning("iOS上でのみ実行可能です。");
		}
	}
	
	/**
	 *	facebookに送信する値(fb_content_type)の設定を行います。
	 *	戻り値はありません。デバッグコンソールでチェックして下さい。
	 *	@fb_content_type		fb_content_typeに設定したい値を設定します。
	 *	@since	2.0
	 */
	public static void setFbContentType(string fb_content_type) {
		DebugMessage.log("PyxisiOSPlugin::SetFbContentType");
		if (TargetChecker.isiOS()) {
			SetFbContentType(fb_content_type);
		}
		else {
			DebugMessage.warning("iOS上でのみ実行可能です。");
		}
	}
	
	/**
	 *	facebookに送信する値(fb_content_id)の設定を行います。
	 *	戻り値はありません。デバッグコンソールでチェックして下さい。
	 *	@fb_content_id		fb_content_idに設定したい値を設定します。
	 *	@since	2.0
	 */
	public static void setFbContentId(string fb_content_id) {
		DebugMessage.log("PyxisiOSPlugin::SetFbContentId");
		if (TargetChecker.isiOS()) {
			SetFbContentId(fb_content_id);
		}
		else {
			DebugMessage.warning("iOS上でのみ実行可能です。");
		}
	}
	
	/**
	 *	facebookに送信する値(fb_registration_method)の設定を行います。
	 *	戻り値はありません。デバッグコンソールでチェックして下さい。
	 *	@fb_registration_method		fb_registration_methodに設定したい値を設定します。
	 *	@since	2.0
	 */
	public static void setFbRegistrationMethod(string fb_registration_method) {
		DebugMessage.log("PyxisiOSPlugin::SetFbRegistrationMethod");
		if (TargetChecker.isiOS()) {
			SetFbRegistrationMethod(fb_registration_method);
		}
		else {
			DebugMessage.warning("iOS上でのみ実行可能です。");
		}
	}
	
	/**
	 *	facebookに送信する値(fb_payment_info_available)の設定を行います。
	 *	戻り値はありません。デバッグコンソールでチェックして下さい。
	 *	@fb_payment_info_available		fb_payment_info_availableに設定したい値を設定します。
	 *	@since	2.0
	 */
	public static void setFbPaymentInfoAvailable(string fb_payment_info_available) {
		DebugMessage.log("PyxisiOSPlugin::SetFbPaymentInfoAvailable");
		if (TargetChecker.isiOS()) {
			SetFbPaymentInfoAvailable(fb_payment_info_available);
		}
		else {
			DebugMessage.warning("iOS上でのみ実行可能です。");
		}
	}
	
	/**
	 *	facebookに送信する値(fb_max_rating_value)の設定を行います。
	 *	戻り値はありません。デバッグコンソールでチェックして下さい。
	 *	@fb_max_rating_value		fb_max_rating_valueに設定したい値を設定します。
	 *	@since	2.0
	 */
	public static void setFbMaxRatingValue(string fb_max_rating_value) {
		DebugMessage.log("PyxisiOSPlugin::SetFbMaxRatingValue");
		if (TargetChecker.isiOS()) {
			SetFbMaxRatingValue(fb_max_rating_value);
		}
		else {
			DebugMessage.warning("iOS上でのみ実行可能です。");
		}
	}
	
	/**
	 *	facebookに送信する値(fb_num_items)の設定を行います。
	 *	戻り値はありません。デバッグコンソールでチェックして下さい。
	 *	@fb_num_items		fb_num_itemsに設定したい値を設定します。
	 *	@since	2.0
	 */
	public static void setFbNumItems(string fb_num_items) {
		DebugMessage.log("PyxisiOSPlugin::SetFbNumItems");
		if (TargetChecker.isiOS()) {
			SetFbNumItems(fb_num_items);
		}
		else {
			DebugMessage.warning("iOS上でのみ実行可能です。");
		}
	}
	
	/**
	 *	facebookに送信する値(fb_search_string)の設定を行います。
	 *	戻り値はありません。デバッグコンソールでチェックして下さい。
	 *	@fb_search_string		fb_search_stringに設定したい値を設定します。
	 *	@since	2.0
	 */
	public static void setFbSearchString(string fb_search_string) {
		DebugMessage.log("PyxisiOSPlugin::SetFbSearchString");
		if (TargetChecker.isiOS()) {
			SetFbSearchString(fb_search_string);
		}
		else {
			DebugMessage.warning("iOS上でのみ実行可能です。");
		}
	}
	
	/**
	 *	facebookに送信する値(fb_description)の設定を行います。
	 *	戻り値はありません。デバッグコンソールでチェックして下さい。
	 *	@fb_description		fb_descriptionに設定したい値を設定します。
	 *	@since	2.0
	 */
	public static void setFbDescription(string fb_description) {
		DebugMessage.log("PyxisiOSPlugin::SetFbDescription");
		if (TargetChecker.isiOS()) {
			SetFbDescription(fb_description);
		}
		else {
			DebugMessage.warning("iOS上でのみ実行可能です。");
		}
	}
	
	
	/**
	 *	PyxisPlugin自体のバージョンを取得します。
	 *	@since	1.0
	 *	@see	pluginVer
	 */
	public static int GetUnityPluginVersion() {
		DebugMessage.log("PyxisiOSPlugin::GetUnityPluginVersion()");
		return _pluginVer;
	}

////////////////////////////////////////////////////////////////////////////////
//	内部処理用メソッド
////////////////////////////////////////////////////////////////////////////////

	/**
	 *	Pyxisライブラリの初期化状態を調べます。
	 *	初期化済みの状態なら真を返します。
	 *	このメソッドが真を返さないと、Pyxisライブラリの殆どのメソッドは呼び出しに失敗します。
	 *	@param	param	変換したい文字列を渡します。
	 *	@since	1.0
	 */
	public static bool isInitialized() {
		return _isInitialized;
	}

	/**
	 *	中身が数値かどうかを調べます。
	 *	@pre	引数がnull又は空文字でないこと
	 *	@param	param	検査したい文字列を渡します。
	 *	@since	1.0
	 */
	private static bool isNumeric(ref string param) {
		int result;
		return int.TryParse(param, out result);
	}

////////////////////////////////////////////////////////////////////////////////
//	内部処理用メンバ変数群
////////////////////////////////////////////////////////////////////////////////

	/**
	 *	初期化制御フラグです。
	 *	多重初期化、及び初期化忘れの防止に使用しています。
	 *	@see	init()
	 *	@since	1.0
	 */
	private static bool _isInitialized = false;

	/**
	 *	このプラグイン自体のバージョンです。
	 *	Pyxisライブラリとの区別を付けるために設定してあります。
	 *	なお、この値は百倍されたものになっています。
	 *	バージョンが1.00の場合は100となります。
	 *	@see	getPyxisPluginVersion()
	 *	@since	1.0
	 */
	private static int _pluginVer = 200;

////////////////////////////////////////////////////////////////////////////////
// [WARNING] ここから下を編集すると問題が発生する可能性があります。
// 触らないようご注意下さい。
////////////////////////////////////////////////////////////////////////////////

////////////////////////////////////////////////////////////////////////////////
// この下はUnity3.5.xのiOS実機環境でのみ動作します。
// 実機環境依存コードは多重処理等のチェックを行なっていません。
// コード修正等による直接呼び出しにはご注意下さい。
#if (UNITY_IPHONE && !UNITY_EDITOR)

	/**
	 *	Pyxis Pluginを初期化します。
	 *	戻り値はありません。デバッグコンソールでチェックして下さい。
	 *	@since	1.0
	 */
	[DllImport("__Internal")]
	public static extern void InitTrack();

	/**
	 *	ライブラリに情報をインストールします。
	 *	戻り値はありません。デバッグコンソールでチェックして下さい。
	 *	@since	1.0
	 */
	[DllImport("__Internal")]
	public static extern void TrackInstall(
								string mv,
								string suid,
								string sales,
								string volume,
								string profit,
								string others);

	/**
	 *	Pyxisライブラリが管理するローカルDBに情報を保存します。
	 *	呼び出しコストが高いため、Update()等での連続呼び出しは行わないようご注意下さい。
	 *	戻り値はありません。デバッグコンソールでチェックして下さい。
	 *	@param	mv		株式会社セプテーニから渡されたmv値を入れてください。
	 *	@param	suid		御社管理のsuid値を入れてください。
	 *	@since	1.0
	 */
	[DllImport("__Internal")]
	public static extern void SaveTrackApp(
								string mv,
								string suid,
								string sales,
								string volume,
								string profit,
								string others,
								string app_limit_mode);

	/**
	 *	ローカルDBに保存した情報をサーバへ送信します。
	 *	呼び出しコストが高いため、Update()等での連続呼び出しは行わないようご注意下さい。
	 *	戻り値はありません。デバッグコンソールでチェックして下さい。
	 *	@since	1.0
	 */
	[DllImport("__Internal")]
	public static extern void SendTrackApp();
	
	/**
	 *	計測情報をローカルDBに保存した後、成果送信を行います。
	 *	initTrackを先に呼ばないと処理が成功しません。
	 *	戻り値はありません。デバッグコンソールでチェックして下さい。
	 *	呼び出しコストが高いため、Update()等での連続呼び出しは行わないようご注意下さい。
	 *	@param	mv		株式会社セプテーニから渡されたmv値を入れてください。Null,空文字は無効です。
	 *	@param	suid		御社管理のsuid値を入れてください。
	 *	@param	sales		売上金額を指定して下さい。Null,空文字,数値以外は無効となります。
	 *	@param	volume		成果タイプが定率の場合に購入数を指定して下さい。Null,空文字,数値以外は無効となります。
	 *	@param	 profit		成果タイプが定率の場合に購金額合計値を指定して下さい。Null,空文字,数値以外は無効となります。
	 *	@param	others		Null,空文字,数値以外は無効となります。
	 *	@since	1.0
	 *	@see	initTrack
	 *	@see	trackInstall
	 */
	[DllImport("__Internal")]
	public static extern void SaveAndSendTrackApp(
								string mv,
								string suid,
								string sales,
								string volume,
								string profit,
								string others,
								string app_limit_mode);
	
	/**
	 *	オプトアウトの設定を行います。
	 *	戻り値はありません。デバッグコンソールでチェックして下さい。
	 *	@param	flg		オプトアウトをOnにする場合は1を、Offにする場合は0を指定します。
	 *	@since	2.0
	 */
	[DllImport("__Internal")]
	public static extern void SetOptOut(string flg);
	
	/**
	 *	オプトアウトの設定を取得します。
	 *	戻り値はbool型となります。
	 *	@return bool
	 *	@since	2.0
	 */
	[DllImport("__Internal")]
	public static extern bool GetOptOut();
	
	/**
	 *	facebookに送信する値(_valueToSum)の設定を行います。
	 *	戻り値はありません。デバッグコンソールでチェックして下さい。
	 *	@_valueToSum		_valueToSumに設定したい値を設定します。
	 *	@since	2.0
	 */
	[DllImport("__Internal")]
	public static extern void SetValueToSum(string _valueToSum);
	
	/**
	 *	facebookに送信する値(fb_level)の設定を行います。
	 *	戻り値はありません。デバッグコンソールでチェックして下さい。
	 *	@fb_level		fb_levelに設定したい値を設定します。
	 *	@since	2.0
	 */
	[DllImport("__Internal")]
	public static extern void SetFbLevel(string fb_level);
	
	/**
	 *	facebookに送信する値(fb_success)の設定を行います。
	 *	戻り値はありません。デバッグコンソールでチェックして下さい。
	 *	@fb_success		trueを設定する場合は1を、falseを設定する場合は0を指定します。
	 *	@since	2.0
	 */
	[DllImport("__Internal")]
	public static extern void SetFbSuccess(string fb_success);
	
	/**
	 *	facebookに送信する値(fb_content_type)の設定を行います。
	 *	戻り値はありません。デバッグコンソールでチェックして下さい。
	 *	@fb_content_type		fb_content_typeに設定したい値を設定します。
	 *	@since	2.0
	 */
	[DllImport("__Internal")]
	public static extern void SetFbContentType(string fb_content_type);
	
	/**
	 *	facebookに送信する値(fb_content_id)の設定を行います。
	 *	戻り値はありません。デバッグコンソールでチェックして下さい。
	 *	@fb_content_id		fb_content_idに設定したい値を設定します。
	 *	@since	2.0
	 */
	[DllImport("__Internal")]
	public static extern void SetFbContentId(string fb_content_id);
	
	/**
	 *	facebookに送信する値(fb_registration_method)の設定を行います。
	 *	戻り値はありません。デバッグコンソールでチェックして下さい。
	 *	@fb_registration_method		fb_registration_methodに設定したい値を設定します。
	 *	@since	2.0
	 */
	[DllImport("__Internal")]
	public static extern void SetFbRegistrationMethod(string fb_registration_method);
	
	/**
	 *	facebookに送信する値(fb_payment_info_available)の設定を行います。
	 *	戻り値はありません。デバッグコンソールでチェックして下さい。
	 *	@fb_payment_info_available		trueを設定する場合は1を、falseを設定する場合は0を指定します。
	 *	@since	2.0
	 */
	[DllImport("__Internal")]
	public static extern void SetFbPaymentInfoAvailable(string fb_payment_info_available);
	
	/**
	 *	facebookに送信する値(fb_max_rating_value)の設定を行います。
	 *	戻り値はありません。デバッグコンソールでチェックして下さい。
	 *	@fb_max_rating_value		fb_max_rating_valueに設定したい値を設定します。
	 *	@since	2.0
	 */
	[DllImport("__Internal")]
	public static extern void SetFbMaxRatingValue(string fb_max_rating_value);
	
	/**
	 *	facebookに送信する値(fb_num_items)の設定を行います。
	 *	戻り値はありません。デバッグコンソールでチェックして下さい。
	 *	@fb_num_items		fb_num_itemsに設定したい値を設定します。
	 *	@since	2.0
	 */
	[DllImport("__Internal")]
	public static extern void SetFbNumItems(string fb_num_items);
	
	/**
	 *	facebookに送信する値(fb_search_string)の設定を行います。
	 *	戻り値はありません。デバッグコンソールでチェックして下さい。
	 *	@fb_search_string		fb_search_stringに設定したい値を設定します。
	 *	@since	2.0
	 */
	[DllImport("__Internal")]
	public static extern void SetFbSearchString(string fb_search_string);
	
	/**
	 *	facebookに送信する値(fb_description)の設定を行います。
	 *	戻り値はありません。デバッグコンソールでチェックして下さい。
	 *	@fb_description		fb_descriptionに設定したい値を設定します。
	 *	@since	2.0
	 */
	[DllImport("__Internal")]
	public static extern void SetFbDescription(string fb_description);
	
#else	// (UNITY_IPHONE && !UNITY_EDITOR)

////////////////////////////////////////////////////////////////////////////////
// Unity Editor及び他の環境でのエラー回避用ダミー関数です。
// 何度呼び出しても影響はありません。

	/**
	 *	Pyxis Pluginを初期化します。
	 *	戻り値はありません。デバッグコンソールでチェックして下さい。
	 *	@since	1.0
	 */
	public static void InitTrack() {
		DebugMessage.warning("iOS上でのみ実行可能です。");
	}

	/**
	 *	ライブラリに情報をインストールします。
	 *	戻り値はありません。デバッグコンソールでチェックして下さい。
	 *	@since	1.0
	 */
	public static void TrackInstall(
								string mv,
								string suid,
								string sales,
								string volume,
								string profit,
								string others) {
		DebugMessage.warning("iOS上でのみ実行可能です。");
	}

	/**
	 *	Pyxisライブラリが管理するローカルDBに情報を保存します。
	 *	呼び出しコストが高いため、Update()等での連続呼び出しは行わないようご注意下さい。
	 *	戻り値はありません。デバッグコンソールでチェックして下さい。
	 *	@since	1.0
	 */
	public static void SaveTrackApp(
								string mv,
								string suid,
								string sales,
								string volume,
								string profit,
								string others,
								string app_limit_mode) {
		DebugMessage.warning("iOS上でのみ実行可能です。");
	}

	/**
	 *	ローカルDBに保存した情報をサーバへ送信します。
	 *	呼び出しコストが高いため、Update()等での連続呼び出しは行わないようご注意下さい。
	 *	戻り値はありません。デバッグコンソールでチェックして下さい。
	 *	@since	1.0
	 */
	public static void SendTrackApp() {
		DebugMessage.warning("iOS上でのみ実行可能です。");
	}
	
	/**
	 *	Pyxisライブラリが管理するローカルDBに情報を保存した後、成果送信を行います。
	 *	呼び出しコストが高いため、Update()等での連続呼び出しは行わないようご注意下さい。
	 *	戻り値はありません。デバッグコンソールでチェックして下さい。
	 *	@since	1.0
	 */
	public static void SaveAndSendTrackApp(
								string mv,
								string suid,
								string sales,
								string volume,
								string profit,
								string others,
								string app_limit_mode) {
		DebugMessage.warning("iOS上でのみ実行可能です。");
	}
	
	/**
	 *	オプトアウトの設定を行います。
	 *	戻り値はありません。デバッグコンソールでチェックして下さい。
	 *	@param	flg		オプトアウトをOnにする場合は1を、Offにする場合は0を指定します。
	 *	@since	2.0
	 */
	public static void SetOptOut(string flg) {
		DebugMessage.warning("iOS上でのみ実行可能です。");
	}
	
	/**
	 *	オプトアウトの設定を取得します。
	 *	戻り値はbool型となります。
	 *	@return bool
	 *	@since	2.0
	 */
	public static bool GetOptOut() {
		DebugMessage.warning("iOS上でのみ実行可能です。強制的にfalseを返却します。");
		return false;
	}
	
	/**
	 *	facebookに送信する値(_valueToSum)の設定を行います。
	 *	戻り値はありません。デバッグコンソールでチェックして下さい。
	 *	@_valueToSum		_valueToSumに設定したい値を設定します。
	 *	@since	2.0
	 */
	public static void SetValueToSum(string _valueToSum) {
		DebugMessage.warning("iOS上でのみ実行可能です。");
	}
	
	/**
	 *	facebookに送信する値(fb_level)の設定を行います。
	 *	戻り値はありません。デバッグコンソールでチェックして下さい。
	 *	@fb_level		fb_levelに設定したい値を設定します。
	 *	@since	2.0
	 */
	public static void SetFbLevel(string fb_level) {
		DebugMessage.warning("iOS上でのみ実行可能です。");
	}
	
	/**
	 *	facebookに送信する値(fb_success)の設定を行います。
	 *	戻り値はありません。デバッグコンソールでチェックして下さい。
	 *	@fb_success		trueを設定する場合は1を、falseを設定する場合は0を指定します。
	 *	@since	2.0
	 */
	public static void SetFbSuccess(string fb_success) {
		DebugMessage.warning("iOS上でのみ実行可能です。");
	}
	
	/**
	 *	facebookに送信する値(fb_content_type)の設定を行います。
	 *	戻り値はありません。デバッグコンソールでチェックして下さい。
	 *	@fb_content_type		fb_content_typeに設定したい値を設定します。
	 *	@since	2.0
	 */
	public static void SetFbContentType(string fb_content_type) {
		DebugMessage.warning("iOS上でのみ実行可能です。");
	}
	
	/**
	 *	facebookに送信する値(fb_content_id)の設定を行います。
	 *	戻り値はありません。デバッグコンソールでチェックして下さい。
	 *	@fb_content_id		fb_content_idに設定したい値を設定します。
	 *	@since	2.0
	 */
	public static void SetFbContentId(string fb_content_id) {
		DebugMessage.warning("iOS上でのみ実行可能です。");
	}
	
	/**
	 *	facebookに送信する値(fb_registration_method)の設定を行います。
	 *	戻り値はありません。デバッグコンソールでチェックして下さい。
	 *	@fb_registration_method		fb_registration_methodに設定したい値を設定します。
	 *	@since	2.0
	 */
	public static void SetFbRegistrationMethod(string fb_registration_method) {
		DebugMessage.warning("iOS上でのみ実行可能です。");
	}
	
	/**
	 *	facebookに送信する値(fb_payment_info_available)の設定を行います。
	 *	戻り値はありません。デバッグコンソールでチェックして下さい。
	 *	@fb_payment_info_available		trueを設定する場合は1を、falseを設定する場合は0を指定します。
	 *	@since	2.0
	 */
	public static void SetFbPaymentInfoAvailable(string fb_payment_info_available) {
		DebugMessage.warning("iOS上でのみ実行可能です。");
	}
	
	/**
	 *	facebookに送信する値(fb_max_rating_value)の設定を行います。
	 *	戻り値はありません。デバッグコンソールでチェックして下さい。
	 *	@fb_max_rating_value		fb_max_rating_valueに設定したい値を設定します。
	 *	@since	2.0
	 */
	public static void SetFbMaxRatingValue(string fb_max_rating_value) {
		DebugMessage.warning("iOS上でのみ実行可能です。");
	}
	
	/**
	 *	facebookに送信する値(fb_num_items)の設定を行います。
	 *	戻り値はありません。デバッグコンソールでチェックして下さい。
	 *	@fb_num_items		fb_num_itemsに設定したい値を設定します。
	 *	@since	2.0
	 */
	public static void SetFbNumItems(string fb_num_items) {
		DebugMessage.warning("iOS上でのみ実行可能です。");
	}
	
	/**
	 *	facebookに送信する値(fb_search_string)の設定を行います。
	 *	戻り値はありません。デバッグコンソールでチェックして下さい。
	 *	@fb_search_string		fb_search_stringに設定したい値を設定します。
	 *	@since	2.0
	 */
	public static void SetFbSearchString(string fb_search_string) {
		DebugMessage.warning("iOS上でのみ実行可能です。");
	}
	
	/**
	 *	facebookに送信する値(fb_description)の設定を行います。
	 *	戻り値はありません。デバッグコンソールでチェックして下さい。
	 *	@fb_description		fb_descriptionに設定したい値を設定します。
	 *	@since	2.0
	 */
	public static void SetFbDescription(string fb_description) {
		DebugMessage.warning("iOS上でのみ実行可能です。");
	}
	
#endif	// (UNITY_IPHONE && !UNITY_EDITOR)

}

///////////////////////////////////////////////////////////////////////////////
// 更新履歴
// 
// ver 1.0.0	2013/10/04		初期作成
