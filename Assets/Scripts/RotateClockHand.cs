using UnityEngine;
using System.Collections;

public class RotateClockHand : MonoBehaviour {

	private bool canRotate;
	public GameObject clockhand;
	// Use this for initializationad
	void Start () {
		canRotate = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

//	public void rotateHand()
//	{
//		if(canRotate)
//		{
//			if(position1)
//			{
//
//				clockhand.transform.Rotate(Vector3.up*rotationAngle);
//				canRotate = false;
//				position1 = !position1;
//			}
//			else
//			{
//				clockhand.transform.Rotate(Vector3.up*-rotationAngle);
//				canRotate = false;
//				position1 = !position1;
//			}
//		}
//	}

	void OnCollisionEnter(Collision other)
	{
		if (other.collider.gameObject.tag == "Player") {
			RotateClock c = clockhand.GetComponent<RotateClock>();
			if(canRotate)
			{
				c.rotateHand();
				canRotate = false;
			}
			
		}
	}

	void OnCollisionExit(Collision other)
	{
		canRotate = true;
	}
}
