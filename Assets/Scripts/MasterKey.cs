using UnityEngine;
using System.Collections;

public class MasterKey : MonoBehaviour {

	public int numKeys;
	public int collectedKeys;
	private bool canOpenDoor;

	// Use this for initialization
	void Start () {
		//numKeys = 1;
		collectedKeys = 0;
		canOpenDoor = false;
	}

	void update(){
		
	}
	
	// Update is called once per frame
	public void incrementCollectedKeys () {
		collectedKeys++;
		if (collectedKeys >= numKeys) {
			//opendoor
			canOpenDoor = true;
		}
	}

	public bool canOpen()
	{
		return canOpenDoor;
	}
}
