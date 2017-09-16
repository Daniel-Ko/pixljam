using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour {
	private Rigidbody2D r2d2;
	public float fallDelay;

	void Start() {
		r2d2 = GetComponent<Rigidbody2D> ();
		r2d2.isKinematic = true;
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.collider.CompareTag ("Bird") || coll.collider.CompareTag ("Kiwi") ) {
			StartCoroutine (Fall ());
		}
	}

	IEnumerator Fall() {
		yield return new WaitForSeconds (fallDelay);
		r2d2.isKinematic = false;
		GetComponent<BoxCollider2D> ().isTrigger = true;
		yield return 0;
	}


}
	