using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikePlatform : MonoBehaviour
{

	// Spike at the top or bottom.
	public bool top;
	public int bounceBackForce = 200;
	public AudioClip bounceClip;

	// Use this for initialization
	void Start ()
	{

	}

	// Update is called once per frame
	void Update ()
	{

	}

	void OnCollisionEnter2D (Collision2D other)
	{

		bool collided = false;
		ContactPoint2D contact = other.contacts [0];

		if (other.collider.CompareTag ("Bird") || other.collider.CompareTag ("Kiwi")) {
			// top is true if the spike is at the top.
			if (top) {
				// Collision is from the top.
				if (Vector3.Dot (contact.normal, Vector3.down) > 0.5) {
					other.gameObject.SendMessage ("hurtPlayer", 1);
					collided = true;
				}
			} else {
				// Collision is from the bottom.
				if (Vector3.Dot (contact.normal, Vector3.up) > 0.5) {
					other.gameObject.SendMessage ("hurtPlayer", 1);
					collided = true;
				}
			}
				
		}

		if (collided) {
			StartCoroutine (bounceBack (other, contact));
		}
	}

	public IEnumerator bounceBack (Collision2D other, ContactPoint2D contact)
	{

		float timer = 0;
		float bounceDuration = 0.01f;

		// Calculate angle between collision point and player.
		Vector2 platform = (Vector2)transform.position;
		Vector2 dir = contact.point - platform;

		// Get the opposite vector and normalized it.
		dir = -dir.normalized;

		while (timer < bounceDuration) {
			timer += Time.deltaTime;

			// Player bounce back and flash red.
			other.gameObject.GetComponent<Rigidbody2D> ().AddForce (dir * bounceBackForce);
			other.gameObject.GetComponent<Animation> ().Play ("HurtAnimation");
		}
			
		// Bounce back sound.
		if (bounceClip) {
			AudioSource.PlayClipAtPoint (bounceClip, transform.position);
		}

		yield return 0;


	}
		
}
