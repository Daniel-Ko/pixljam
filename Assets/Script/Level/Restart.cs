using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Restart : MonoBehaviour
{
	private bool restartLock; //thanks, Sian!

	public void Update() {
		if (Input.GetMouseButtonDown (0) && !restartLock) {
			RestartGame ();
		}
	}

	public void RestartGame() {
		restartLock = true;
		SceneManager.LoadScene ("FINAL LEVEL");
	}
}

		