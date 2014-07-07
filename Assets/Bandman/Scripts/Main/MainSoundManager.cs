using UnityEngine;
using System.Collections;

public class MainSoundManager : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
		BroadcastSoundMode ();
	}
	
	public void SoundModeChanged ()
	{
		BroadcastSoundMode ();
	}

	private void BroadcastSoundMode ()
	{
		Debug.Log ("sound change");
		int soundMode = PrefsManager.getInstance ().GetSoundMode ();
		switch (soundMode) {
		case PrefsManager.SOUND_ON:
			BroadcastMessage (false);
			GameObject.Find ("BGM").audio.Play ();
			break;
		case PrefsManager.SOUND_OFF:
			BroadcastMessage (true);
			break;
		}
	}

	private void BroadcastMessage (bool muteState)
	{
		GameObject.Find ("BGM").audio.mute = muteState;
		GameObject.Find ("MoneySound").audio.mute = muteState;
		GameObject.Find ("Evolution").audio.mute = muteState;
		GameObject.Find ("Player").SendMessage ("SetMute", muteState);
		GameObject.Find ("LevelUp").audio.mute = muteState;
	}
}
