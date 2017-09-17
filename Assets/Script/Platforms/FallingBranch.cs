using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBranch : MonoBehaviour {
	private Rigidbody2D r2d2;
	public float delayBeforeDespawn = 4;
	private FallingLinked parent;


	Vector3 platvec;
	float w;
	float h;

	void Start() {
		platvec = this.transform.position;

		h = GetComponent<SpriteRenderer> ().bounds.size.y;

		r2d2 = GetComponent<Rigidbody2D> ();
		r2d2.isKinematic = true;

		parent = (FallingLinked) transform.parent.gameObject.GetComponent<FallingLinked>();
	}

	void Update() {
		
		if(parent.IsFalling ()) {
			StartCoroutine (Fall ()); //make the platform fall
		}
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.collider.CompareTag ("Bird") || coll.collider.CompareTag ("Kiwi")) {

			PlayerController player = coll.gameObject.GetComponent<PlayerController> ();

			if (PlayerIsOnTopOfBranch(player)) {
				parent.SetFall (true);
				parent.setPlayer (player);
			}
		} else if(coll.collider.CompareTag ("ground")) {
			StartCoroutine (Despawn ());
		}
	}

	bool PlayerIsOnTopOfBranch(PlayerController player) {
		Vector3 pvec = player.gameObject.transform.position;

		return (pvec.y > platvec.y + (h / 3));  //if player is only touching top of object
	}

	IEnumerator Fall() {
		yield return new WaitForSeconds (parent.GetDelay());
		r2d2.isKinematic = false;
		yield return 0;
	}

	IEnumerator Despawn() {
		r2d2.freezeRotation = true;
		yield return new WaitForSeconds (parent.GetDelay());
		Destroy (gameObject, delayBeforeDespawn);
		yield return 0;
	}
}
