using UnityEngine;
using System.Collections;

public class DeathPoint : MonoBehaviour {
    public bool canDodge;

    void OnTriggerEnter(Collider collision) {
        if (collision.gameObject.tag == "Player") {
            collision.gameObject.GetComponent<GoodController>().resetToCheckPoint(canDodge);
        }
    }
}
