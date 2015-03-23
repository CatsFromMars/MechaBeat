
using UnityEngine;
using System.Collections;
using Rhythmify;

public class Pulse : _AbstractRhythmObject {
    public Vector3[] scaleVectors;
    public int modulo;
    public int moduloBeat;
        
    override protected void rhythmUpdate() {
        int size = scaleVectors.Length;
            
        if (size < 1 || modulo < 1 || getBeat() % modulo != moduloBeat) {
            return;
        }
        
        int idx = getBeat() / modulo;
            
        Vector3 endScale = scaleVectors[idx % size];

        StartCoroutine(scale(new Vector3(1,1,1), endScale, secondsPerBeat * modulo));
    }
        
    private IEnumerator scale(Vector3 startScale, Vector3 endScale, float duration) {
        float startTime = Time.time;

        while (Time.time <= startTime + duration) {
            float lerpPercent = Mathf.Clamp01((Time.time - startTime) / duration);
            transform.localScale = Vector3.Lerp(startScale, endScale, lerpPercent);
            yield return null;
        }
    }
}