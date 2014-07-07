using UnityEngine;
using System.Collections;

public class BackGroundKeeper : MonoBehaviour
{

	public tk2dSprite backGroundSprite;

	public void UpdateBackGround (int evolutionPoint)
	{
		if (evolutionPoint < 6) {
			backGroundSprite.SetSprite ("bg_main_1");
		} else if (evolutionPoint < 9) {
			backGroundSprite.SetSprite ("bg_main_2");
		} else {
			backGroundSprite.SetSprite ("bg_main_3");
		}
	}
}
