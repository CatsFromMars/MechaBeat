using UnityEngine;
using System.Collections;

public class Windkey : MonoBehaviour {

	public GameObject gameController;
	private int coll;

	// Use this for initialization
	void Start () {
		coll = 0;
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(Vector3.up * Time.deltaTime * 40.0f);
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player") {
			MasterKey m = gameController.GetComponent<MasterKey>();
			m.incrementCollectedKeys();
			coll++;
			Destroy(gameObject);
		}
	}
}
