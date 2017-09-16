using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceAnimationController : MonoBehaviour {

    public Animation bounceAnimation;
    private Animator anim;
	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}

    private void FixedUpdate()
    {

        
    }

    // Update is called once per frame
    void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.GetComponent<FruitPickup>()!= null) {
            bounceAnimation.Play();
            anim.SetBool("eating", true);
        }
    }
}
