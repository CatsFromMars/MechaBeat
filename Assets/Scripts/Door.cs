using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

	public string nextLevel;
	private GameObject gameController;
	// Use this for initialization
	void Awake () {
		gameController = GameObject.FindGameObjectWithTag ("GameController");
	}
	
	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player") {
			MasterKey m = gameController.GetComponent<MasterKey>();
			if(m.canOpen())
			{
				Application.LoadLevel(nextLevel);
			}

		}
	}
}
