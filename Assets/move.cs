using UnityEngine;
using System.Collections;

public class move : MonoBehaviour {
	public GameObject key1;
	public GameObject key2;
	
	// Update is called once per frame
	void Update () {
		if((key1==null) & (key2==null))
			transform.rotation=Quaternion.Euler(new Vector3(365,267,94));
	}
}
