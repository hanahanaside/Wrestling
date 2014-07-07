using UnityEngine;
using System.Collections;

public class HarajukuSoundManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		int soundMode = PrefsManager.getInstance ().GetSoundMode ();
		switch (soundMode) {
		case PrefsManager.SOUND_ON:
			setMute(false);
			GameObject.Find("BGM").audio.Play();
			break;
		case PrefsManager.SOUND_OFF:
			setMute(true);
			break;
		}
	}

	private void setMute(bool muteState){
		GameObject.Find("BGM").audio.mute = muteState;
		GameObject.Find("Pon").audio.mute = muteState;
	}


}
