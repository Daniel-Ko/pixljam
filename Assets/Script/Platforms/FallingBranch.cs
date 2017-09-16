using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBranch : MonoBehaviour {
	private Rigidbody2D r2d2;
	public float fallDelay = 2;
	private FallingLinked parent;

	Vector3 platvec;
	float w;
	float h;

	void Start() {
		platvec = this.transform.position;

		h = GetComponent<SpriteRenderer> ().bounds.size.y;

		r2d2 = GetComponent<Rigidbody2D> ();
		r2d2.isKinematic = true;
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.collider.CompareTag ("Bird") || coll.collider.CompareTag ("Kiwi")) {

			PlayerController player = coll.gameObject.GetComponent<PlayerController> ();

			if (PlayerIsOnTopOfBranch(player)) {
				parent = (FallingLinked) transform.parent.gameObject.GetComponent<FallingLinked>();
				parent.SetFall (true);
				parent.setPlayer (player);
//				SetPlatformFallDelay (player); //make the platform fall according to weight of player
//				StartCoroutine (Fall ()); //make the platform fall
			}
		}
	}

	bool PlayerIsOnTopOfBranch(PlayerController player) {
		Vector3 pvec = player.gameObject.transform.position;

		return (pvec.y > platvec.y + (h / 3));  //if player is only touching top of object
	}
}
