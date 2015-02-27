using UnityEngine;
using System.Collections;

public class SmoothRotation : _AbstractRhythmObject {
    public Vector3[] rotations;
    public int offset;
    public bool spherical;

    override
    protected void rhythmUpdate(int beat) {
        int size = rotations.Length;
        
        if (size <= 1) {
            return;
        }
        
        int idx = beat + offset;

        Quaternion startRot = Quaternion.Euler(rotations[idx % size]);
        Quaternion endRot = Quaternion.Euler(rotations[(idx + 1) % size]);

        if (spherical) {
            StartCoroutine(sphericalRotate(startRot, endRot, secondsPerBeat));
        }
        else {
            StartCoroutine(linearRotate(startRot, endRot, secondsPerBeat));
        }
    }
    
    private IEnumerator linearRotate(Quaternion startRot, Quaternion endRot, float duration) {
        float startTime = Time.time;
        
        while (Time.time <= startTime + duration) {
            float lerpPercent = Mathf.Clamp01((Time.time - startTime)/duration);
            transform.rotation = Quaternion.Lerp(startRot, endRot, lerpPercent);
            yield return null;
        }
    }
    
    private IEnumerator sphericalRotate(Quaternion startRot, Quaternion endRot, float duration) {
        float startTime = Time.time;
        
        while (Time.time <= startTime + duration) {
            float lerpPercent = Mathf.Clamp01((Time.time - startTime)/duration);
            transform.rotation = Quaternion.Slerp(startRot, endRot, lerpPercent);
            yield return null;
        }
    }
}
