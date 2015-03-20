using UnityEngine;
using System.Collections;

public class RotateClockHand1 : MonoBehaviour {

	private bool canRotate;
	public GameObject partnerTrampoline;
	//private bool position1;
	public int rotationAngle;
	// Use this for initialization
	void Start () {
		canRotate = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

//	void OnCollisionEnter(Collision other)
//	{
//		if (other.collider.gameObject.tag == "Player") {
//			RotateClockHand r = partnerTrampoline.GetComponent<RotateClockHand>();
//			if(canRotate)
//			{
//				r.rotateHand();
//				canRotate = false;
//			}
//
//			
//		}
//	}
//
//	void OnCollisionExit(Collision other)
//	{
//		canRotate = true;
//	}
}
