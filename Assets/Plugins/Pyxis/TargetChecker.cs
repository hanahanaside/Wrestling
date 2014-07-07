using UnityEngine;
using System.Collections;
/**
 *	TargetChecker
 *	@ver	1.0.0
 */
public sealed class TargetChecker {

	public static bool isiOS() {
		return (Application.platform == RuntimePlatform.IPhonePlayer);
	}

	public static bool isAndroid() {
		return (Application.platform == RuntimePlatform.Android);
	}

	public static bool isPcEditor() {
		return (Application.platform == RuntimePlatform.OSXEditor) ||
				(Application.platform == RuntimePlatform.WindowsEditor);
	}

	public static bool _isiOS() {
		return SystemInfo.operatingSystem.Contains("iPhone");
	}

	public static bool _isAndroid() {
		return SystemInfo.operatingSystem.Contains("Android");
	}

}
