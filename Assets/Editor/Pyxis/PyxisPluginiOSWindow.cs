using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections;
using System.Text.RegularExpressions;

/**
 *	PyxisUnityPlugin Unity3.5.x用のXCodeビルドユーティリティです。
 *	Unity3.5.xにPyxis for iOS専用の拡張メニューとウィンドウを追加します。
 *	MacOSX 10.7 Lion以降を推奨します。Windows環境には対応していません。
 *	このファイルは/Assets/Editor/Pyxis/直下に配置し、移動や削除をしないように注意して下さい。
 *	@auther		株式会社セプテーニ
 *	@version	1.0.0
 */
public sealed class PyxisPluginiOSWindow : EditorWindow {

	////////////////////////////////////////////////////////////////////////////
	// Unityメニュー・ウィンドウ拡張
	////////////////////////////////////////////////////////////////////////////

	// Unityに追加される拡張メニューの表示文字列です。ここだけconst必須です。
	private const string m_menu_str = "PyxisPlugin/XCode Params for Pyxis...";

	// 拡張ダイアログのタイトル文字列です。
	private static string m_title_str = "PyxisPlugin";

	// ダイアログの最小サイズです。
	private static int m_min_win_width = 300;
	private static int m_min_win_height = 400;

	////////////////////////////////////////////////////////////////////////////
	// pyxisconfg.dat設定ファイルへの保存
	////////////////////////////////////////////////////////////////////////////

	// 設定を保存するファイルパスです。
	private static string m_config_file =
		Application.dataPath + "/Editor/Pyxis/pyxisconfig.dat";

	// 設定ファイルの変数名です。
	private static string m_config_identifier = "URL identifier";
	private static string m_config_scheme = "URL schemes";
	private static string m_config_idn = "jp.co.septeni.pyxis.idn";
	private static string m_config_scm = "jp.co.septeni.pyxis.scm";
	private static string m_config_pid = "jp.co.septeni.pyxis.pid";
	private static string m_config_save_max = "jp.co.septeni.pyxis.save_max";
	private static string m_config_save_mode = "jp.co.septeni.pyxis.save_mode";
	private static string m_config_log_mode = "jp.co.septeni.pyxis.log_mode";

	// 変数と値を分けるセパレータです。
	private static string m_config_separate = "=";

	////////////////////////////////////////////////////////////////////////////
	// Pyxis本体用変数
	////////////////////////////////////////////////////////////////////////////

	// XCodeで使われるマクロ定義です。
	private static string m_product_name = "${PRODUCT_NAME}";

	// XCodeで使用する変数群
	private static string identifier = "com.example";
	private static string scheme = "sample";

	// Pyxis専用のinfo.plist変数です。
	private static bool m_is_product_name = false;
	private static string pid = "SEPTENI";
	private static int save_max = 9999;
	private static int save_mode = 0;
	private static bool log_mode = true;

	////////////////////////////////////////////////////////////////////////////
	// EditorPrefs用保存キー
	////////////////////////////////////////////////////////////////////////////

	private static string m_prefs_identifier = "jp.co.septeni.pyxis.identifier";
	private static string m_prefs_scheme = "jp.co.septeni.pyxis.scheme";
	private static string m_prefs_is_product_name = "m_is_product_name";
	private static string m_prefs_pid = "jp.co.septeni.pyxis.pid";
	private static string m_prefs_save_max = "jp.co.septeni.pyxis.save_max";
	private static string m_prefs_save_mode = "jp.co.septeni.pyxis.save_mode";
	private static string m_prefs_log_mode = "jp.co.septeni.pyxis.log_mode";

	////////////////////////////////////////////////////////////////////////////
	// ダイアログGUI用変数
	////////////////////////////////////////////////////////////////////////////

