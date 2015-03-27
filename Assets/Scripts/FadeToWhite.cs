using UnityEngine;
using System.Collections;

public class FadeToWhite : MonoBehaviour {

	private GameObject gameController;
	// Use this for initialization
	void Awake () {
		gameController = GameObject.FindGameObjectWithTag ("GameController");
	}
	
	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player") {
			MasterKey m = gameController.GetComponent<MasterKey>();
			if(m.canOpen())
			{
				//Application.LoadLevel(nextLevel);
				GameObject.FindGameObjectWithTag("WhiteFade").GetComponent<Animator>().SetTrigger("FadeIn");
			}
			
		}
	}

	public void fader()
	{

	}
}
