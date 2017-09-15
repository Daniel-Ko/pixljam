using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    public GameObject kiwifruitParticle;
    private PlayerController player;

	// Use this for initialization
	void Start () {
        player = FindObjectOfType<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PlayKiwiParticleAnimation(Vector3 position, Quaternion rotation) {
        Instantiate(kiwifruitParticle, position, rotation);
    }
}
