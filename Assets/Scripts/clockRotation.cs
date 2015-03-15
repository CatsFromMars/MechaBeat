using UnityEngine;
using System.Collections;

public class clockRotation : MonoBehaviour {

	public float speed = 180f;
	private Vector3 axle = new Vector3(0,1,0);
	
	
	void Update ()
	{
		transform.Rotate(axle, speed * Time.deltaTime);
	}
}
