using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour
{

	public float speedX;
	public float speedY;
	public float topY;
	public float bottomY;
	public float leftX;
	public float rightX;
	private bool isWalking = true;

	void Start ()
	{
		int rand = Random.Range (0, 4);
		Debug.Log ("rand = " + rand);
		switch (rand) {
		case 0:

			break;
		case 1:
			speedX = -speedX;
			break;
		case 2:
			speedY = -speedY;
			break;
		case 3:
			speedX = -speedX;
			speedY = -speedY;
			break;
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!isWalking) {
			return;
		}
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
	}

	public void StartWalking ()
	{
		isWalking = true;
	}

	public void StopWalking ()
	{
		isWalking = false;
	}
}
