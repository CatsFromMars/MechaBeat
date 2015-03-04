using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    private GameObject player;
    private Vector3 offset;

    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");

        offset = player.transform.position - transform.position;
    }

    void Update () {
        transform.position = player.transform.position - offset;
    }
}
