using UnityEngine;
using System.Collections;

public class BreakingPlatform : MonoBehaviour
{
	private Rigidbody2D r2d2;
	public int hp;
	Texture2D hp2;
	Texture2D hp1;


	//var remains : GameObject;

	void Start() {
		r2d2 = GetComponent<Rigidbody2D> ();
		r2d2.isKinematic = true;
		hp = 2;
		//platformTexture = (Texture2D) Resources.Load("Art/Platforms/platform1.png")
		//renderer.material.mainTexture = texture;
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.collider.CompareTag ("Kiwi") ) {
			BounceDamage(coll.gameObject.GetComponent<KiwiController>());
			if(hp == 0)	
				StartCoroutine (PlatformBreak ());
		}
	}

	IEnumerator PlatformBreak() {
		//Instantiate(remains, transform.position, transform.rotation);
		Destroy(gameObject);
		yield return 0;
	}

	void BounceDamage(KiwiController kiwi) {
		if (kiwi == null)
			return;
		if(kiwi.isBouncy ()) { 
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

