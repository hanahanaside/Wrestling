using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public  abstract  class AbstractAnimationController : MonoBehaviour
{
	private float animationSeconds = 3.0f;
	public abstract void CompleteAnimation ();

	public virtual void StartAnimation (Transform target)
	{
		if (PrefsManager.getInstance ().GetSoundMode() == PrefsManager.SOUND_ON) {
			audio.Play ();
		}
	}

	public void PlayAnimation(GameObject animationObject){
		iTweenEvent.GetEvent(animationObject,"Animation").Play();
	}

	public IEnumerator WaitForComplete(){
		yield return new WaitForSeconds(animationSeconds);
		CompleteAnimation();
	}
}