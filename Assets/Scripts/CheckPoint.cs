using UnityEngine;
using System.Collections;

public class CheckPoint : MonoBehaviour {
    void OnTriggerEnter(Collider collision) {
        if (collision.gameObject.tag == "Player") {
            collision.gameObject.GetComponent<GoodController>().setCheckPoint(transform.position);
        }
    }
}
