using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour {
    void OnCollisionEnter(Collision collision) {
        collision.gameObject.transform.parent = transform;
    }

    void OnCollisionExit(Collision collision) {
        collision.gameObject.transform.parent = null;
    }
}
