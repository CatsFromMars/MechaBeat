using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

	public string nextLevel;
	public GameObject gameController;
	public bool loaded;
	// Use this for initialization
	void Start () {
		nextLevel = "Level 3";
		loaded = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player") {
			MasterKey m = gameController.GetComponent<MasterKey>();
			if(m.canOpen())
			{
				loaded = true;
				Application.LoadLevel(nextLevel);
			}

		}
	}
}
