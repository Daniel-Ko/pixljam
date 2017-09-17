using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {

	private PlayerController player;
	private bool loadLock;

	// Use this for initialization
	void Start () {
		player = GameObject.FindObjectOfType<PlayerController> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (player.HP <= 0){// && !loadLock) {
			loadLock = true;
			SceneManager.LoadScene ("EndScreen");
			Debug.Log ("?");
		} 
	}

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.GetComponent<PlayerController>() != null)
        {
            SceneManager.LoadScene("WinningScene");
        }
    }
}