	// save_mode用ポップアップGUIラベルです。
	private static string[] save_mode_labels = {
		"それ以上記録しない",		// 0
		"消去し新規に記録"		// 1
	};
	// save_mode用ポップアップGUI実値です。
	private static int[] save_mode_params = {
		0,						// それ以上記録しない
		1						// 消去し新規に記録
	};

	////////////////////////////////////////////////////////////////////////////
	// Unity派生メンバ関数
	////////////////////////////////////////////////////////////////////////////

	/**
	 *	初期化時イベント関数です。
	 *	@since	1.0.0
	 */
	void Awake() {
		// 起動時にEditorPrefsから値を復元します。
		loadParams();
	}

	/**
	 *	ウィンドウの破棄イベント関数です。
	 *	@since	1.0.0
	 */
	void OnDestroy() {
		// EditorPrefsに値の保存をし、書き出しは行いません。
		saveParams();
	}

	/**
	 *	フォーカス時の挙動です。
	 *	@since	1.0.0
	 */
	void OnFocus() {
		loadParams();
	}

	/**
	 *	アンフォーカス時の挙動です。
	 *	@since	1.0.0
	 */
	void OnLostFocus() {
		saveParams();
	}

	////////////////////////////////////////////////////////////////////////////
	// メニュー制御
	////////////////////////////////////////////////////////////////////////////

	/**
	 *	プロパティウィンドウの表示を行います。
	 *	この関数が存在するとUnity Editorに拡張メニューが表示されます。
	 *	@since	1.0.0
	 */
	[MenuItem(m_menu_str)]
	private static void OpenWindow() {
		// Windowを生成し、タイトルとサイズを設定します。
		PyxisPluginiOSWindow win =
			(PyxisPluginiOSWindow)EditorWindow.GetWindow(typeof(PyxisPluginiOSWindow));
		win.title = m_title_str;
		win.minSize = new Vector2(m_min_win_width, m_min_win_height);
	}

	////////////////////////////////////////////////////////////////////////////
	// メンバ関数
	////////////////////////////////////////////////////////////////////////////

	/**
	 *	XCode用ビルド設定ファイルをダイアログの値で上書きします。
	 *	@since 	1.0.0
	 *	@return	成否
	 */
	private static bool writeConfig() {
		// 書き出し＆プロダクトID判断を行います。
		try {
			StreamWriter writer;
			writer = new StreamWriter(m_config_file);
			writer.WriteLine(m_config_identifier + m_config_separate + identifier);
			if (m_is_product_name) {
				writer.WriteLine(m_config_scheme + m_config_separate + m_product_name);
			}
			else {
				writer.WriteLine(m_config_scheme + m_config_separate + scheme);
			}
			writer.WriteLine(m_config_idn + m_config_separate + identifier);
			if (m_is_product_name) {
				writer.WriteLine(m_config_scm + m_config_separate + m_product_name);
			}
			else {
				writer.WriteLine(m_config_scm + m_config_separate + scheme);
			}
			writer.WriteLine(m_config_pid + m_config_separate + pid);
			writer.WriteLine(m_config_save_max + m_config_separate + save_max);
			writer.WriteLine(m_config_save_mode + m_config_separate + save_mode);
			writer.WriteLine(m_config_log_mode + m_config_separate + log_mode);
			// 最後に閉じます。
			writer.Close();
		}
		catch {
			Debug.LogError("pyxisconfig.datに書き込めません。");
			return false;
		}
		// 成功を戻します。
		return true;
	}

	/**
	 *	UnityEditor用一時退避領域からダイアログに反映させます。
	 *	@since		1.0.0
	 */
	private static void loadParams() {
		identifier = EditorPrefs.GetString(m_prefs_identifier, "com.example");
		scheme = EditorPrefs.GetString(m_prefs_scheme, "sample");
		m_is_product_name = EditorPrefs.GetBool(m_prefs_is_product_name, false);
		pid = EditorPrefs.GetString(m_prefs_pid, "SEPTENI");
		save_max = EditorPrefs.GetInt(m_prefs_save_max, 9999);
		save_mode = EditorPrefs.GetInt(m_prefs_save_mode, 0);
		log_mode = EditorPrefs.GetBool(m_prefs_log_mode, true);
	}

