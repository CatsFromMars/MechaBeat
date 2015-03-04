using UnityEngine;
using System.Collections;
using Rhythmify;

public class BackgroundMovement : _AbstractRhythmObject {
    public Vector2[] steps;
    public int[] indices;

    override protected void rhythmUpdate(int beat) {
        int size = steps.Length;

        if (size < 1) {
            return;
        }

        int idx = beat;
        if (indices.Length > 0) {
            idx = indices[idx % indices.Length];
        }

        StartCoroutine(stepBackground(steps[idx % size], secondsPerBeat));
    }

    private IEnumerator stepBackground(Vector2 step, float duration){
        float startTime = Time.time;
        Vector2 endPos = renderer.material.GetTextureOffset("_MainTex") + step;

        while (Time.time <= startTime + duration) {
            Vector2 startPos = renderer.material.GetTextureOffset("_MainTex");
            float lerpPercent = Mathf.Clamp01((Time.time - startTime) / duration);
            renderer.material.SetTextureOffset("_MainTex", Vector2.Lerp(startPos, endPos, lerpPercent));
            yield return null;
        }
    }
}
