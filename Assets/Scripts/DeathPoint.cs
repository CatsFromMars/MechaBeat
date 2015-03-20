using UnityEngine;
using System.Collections;

public class DeathPoint : MonoBehaviour {
    void OnTriggerEnter(Collider collision) {
        if (collision.gameObject.tag == "Player") {
            collision.gameObject.GetComponent<GoodController>().resetToCheckPoint();
        }
    }
}