	/**
	 *	ダイアログ上の値をUnityEditor用一時退避領域に保存します。
	 *	@since		1.0.0
	 */
	private static void saveParams() {
		EditorPrefs.SetString(m_prefs_identifier, identifier);
		EditorPrefs.SetString(m_prefs_scheme, scheme);
		EditorPrefs.SetBool(m_prefs_is_product_name, m_is_product_name);
		EditorPrefs.SetString(m_prefs_pid, pid);
		EditorPrefs.SetInt(m_prefs_save_max, save_max);
		EditorPrefs.SetInt(m_prefs_save_mode, save_mode);
		EditorPrefs.SetBool(m_prefs_log_mode, log_mode);
	}

	/**
	 *	ダイアログ上のGUI構成を処理します。
	 *	@since		1.0.0
	 */
    void OnGUI () {
		EditorGUILayout.Space();
		EditorGUILayout.HelpBox("PyxisUnityPluginのXCode用パラメータ設定です。\n実機ビルド前に、各項目に設定し保存ボタンを押して下さい。\n２回目以降は設定不要です。", MessageType.Info);
		EditorGUILayout.Space();

		GUILayout.Label("[info.plist] URL Identifier/Scheme 設定", EditorStyles.boldLabel);
		EditorGUILayout.BeginVertical("Box");
		EditorGUILayout.Space();
		identifier = EditorGUILayout.TextField("URL Identifier", identifier);
		if (m_is_product_name) {
			EditorGUILayout.LabelField("URL Schemes", m_product_name);
		}
		else {
			scheme = EditorGUILayout.TextField("URL Schemes", scheme);
		}
		m_is_product_name = EditorGUILayout.Toggle("PRODUCT_NAMEを適用", m_is_product_name);
		EditorGUILayout.Space();
		EditorGUILayout.EndVertical();

		EditorGUILayout.Space();
		GUILayout.Label("[info.plist] com.example 設定", EditorStyles.boldLabel);
		EditorGUILayout.BeginVertical("Box");
		EditorGUILayout.Space();
		EditorGUILayout.LabelField("idn", identifier);
		if (m_is_product_name) {
			EditorGUILayout.LabelField("scm", m_product_name);
		}
		else {
			EditorGUILayout.LabelField("scm", scheme);
		}
		pid = EditorGUILayout.TextField("pid", pid);
		save_max = EditorGUILayout.IntSlider("save_max", save_max, 1, 9999);
		save_mode = EditorGUILayout.IntPopup("save_mode", save_mode, save_mode_labels, save_mode_params);
		log_mode = EditorGUILayout.Toggle("log_mode", log_mode);
		EditorGUILayout.Space();
		EditorGUILayout.EndVertical();
		EditorGUILayout.Space();
		if (GUILayout.Button("\n値の保存及び、設定ファイルへの書き出し\n"))
		{
			// 値はPrefsに保存しておきます。
			saveParams();
			// 書き込みに失敗する可能性を考慮します。
			if (writeConfig()) {
				EditorUtility.DisplayDialog("PyxisUnityPlugin Utlity","設定の保存と書き出しに成功しました", "OK");
				Debug.Log("[PyxisUnityPlugin Utility] 設定の保存と書き出しに成功しました。" + System.DateTime.Now);
			}
			else {
				EditorUtility.DisplayDialog("PyxisUnityPlugin Utlity","[!!! ERROR !!!]\n設定の保存と書き出しに失敗しました！\nファイルが書き込み禁止になっていないかどうか確認して下さい。", "OK");
				Debug.LogError("[PyxisUnityPlugin Utility] 設定の保存と書き出しに失敗しました。" + System.DateTime.Now);
			}
		}
	}
}
 