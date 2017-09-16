using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikePlatform : MonoBehaviour {

	// Spike at the top or bottom.
	public bool top;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}
		
	void OnCollisionEnter2D(Collision2D other) {
		if (other.collider.CompareTag("Bird") ||other.collider.CompareTag("Kiwi")){

			ContactPoint2D contact = other.contacts [0];
			// top is true if the spike is at the top.
			if (top) {
				// Collision is from the top.
				if (Vector3.Dot (contact.normal, Vector3.down) > 0.5) {
					other.gameObject.SendMessage ("hurtPlayer", 1);
				}
			} else {
				// Collision is from the down.
				if (Vector3.Dot (contact.normal, Vector3.up) > 0.5) {
					other.gameObject.SendMessage ("hurtPlayer", 1);
				}
			}
				
		}
	}
		
}
