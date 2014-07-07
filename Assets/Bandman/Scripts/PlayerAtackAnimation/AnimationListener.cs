using UnityEngine;
using System.Collections;

public class AnimationListener : MonoBehaviour {

	public delegate void delegeteMethod (Transform target);

	public static event delegeteMethod AnimationFinishedEvent;

	public static void AnimationFinished (Transform target)
	{
		AnimationFinishedEvent (target);
	}
}
