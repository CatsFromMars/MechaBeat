using UnityEngine;
using System.Collections;
using Rhythmify;

public class GUI_BPM : MonoBehaviour {
	void Start () {
        GameObject bgm = GameObject.FindGameObjectWithTag("Rhythmify_Music");

        GetComponent<TextMesh>().text = "" + bgm.GetComponent<MusicWrapper>().BPM;
	}
}