using UnityEngine;
using System.Collections;

public class MusicWrapper : MonoBehaviour {
    /* Public serializable variables */
    public int BPM;
    public int beatCount;

    /* Private variables */

    private AudioSource audioSource;
    private AudioClip audioClip;

    private float beatLength;

    private int beatNumber;

    public void Start() {
        audioSource = gameObject.audio;
        audioClip = audioSource.clip;

        beatLength = 60.0f / BPM;
    }

    public void Update() {
        //Debug.Log(audioSource.timeSamples/(int)(beatLength * audioClip.frequency));
    }


}
