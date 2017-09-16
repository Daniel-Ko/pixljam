using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    private PlayerController player;
    public GameObject currentCheckpoint;
    public GameObject kiwifruitParticle;
    public float respawnDelay;
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

    public void RespawnPlayer() {
        StartCoroutine("RespawnPlayerCo");
    }

    public IEnumerator RespawnPlayerCo() {
        player.transform.position = currentCheckpoint.transform.position;
        yield return new WaitForSeconds(respawnDelay);
    }
}
