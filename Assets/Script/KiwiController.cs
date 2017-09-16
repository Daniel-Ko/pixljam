using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KiwiController : MonoBehaviour {
    private int totalWeight;

    private PlayerController script = null;
    private bool beingCarried;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //we know of the bird
        if (beingCarried) {
             //make this kiwi pos be moved along with Birds
             //transform.Translate(new Vector3(script.GetComponent<Rigidbody2D>().position.x/2, script.GetComponent<Rigidbody2D>().position.y/2));
             //GetComponent<Rigidbody2D>().position = new Vector3(script.GetComponent<Rigidbody2D>().position.x, script.GetComponent<Rigidbody2D>().position.y / 2);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Make sure we are just connecting with Bird
        if (other.gameObject.tag == "Bird")
        {
            if (other.GetComponent<PlayerController>() == null) return;
            PlayerController script = other.GetComponent<PlayerController>();
            this.script = script;
            script.carryingCheck(true);
            beingCarried = true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        //Make sure its the bird
        if (other.gameObject.tag == "Bird")
        {
            if (this.script != null)
            {
                this.script.carryingCheck(false);
                beingCarried = false;
            }
        }
    }
}
