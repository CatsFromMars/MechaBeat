using UnityEngine;
using System.Collections;
using Rhythmify;

public class FadeColors : _AbstractRhythmObject {
    public Color[] fadeColors;
    public int modulo;

    private Color startColor;

    override protected void init() {
        startColor = renderer.material.color;
    }
    
    override protected void rhythmUpdate() {
        int size = fadeColors.Length;
        
        if (size < 1 || modulo < 1 || getBeat() % modulo != 0) {
            return;
        }
        
        int idx = getBeat() / modulo;
        
        StartCoroutine(scaleOut(fadeColors[idx % size], secondsPerBeat));
    }
    
    private IEnumerator scaleOut(Color fadeColor, float duration) {
        float startTime = Time.time;
        duration *= modulo;

        while (Time.time <= startTime + duration) {
            float lerpPercent = Mathf.Clamp01((Time.time - startTime) / duration);
            renderer.material.color = Color.Lerp(startColor, fadeColor, lerpPercent);

            yield return null;
        }
    }
}