using UnityEngine;
using System.Collections;

public class MasterKey : MonoBehaviour {

	public int numKeys;
	public int collectedKeys;

	// Use this for initialization
	void Start () {
		numKeys = 4;
		collectedKeys = 0;
	}
	
	// Update is called once per frame
	public void incrementCollectedKeys () {
		collectedKeys++;
		if (collectedKeys == numKeys) {
			//opendoor
		}
	}
	
}
