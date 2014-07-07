using UnityEngine;

/**
 *	Message Controller on Debug and Release
 *	@ver		1.0.0
 *	@see	[File]->[Build Settings..]->Delelopment Build Check box
 */
public sealed class DebugMessage {

	/**
	 *	log
	 *	@see	UnityEngine.Debug.log
	 */
	public static void log(string msg) {
		if (isDebug()) {
			Debug.Log(msg);
		}
	}

	/**
	 *	warning
	 *	@see	UnityEngine.Debug.warning
	 */
	public static void warning(string msg) {
		if (isDebug()) {
			Debug.LogWarning(msg);
		}
	}

	/**
	 *	error
	 *	@see	UnityEngine.Debug.error
	 */
	public static void error(string msg, Object obj = null) {
		if (isDebug()) {
			Debug.LogError(msg, obj);
		}
	}

	/**
	 *	isDebug
	 *	@see	UnityEngine.Debug.isDebugBuild
	 *	@see	UnityEngine. Application.isEditor
	 */
    private static bool isDebug() {
    	return (Debug.isDebugBuild || Application.isEditor);
 
    	// INFO:
    	// If you do not want to display the debugging information if, you should always rewrite to return false.
    	// return false;
    }

}
