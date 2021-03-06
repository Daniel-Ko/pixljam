﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {

    public LevelManager levelManager;

	// Use this for initialization
	void Start () {
        levelManager = FindObjectOfType<LevelManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.GetComponent<PlayerController>() == null) return;
        levelManager.currentCheckpoint = gameObject;
        levelManager.PlayKiwiParticleAnimation(gameObject.transform.position, gameObject.transform.rotation);
    }
}
