using UnityEngine;
using System.Collections;

public class PlayerVoice : MonoBehaviour
{

	public AudioClip[] positiveVoiceArray;
	public AudioClip[] negativeVoiceArray;

	public void PlayPositiveVoice ()
	{
		int rand = Random.Range (0, 3);
		AudioClip audioClip = positiveVoiceArray [rand];
		audio.PlayOneShot (audioClip);
	}

	public void PlayNegativeVoice ()
	{
		int rand = Random.Range (0, 3);
		AudioClip audioClip = negativeVoiceArray [rand];
		audio.PlayOneShot (audioClip);
	}

	public void SetMute (bool muteState)
	{
		audio.mute = muteState;
	}
}
