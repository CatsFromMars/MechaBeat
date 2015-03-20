using UnityEngine;
using System.Collections;

public class RotateClock : MonoBehaviour {

	public bool position1;
	public int rotationAngle;
	// Use this for initialization
	void Start () {
		position1 = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void rotateHand()
	{
			if(position1)
			{
				
				this.transform.Rotate(Vector3.up*rotationAngle);
				position1 = !position1;
			}
			else
			{
				this.transform.Rotate(Vector3.up*-rotationAngle);
				position1 = !position1;
			}
	}

}
