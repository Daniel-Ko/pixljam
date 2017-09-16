using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour {
	private Rigidbody2D r2d2;
	public float fallDelay ;

	Vector3 platvec;
	float w;
	float h;

	void Start() {
		platvec = this.transform.position;

		h = GetComponent<SpriteRenderer> ().bounds.size.y;//((RectTransform)this.transform).rect.height;

		r2d2 = GetComponent<Rigidbody2D> ();
		r2d2.isKinematic = true;
		fallDelay = 2;
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.collider.CompareTag ("Bird") || coll.collider.CompareTag ("Kiwi")) {
			
			PlayerController player = coll.gameObject.GetComponent<PlayerController> ();

			if (PlayerIsOnTopOfPlatform(player)) {
				SetPlatformFallDelay (player); //make the platform fall according to weight of player
				StartCoroutine (Fall ()); //make the platform fall
			}
		}
	}

	bool PlayerIsOnTopOfPlatform(PlayerController player) {
		Vector3 pvec = player.gameObject.transform.position;

		return (pvec.y > platvec.y + (h / 3));  //if player is only touching top of object
	}

	IEnumerator Fall() {
		yield return new WaitForSeconds (fallDelay);
		r2d2.isKinematic = false;
		GetComponent<BoxCollider2D> ().isTrigger = true;
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
	