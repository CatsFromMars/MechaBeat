using UnityEngine;
using System.Collections;
using Rhythmify;

public class JumpTest : _AbstractRhythmObject 
{
	public float jumpHeight;
	private CharacterController characterController;

	void Awake() {
		characterController = GetComponent<CharacterController> ();
	}

	override
	protected void rhythmUpdate (int beat)
	{
		Vector3 pos = Vector3.up*jumpHeight;
		characterController.Move(pos);
	}

}
