using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingLinked : MonoBehaviour {
	private Rigidbody2D r2d2;
	public float fallDelay = 2;
	private bool isFalling = false;
	private PlayerController player;

	Vector3 platvec;
	float w;
	float h;

	// Use this for initialization
	void Start () {
		r2d2 = GetComponent<Rigidbody2D> ();
		r2d2.isKinematic = true;
	}
	
	// Update is called once per frame
	void Update () {
		if(isFalling) {
			SetPlatformFallDelay (player); //make the platform fall according to weight of player
			StartCoroutine (Fall ()); //make the platform fall
		}
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
		

	IEnumerator Fall() {
		yield return new WaitForSeconds (fallDelay);
		r2d2.isKinematic = false;
		GetComponent<PolygonCollider2D> ().isTrigger = true;
		yield return 0;
	}

	void SetPlatformFallDelay(PlayerController player) {
		if (player == null)
			return;

		float newDelay = 2;

		newDelay -= (float) ((player.weight() / 5) * 0.2);
		fallDelay = newDelay;
	}
}


