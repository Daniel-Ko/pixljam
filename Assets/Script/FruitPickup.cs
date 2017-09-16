using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitPickup : MonoBehaviour {

    public int weight;
    public LevelManager levelManager;
    public AudioSource kiwifruitSoundEffect;

	// Use this for initialization
	void Start () {
        levelManager = FindObjectOfType<LevelManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other) {
        if (other.GetComponent<PlayerController>() == null) return;
        PlayerController script = other.GetComponent<PlayerController>();
        script.feedKiwi(weight);      
        levelManager.PlayKiwiParticleAnimation(gameObject.transform.position, gameObject.transform.rotation);
        kiwifruitSoundEffect.Play();
            /*
            ScoreManager.AddPoints(pointsToAdd);
            */
        Destroy(gameObject);
    }
}
