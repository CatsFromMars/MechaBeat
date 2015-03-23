using UnityEngine;
using System.Collections;

public class MasterKey : MonoBehaviour {

	public int numKeys;
	private int collectedKeys;
	private bool canOpenDoor;

    private GameObject keyProgress;
    private GameObject keyProgressEffect;

	void Start () {
		collectedKeys = 0;
		canOpenDoor = false;

        GameObject guiCamera = GameObject.FindGameObjectWithTag("GUI");
        keyProgress = guiCamera.transform.FindChild("Key Info/Key Progress").gameObject;
        keyProgressEffect = guiCamera.transform.FindChild("Key Info Effect/Key Progress").gameObject;

        updateKeyProgress();
	}

    private void updateKeyProgress() {
        string progress = collectedKeys + "/" + numKeys;
        keyProgress.GetComponent<TextMesh>().text = progress;
        keyProgressEffect.GetComponent<TextMesh>().text = progress;
    }
	
	public void incrementCollectedKeys () {
        collectedKeys++;
        updateKeyProgress();
	}

    public int getNumKeys() {
        return collectedKeys;
    }

	public bool canOpen() {
        return collectedKeys >= numKeys;
	}
}