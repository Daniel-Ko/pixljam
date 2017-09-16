using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadlySpike : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D target){

		// If the Player touches the deadly spike.
		if (target.gameObject.tag == "Deadly") {
			PlayerHurt ();
		}
	}

	void PlayerHurt(){
		// Player is destroyed for now.
		// Need to hurt the player though.
		Destroy (gameObject);
	}
}
