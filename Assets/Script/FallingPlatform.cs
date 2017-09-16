using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour {
	private Rigidbody2D r2d2;
	public float fallDelay;

	void Start() {
		r2d2 = GetComponent<Rigidbody2D> ();
		r2d2.isKinematic = true;
		print (fallDelay);
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.collider.CompareTag ("Bird") || coll.collider.CompareTag ("Kiwi") ) {
			setWeight (coll.gameObject.GetComponent<PlayerController>());
			StartCoroutine (Fall ());
		}
	}

	IEnumerator Fall() {
		yield return new WaitForSeconds (fallDelay);
		r2d2.isKinematic = false;
		GetComponent<BoxCollider2D> ().isTrigger = true;
		yield return 0;
	}
		
	void setWeight(PlayerController player) {
		if (player == null)
			return;

		float newDelay = fallDelay;
		newDelay += ((float) (player.getTotalKiwisEatten()) / 5);

		fallDelay = newDelay;
	}
}
	