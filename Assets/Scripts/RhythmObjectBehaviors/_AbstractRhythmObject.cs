using UnityEngine;
using System.Collections;


public abstract class _AbstractRhythmObject : MonoBehaviour {

    /* Protected variables (can be accessed by subclasses) */
    protected int BPM;
    protected float samplesPerBeat;
    protected float secondsPerBeat;

    /* Private variables */
    private AudioSource audioSource;
    private AudioClip audioClip;

    private int lastBeatUpdate = -1;

    public void Start() {
        GameObject bgmContainer = GameObject.FindGameObjectWithTag("BackgroundMusic");

        audioSource = bgmContainer.audio;
        audioClip = audioSource.clip;

        BPM = bgmContainer.GetComponent<MusicWrapper>().BPM;
        
        secondsPerBeat = 60.0f / BPM;
        samplesPerBeat = secondsPerBeat * audioClip.frequency;
    }

	public void Update() { // Place this in Update();
		int beat = (int)(audioSource.timeSamples / samplesPerBeat);
		
		if (beat != lastBeatUpdate) {
			lastBeatUpdate = beat;
			rhythmUpdate(beat);
		}
		
		asyncUpdate();
	}

    protected bool onBeat(float delta) {
        float beatOffset = audioSource.timeSamples % samplesPerBeat;
        return beatOffset < delta * audioClip.frequency;
    }

	// Use asyncUpdate() to handle rhythm independent things
	protected virtual void asyncUpdate() {

	}

	// Use rhythmUpdate() to specify what to do on each beat.
    protected abstract void rhythmUpdate(int beat);
}
