using UnityEngine;
using System.Collections;

public class Windkey : MonoBehaviour {

	public GameObject gameController;
	private int coll;

	// Use this for initialization
	void Awake () {
		coll = 0;
		gameController = GameObject.FindGameObjectWithTag ("GameController");
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(Vector3.up * Time.deltaTime * 60.0f);
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
