﻿using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

	public string nextLevel;
	public GameObject gameController;
	public bool loaded;
	public bool col;
	// Use this for initialization
	void Awake () {
		loaded = false;
		col = false;
		gameController = GameObject.FindGameObjectWithTag ("GameController");
	}
	
	// Update is called once per frame
	void Update () {

	}
	
//	void OnTriggerEnter(Collider other)
//	{
//		if (other.tag == "Player") {
//			MasterKey m = gameController.GetComponent<MasterKey>();
//			if(m.canOpen())
//			{
//				loaded = true;
//				Application.LoadLevel(nextLevel);
//			}
//
//		}
//	}

	void OnCollisionEnter(Collision other)
	{
		if (other.collider.gameObject.tag == "Player") {
			MasterKey m = gameController.GetComponent<MasterKey>();
			col = true;
			if(m.canOpen())
			{
				loaded = true;
				Application.LoadLevel(nextLevel);
			}
			
		}
	}
}
