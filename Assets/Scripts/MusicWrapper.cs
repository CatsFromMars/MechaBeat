using UnityEngine;
using System.Collections;

public class MusicWrapper : MonoBehaviour {
    /* Public serializable variables */
    public int BPM;
    public float introTime;
    public float loopThreshold;

    private AudioSource audioSource;
    private AudioClip audioClip;
    private float audioLength;
    private float loopTime;

    public void Start() {
        audioSource = gameObject.audio;
        audioClip = audioSource.clip;
        audioLength = audioClip.length;

        loopTime = audioLength - introTime;
    }

    public void Update() {
        if (introTime > 0 && loopThreshold > 0) {
            if (audioSource.timeSamples > loopThreshold * audioClip.frequency) {
                audioSource.timeSamples -= Mathf.RoundToInt(loopTime * audioClip.frequency);
            }
        }
    }
}
