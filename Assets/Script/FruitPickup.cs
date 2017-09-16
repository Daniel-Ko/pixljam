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
        kiwifruitSoundEffect = FindObjectOfType<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other) {
        if (other.GetComponent<KiwiController>() == null) return;
        KiwiController script = other.GetComponent<KiwiController>();
        script.feedKiwi(weight);      
        levelManager.PlayKiwiParticleAnimation(script.transform.position, script.transform.rotation);
        
        kiwifruitSoundEffect.Play();
        Debug.Log("PLAY SOUND");
        /*
        ScoreManager.AddPoints(pointsToAdd);
        */
        Destroy(gameObject);
    }
}
