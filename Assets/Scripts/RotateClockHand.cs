using UnityEngine;
using System.Collections;

public class RotateClockHand : MonoBehaviour {

	private bool canRotate;
	public GameObject clockhand;
	private bool position1;
	public int rotationAngle;
	// Use this for initialization
	void Start () {
		canRotate = true;
		position1 = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision other)
	{
		if (other.collider.gameObject.tag == "Player") {
			if(canRotate)
			{
				if(position1)
				{
					clockhand.transform.Rotate(Vector3.up*rotationAngle);
					canRotate = false;
					position1 = !position1;
				}
				else
				{
					clockhand.transform.Rotate(Vector3.up*-rotationAngle);
					canRotate = false;
					position1 = !position1;
				}
			}

			
		}
	}

	void OnCollisionExit(Collision other)
	{
		canRotate = true;
	}
}
