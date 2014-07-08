using UnityEngine;
using System.Collections;

public class MainGalController : MonoBehaviour
{

	public float speedX;
	public float speedY;
	public float topY;
	public float bottomY;
	public float leftX;
	public float rightX;
	
	void Start ()
	{
		int rand = Random.Range (0, 3);
		switch (rand) {
		case 0:
			speedX = -speedX;
			break;
		case 1:
			speedY = -speedY;
			break;
		case 2:
			speedX = -speedX;
			speedY = -speedY;
			break;
		}
		StartCoroutine (Walk ());
	}

	private IEnumerator Walk ()
	{
		if (transform.localPosition.x > rightX) {
			speedX = -speedX;
		}
		if (transform.localPosition.x < leftX) {
			transform.localPosition = new Vector3 (-2.5f, transform.position.y, 0);
			speedX = -speedX;
		}
		if (transform.localPosition.y > topY) {
			transform.localPosition = new Vector3 (transform.position.x, 2.5f, 0);
			speedY = -speedY;
		}
		if (transform.localPosition.y < bottomY) {
			transform.localPosition = new Vector3 (transform.position.x, -2.5f, 0);
			speedY = -speedY;
		}
		transform.Translate (speedX, speedY, 0);
		yield return new WaitForSeconds (2.0f);
		StartCoroutine (Walk ());
	}
	
}
