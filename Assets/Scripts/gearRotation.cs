using UnityEngine;
using System.Collections;

public class gearRotation : MonoBehaviour {

	public float speed = 180f;
	private Vector3 axle = new Vector3(0,0,1);
	
	
	void Update ()
	{
		transform.Rotate(axle, speed * Time.deltaTime);
	}
}
