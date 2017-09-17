using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingLinked : MonoBehaviour {
	private Rigidbody2D r2d2;
	public float fallDelay = 2;
	private bool isFalling = false;
	private PlayerController player;
	private FallingBranch branch;

	Vector3 platvec;
	float w;
	float h;

	// Use this for initialization
	void Start () {
		r2d2 = GetComponent<Rigidbody2D> ();
		r2d2.isKinematic = true;

		foreach (Transform child in transform) {
			if(child.gameObject.tag == "Branch") {
				branch = (FallingBranch)child.GetComponent<FallingBranch> ();
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(isFalling) {
			SetPlatformFallDelay (player); //make the platform fall according to weight of player
		}
	}

	public float GetDelay() {
		return fallDelay;
	}

	public bool IsFalling() {
		return isFalling;
	}

	//called by children
	public void SetFall(bool canFallNow) {
		isFalling = canFallNow;
	}

	//called by player
	public void setPlayer(PlayerController pc) {
		if (pc == null)
			return;
		player = pc;
	}
		

	void SetPlatformFallDelay(PlayerController player) {
		if (player == null)
			return;

		float newDelay = 2;

		newDelay -= (float) ((player.weight() / 5) * 0.2);
		fallDelay = newDelay;
	}
}


