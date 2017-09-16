using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikePlatform : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}
		
	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Bird" || other.tag == "Kiwi"){
			PlayerController script = other.GetComponent<PlayerController>();
			script.hurtPlayer (1);
		}
	}
		
}
