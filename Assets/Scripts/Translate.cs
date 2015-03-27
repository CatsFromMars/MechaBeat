using UnityEngine;
using System.Collections;

public class Translate : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(new Vector3 (0, -0.03f, 0));
	
	}
}
