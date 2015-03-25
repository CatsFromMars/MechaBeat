using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public Vector3 offset = new Vector3(0, 1.5f, -10);

    public float minX, minY, maxX, maxY;
    
    private GameObject player;

	// Use this for initialization
	void Awake () {
		player = GameObject.FindGameObjectWithTag ("Player");
	}
	
	// Update is called once per frame
	void LateUpdate () {
		Vector3 temp = player.transform.position + offset;
        transform.position = new Vector3(Mathf.Clamp(temp.x, minX, maxX), Mathf.Clamp(temp.y, minY, maxY), -10);
	}
}
