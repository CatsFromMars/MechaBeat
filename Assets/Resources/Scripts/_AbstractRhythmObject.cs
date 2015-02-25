using UnityEngine;
using System.Collections;


public abstract class _AbstractRhythmObject : MonoBehaviour {

    private AudioSource audioSource;
    private AudioClip audioClip;

    private int BPM;
    private float samplesPerBeat;
    private int lastBeatUpdate = -1;


    public void Start() {
        GameObject bgmContainer = GameObject.FindGameObjectWithTag("BackgroundMusic");

        audioSource = bgmContainer.audio;
        audioClip = audioSource.clip;

        BPM = bgmContainer.GetComponent<MusicWrapper>().BPM;

        samplesPerBeat = 60 * audioClip.frequency/BPM;
    }

    public void Update() {
        int beat = (int)(audioSource.timeSamples / samplesPerBeat);

        if (beat != lastBeatUpdate) {
            lastBeatUpdate = beat;
            rhythmUpdate(beat);
        }
    }

    public abstract void rhythmUpdate(int beat);
}
