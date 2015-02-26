using UnityEngine;
using System.Collections;

public class SmoothCycleMovement : _AbstractRhythmObject {

    public Vector3[] positions;
    public int offset;

    override
    public void rhythmUpdate(int beat) {

        int size = positions.Length;

        if (size <= 1) {
            return;
        }

        int idx = beat + offset;

        StartCoroutine(move(positions [idx % size], positions [(idx + 1) % size], secondsPerBeat));
    }

    private IEnumerator move(Vector3 startPos, Vector3 endPos, float duration, float beatMod = 1) {
		//duration = seconds per beat.
        float startTime = Time.time;

        while (Time.time <= startTime + duration) {
            float lerpPercent = Mathf.Clamp01((Time.time - startTime)/(duration*beatMod));
            transform.position = Vector3.Lerp(startPos, endPos, lerpPercent);
            yield return null;
        }
    }
}
