using UnityEngine;
using System.Collections;

public class gearRotation : MonoBehaviour {

	public float speed = 180f;
	
	
	void Update ()
	{
		transform.Rotate(new Vector3(0,0,1), speed * Time.deltaTime);
	}
}
