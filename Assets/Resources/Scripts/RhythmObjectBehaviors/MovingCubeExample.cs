using UnityEngine;
using System.Collections;

public class MovingCubeExample : _AbstractRhythmObject {

    override
    public void rhythmUpdate(int beat) {
        if (beat % 2 == 0) {
            transform.position = new Vector3(0, 0, 1);
        }
        if (beat % 2 == 1) {
            transform.position = new Vector3(0, 0, 0);
        }
    }
}