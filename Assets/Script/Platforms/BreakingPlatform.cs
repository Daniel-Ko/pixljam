using UnityEngine;
using System.Collections;

public class BreakingPlatform : MonoBehaviour
{
	private Rigidbody2D r2d2;
	public int hp;
	Texture2D platformTexture;

	//var remains : GameObject;

	void Start() {
		r2d2 = GetComponent<Rigidbody2D> ();
		r2d2.isKinematic = true;
		hp = 2;
		//platformTexture = (Texture2D) Resources.Load("Art/Platforms/platform1.png")
		//renderer.material.mainTexture = texture;
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.collider.CompareTag ("Bird") || coll.collider.CompareTag ("Kiwi") ) {
			BounceDamage(coll.gameObject.GetComponent<PlayerController>());
			if(hp == 0)
				StartCoroutine (PlatformBreak ());
		}
	}

	IEnumerator PlatformBreak() {
		//Instantiate(remains, transform.position, transform.rotation);
		Destroy(gameObject);
		yield return 0;
	}

	void BounceDamage(PlayerController player) {
		if (player == null)
			return;
		if(false) { //player.isBounce()
			--this.hp;
			SetTexture ();
		}
	}

	void SetTexture() {
		if(hp == 2) {
			
		} else if(hp == 1) {
			
		}
	}
}

